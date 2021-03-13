using Hzdtf.BasicFunction.Model;
using Hzdtf.BasicFunction.Model.Expand.User;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Service.Contract
{
    /// <summary>
    /// 用户服务接口
    /// @ 黄振东
    /// </summary>
    public partial interface IUserService
    {
        /// <summary>
        /// 根据登录ID修改密码
        /// </summary>
        /// <param name="currUserModifyPassword">当前用户修改密码</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> ModifyPasswordByLoginId(CurrUserModifyPasswordInfo currUserModifyPassword, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="modifyPassword">修改密码</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> ResetUserPassword(ModifyPasswordInfo modifyPassword, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 根据菜单编码和功能编码判断当前用户是否有权限
        /// </summary>
        /// <param name="menuCode">菜单编码</param>
        /// <param name="functionCode">功能编码</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> IsCurrUserPermission(string menuCode, string functionCode, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 根据菜单编码和功能编码集合判断当前用户是否有权限
        /// </summary>
        /// <param name="menuCode">菜单编码</param>
        /// <param name="functionCodes">功能编码集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> IsCurrUserPermission(string menuCode, string[] functionCodes, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 根据用户ID、菜单编码和功能编码集合判断用户是否有权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="menuCode">菜单编码</param>
        /// <param name="functionCodes">功能编码集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> IsPermission(int userId, string menuCode, string[] functionCodes, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 根据菜单编码查询当前用户所拥有的功能信息列表
        /// </summary>
        /// <param name="menuCode">菜单编码</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        ReturnInfo<IList<FunctionInfo>> QueryCurrUserOwnFunctionsByMenuCode(string menuCode, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 根据用户ID和菜单编码查询用户所拥有的功能信息列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="menuCode">菜单编码</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        ReturnInfo<IList<FunctionInfo>> QueryUserOwnFunctionsByMenuCode(int userId, string menuCode, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 判断当前用户是否是系统管理组
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> IsCurrUserAdministrators(CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 判断用户是否是系统管理组
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> IsUserAdministrators(int userId, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 根据筛选条件查询用户列表
        /// </summary>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        ReturnInfo<IList<UserInfo>> QueryByFilter(UserFilterInfo filter, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 查询页面数据
        /// </summary>
        /// <typeparam name="PageInfoT">页面信息类型</typeparam>
        /// <param name="menuCode">菜单编码</param>
        /// <param name="createPage">创建页面数据回调</param>
        /// <param name="appendPageData">追加页面数据回调</param>
        /// <param name="comData">通用数据</param>
        /// <returns></returns>
        ReturnInfo<PageInfoT> QueryPageData<PageInfoT>(string menuCode, Func<PageInfoT> createPage, Action<ReturnInfo<PageInfoT>> appendPageData = null, CommonUseData comData = null)
            where PageInfoT : PageInfo<int>;
    }
}
