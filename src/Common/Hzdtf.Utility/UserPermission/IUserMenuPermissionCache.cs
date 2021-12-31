using Hzdtf.Utility.Cache;
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
    /// 用户菜单权限缓存接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">用户类型</typeparam>
    public interface IUserMenuPermissionCache<IdT> : ISingleTypeCache<IdT, IDictionary<string, string[]>>
    {
        /// <summary>
        /// 初始化缓存
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        BasicReturnInfo InitCache(IdT userId, CommonUseData comData = null);

        /// <summary>
        /// 获取时间范围内没有访问的用户ID数组
        /// </summary>
        /// <param name="timeSpan">时间范围</param>
        /// <returns>时间范围内没有访问的用户ID数组</returns>
        IdT[] GetWithTSNotAccessKeys(TimeSpan timeSpan);

        /// <summary>
        /// 移除时间范围内没有访问的用户
        /// </summary>
        /// <param name="timeSpan">时间范围</param>
        /// <returns>是否移除成功</returns>
        bool RemoveWithTSNotAccess(TimeSpan timeSpan);
    }
}
