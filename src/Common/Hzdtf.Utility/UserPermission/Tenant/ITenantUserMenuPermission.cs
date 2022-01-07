﻿using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.UserPermission.Tenant
{
    /// <summary>
    /// 租户用户菜单权限接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    public interface ITenantUserMenuPermission<IdT>
    {
        /// <summary>
        /// 用户是否拥有权限
        /// </summary>
        /// <param name="merchantId">商户ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="menuCode">菜单编码</param>
        /// <param name="funCode">功能编码</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> UserHavePermission(IdT merchantId, IdT userId, string menuCode, string funCode, CommonUseData comData = null);

        /// <summary>
        /// 根据用户ID获取拥有权限的菜单功能编码字典
        /// </summary>
        /// <param name="tenantId">租户ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息 key：菜单编码，value：功能编码数组</returns>
        ReturnInfo<IDictionary<string, string[]>> GetHavePermissionMenuFunCodes(IdT tenantId, IdT userId, CommonUseData comData = null);
    }
}
