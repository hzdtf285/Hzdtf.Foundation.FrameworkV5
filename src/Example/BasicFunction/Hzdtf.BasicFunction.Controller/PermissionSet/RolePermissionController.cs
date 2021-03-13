using Hzdtf.BasicController;
using Hzdtf.BasicFunction.Model;
using Hzdtf.BasicFunction.Model.Expand.Menu;
using Hzdtf.BasicFunction.Service.Contract;
using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace Hzdtf.BasicFunction.Controller.PermissionSet
{
    /// <summary>
    /// 角色权限控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RolePermissionController : PagingControllerBase<int, RoleInfo, IRoleService, PageInfo, KeywordFilterInfo>
    {
        /// <summary>
        /// 菜单服务
        /// </summary>
        public IMenuService MenuService
        {
            get;
            set;
        }

        /// <summary>
        /// 角色菜单功能服务
        /// </summary>
        public IRoleMenuFunctionService RoleMenuFunctionService
        {
            get;
            set;
        }

        /// <summary>
        /// 用户服务
        /// </summary>
        public IUserService UserService
        {
            get;
            set;
        }

        /// <summary>
        /// 获取菜单树列表（包含菜单及所拥有的功能列表）
        /// </summary>
        /// <returns>返回信息</returns>
        [HttpGet("MenuTrees")]
        public virtual IList<MenuTreeInfo> MenuTrees() => MenuService.QueryMenuTrees(HttpContext.CreateCommonUseData(ComUseDataFactory, menuCode: MenuCode(), functionCode: FunCodeDefine.QUERY_CODE)).Data;

        /// <summary>
        /// 获取角色拥有的功能菜单信息列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>返回信息</returns>
        [HttpGet("HaveMenuFunctions")]
        public virtual ReturnInfo<IList<MenuFunctionInfo>> HaveMenuFunctions(int roleId) => RoleMenuFunctionService.QueryMenuFunctionsByRoleId(roleId, HttpContext.CreateCommonUseData(ComUseDataFactory, menuCode: MenuCode(), functionCode: FunCodeDefine.QUERY_CODE));

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="menuFunctionIds">菜单功能ID列表</param>
        /// <returns>返回信息</returns>
        [HttpPut("SavePermission")]
        public virtual ReturnInfo<bool> SavePermission(int roleId, IList<int> menuFunctionIds) => RoleMenuFunctionService.SaveRoleMenuFunctions(roleId, menuFunctionIds, HttpContext.CreateCommonUseData(ComUseDataFactory, menuCode: MenuCode(), functionCode: FunCodeDefine.SAVE_CODE));
        
        /// <summary>
        /// 菜单编码
        /// </summary>
        /// <returns>菜单编码</returns>
        protected override string MenuCode() => "RolePermission";

        /// <summary>
        /// 填充页面数据，包含当前用户所拥有的权限功能列表
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="comData">通用数据</param>
        protected override void FillPageData(ReturnInfo<PageInfo> returnInfo, CommonUseData comData = null)
        {
            var re = UserService.QueryPageData<PageInfo>(MenuCode(), () =>
            {
                return returnInfo.Data;
            }, comData: comData);
            returnInfo.FromBasic(re);
        }

        /// <summary>
        /// 创建页面数据
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <returns>页面数据</returns>
        protected override PageInfo CreatePageData(CommonUseData comData = null) => new PageInfo();
    }
}
