using Hzdtf.Utility.Cache;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.UserPermission.Tenant
{
    /// <summary>
    /// 租户用户菜单权限缓存接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">用户类型</typeparam>
    public interface ITenantUserMenuPermissionCache<IdT> : ISingleTypeCache<string, IDictionary<string, string[]>>
    {
        /// <summary>
        /// 初始化缓存
        /// </summary>
        /// <param name="tenantId">租户ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        BasicReturnInfo InitCache(IdT tenantId, IdT userId, CommonUseData comData = null);

        /// <summary>
        /// 获取时间范围内没有访问的键数组
        /// </summary>
        /// <param name="timeSpan">时间范围</param>
        /// <returns>时间范围内没有访问的键数组</returns>
        string[] GetWithTSNotAccessKeys(TimeSpan timeSpan);

        /// <summary>
        /// 移除时间范围内没有访问的用户
        /// </summary>
        /// <param name="timeSpan">时间范围</param>
        /// <returns>是否移除成功</returns>
        bool RemoveWithTSNotAccess(TimeSpan timeSpan);

        /// <summary>
        /// 根据租户ID和用户ID移除缓存
        /// </summary>
        /// <param name="tenantId">租户ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否移除成功</returns>
        bool RemoveCache(IdT tenantId, IdT userId);

        /// <summary>
        /// 根据租户ID和用户ID移除缓存
        /// </summary>
        /// <param name="tenantId">租户ID</param>
        /// <param name="userIds">用户ID数组</param>
        /// <returns>是否移除成功</returns>
        bool RemoveCache(IdT tenantId, IdT[] userIds);

        /// <summary>
        /// 根据租户ID和用户ID移除缓存
        /// </summary>
        /// <param name="tenantIdMapUserIds">租户ID映射用户ID，key：租户ID，value：用户ID</param>
        /// <returns>是否移除成功</returns>
        bool RemoveCache(params KeyValueInfo<IdT, IdT>[] tenantIdMapUserIds);

        /// <summary>
        /// 根据租户ID清空缓存
        /// </summary>
        /// <param name="tenantId">租户ID</param>
        /// <returns>是否清空成功</returns>
        bool ClearCache(IdT tenantId);
    }
}
