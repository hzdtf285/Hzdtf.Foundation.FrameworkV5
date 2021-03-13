using Hzdtf.BasicFunction.Model;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Service.Contract
{
    /// <summary>
    /// 用户角色服务接口
    /// @ 黄振东
    /// </summary>
    public partial interface IUserRoleService
    {
        /// <summary>
        /// 根据用户ID查询拥有的角色列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        ReturnInfo<IList<RoleInfo>> OwnRolesByUserId(int userId, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 根据角色ID查询拥有的用户列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        ReturnInfo<IList<UserInfo>> OwnUsersByRoleId(int roleId, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 根据角色编码查询拥有的用户列表
        /// </summary>
        /// <param name="roleCode">角色编码</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        ReturnInfo<IList<UserInfo>> OwnUsersByRoleCode(string roleCode, CommonUseData comData = null, string connectionId = null);
    }
}
