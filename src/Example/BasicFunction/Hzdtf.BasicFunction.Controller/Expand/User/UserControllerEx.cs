using Hzdtf.BasicFunction.Model;
using Hzdtf.BasicFunction.Model.Expand.User;
using Hzdtf.BasicFunction.Service.Contract;
using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Utils;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace Hzdtf.BasicFunction.Controller
{
    /// <summary>
    /// 用户控制器
    /// @ 黄振东
    /// </summary>
    public partial class UserController
    {
        /// <summary>
        /// 角色服务
        /// </summary>
        public IRoleService RoleService
        {
            get;
            set;
        }

        /// <summary>
        /// 修改当前用户密码
        /// </summary>
        /// <param name="currUserModifyPassword">当前用户修改密码</param>
        /// <returns>返回信息</returns>
        [HttpPut("ModifyCurrUserPassword")]
        public virtual ReturnInfo<bool> ModifyCurrUserPassword(CurrUserModifyPasswordInfo currUserModifyPassword)
        {
            var comData = HttpContext.CreateCommonUseData(ComUseDataFactory, menuCode: MenuCode(), functionCode: FunCodeDefine.RESET_PASSWORD_CODE);
            var user = UserTool<int>.GetCurrUser(comData);
            currUserModifyPassword.LoginId = user.LoginId;
            return Service.ModifyPasswordByLoginId(currUserModifyPassword, comData);
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="modifyPassword">修改密码</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [HttpPut("ResetUserPassword")]
        [Function(FunCodeDefine.RESET_PASSWORD_CODE)]
        public virtual ReturnInfo<bool> ResetUserPassword(ModifyPasswordInfo modifyPassword, string connectionId = null) => Service.ResetUserPassword(modifyPassword, HttpContext.CreateCommonUseData(ComUseDataFactory, menuCode: MenuCode(), functionCode: FunCodeDefine.RESET_PASSWORD_CODE));

        /// <summary>
        /// 导出
        /// </summary>
        /// <returns>文件内容结果</returns>
        [HttpGet("Export")]
        [Function(FunCodeDefine.EXPORT_EXCEL_CODE)]
        public virtual FileContentResult Export()
        {
            var comData = HttpContext.CreateCommonUseData(ComUseDataFactory, menuCode: MenuCode(), functionCode: FunCodeDefine.EXPORT_EXCEL_CODE);
            IDictionary<string, string> dicParams = Request.QueryString.Value.ToDictionaryFromUrlParams();
            UserFilterInfo filter = dicParams.ToObject<UserFilterInfo, string>();
            ReturnInfo<IList<UserInfo>> returnInfo = Service.QueryByFilter(filter, comData);
            if (returnInfo.Failure())
            {
                return File(new byte[] { 0 }, null);
            }

            Response.Headers.Add("Content-Disposition", "attachment;filename=" + WebUtility.UrlEncode("用户_" + DateTime.Now.ToFixedDate() + ".xlsx"));

            try
            {
                return File(returnInfo.Data.ToExcelBytes(), "application/vnd.ms-excel");
            }
            catch (Exception ex)
            {
                Log.ErrorAsync("导出Excel发生异常", ex);
                return null;
            }
        }

        /// <summary>
        /// 追加页面数据
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        protected void AppendPageData(ReturnInfo<UserPageInfo> returnInfo)
        {
            var comData = HttpContext.CreateCommonUseData(ComUseDataFactory, menuCode: MenuCode(), functionCode: FunCodeDefine.QUERY_CODE);
            ReturnInfo<IList<RoleInfo>> roleReturnInfo = RoleService.QueryAndNotSystemHide(comData);
            if (roleReturnInfo.Success())
            {
                returnInfo.Data.Roles = roleReturnInfo.Data;
            }
            else
            {
                returnInfo.FromBasic(roleReturnInfo);
            }
        }

        /// <summary>
        /// 填充页面数据，包含当前用户所拥有的权限功能列表
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <param name="returnInfo">返回信息</param>
        protected override void FillPageData(ReturnInfo<UserPageInfo> returnInfo, CommonUseData comData = null)
        {
            var re = Service.QueryPageData<UserPageInfo>(MenuCode(), () =>
            {
                return returnInfo.Data;
            }, re =>
            {
                AppendPageData(re);
            }, comData);
            returnInfo.FromBasic(re);
        }

        /// <summary>
        /// 创建页面数据
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <returns>页面数据</returns>
        protected override UserPageInfo CreatePageData(CommonUseData comData = null) => new UserPageInfo();
    }
}
