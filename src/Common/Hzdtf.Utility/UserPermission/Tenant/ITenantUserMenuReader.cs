using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.UserPermission.Merchant
{
    /// <summary>
    /// 租户用户菜单读取接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    public interface ITenantUserMenuReader<IdT>
    {
        /// <summary>
        /// 根据用户ID获取拥有权限的菜单功能编码字典
        /// </summary>
        /// <param name="merchantId">商户ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息 key：菜单编码，value：功能编码数组</returns>
        ReturnInfo<IDictionary<string, string[]>> GetHavePermissionMenuFunCodes(IdT merchantId, IdT userId, CommonUseData comData = null);
    }
}
