using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.UserPermission
{
    /// <summary>
    /// 用户菜单权限接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    public interface IUserMenuPermission<IdT>
    {
        /// <summary>
        /// 用户是否拥有权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="menuCode">菜单编码</param>
        /// <param name="funCode">功能编码</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> UserHavePermission(IdT userId, string menuCode, string funCode, CommonUseData comData = null);
    }
}
