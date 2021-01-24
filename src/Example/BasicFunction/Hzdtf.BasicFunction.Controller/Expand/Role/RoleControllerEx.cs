﻿using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Utils;
using Hzdtf.Utility.Model.Return;
using Hzdtf.BasicFunction.Model;
using System.Net;
using Hzdtf.BasicFunction.Service.Contract;

namespace Hzdtf.BasicFunction.Controller
{
    /// <summary>
    /// 角色控制器
    /// @ 黄振东
    /// </summary>
    public partial class RoleController
    {
        /// <summary>
        /// 用户服务
        /// </summary>
        public IUserService UserService
        {
            get;
            set;
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <returns>文件内容结果</returns>
        [HttpGet("Export")]
        [Function(FunCodeDefine.EXPORT_EXCEL_CODE)]
        public virtual FileContentResult Export()
        {
            IDictionary<string, string> dicParams = Request.QueryString.Value.ToDictionaryFromUrlParams();
            KeywordFilterInfo filter = dicParams.ToObject<KeywordFilterInfo, string>();
            ReturnInfo<IList<RoleInfo>> returnInfo = Service.QueryByFilter(filter);
            if (returnInfo.Failure())
            {
                return File(new byte[] { 0 }, null);
            }

            Response.Headers.Add("Content-Disposition", "attachment;filename=" + WebUtility.UrlEncode("角色_" + DateTime.Now.ToFixedDate() + ".xlsx"));

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
        /// 填充页面数据，包含当前用户所拥有的权限功能列表
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        protected override void FillPageData(ReturnInfo<PageInfo> returnInfo)
        {
            var re = UserService.QueryPageData<PageInfo>(MenuCode(), () =>
            {
                return returnInfo.Data;
            });
            returnInfo.FromBasic(re);
        }

        /// <summary>
        /// 创建页面数据
        /// </summary>
        /// <returns>页面数据</returns>
        protected override PageInfo CreatePageData() => new PageInfo();
    }
}