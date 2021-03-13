using Hzdtf.Utility.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Persistence.Contract.PermissionFilter
{
    /// <summary>
    /// 权限过滤接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="PermissionFilterT">权限过滤类型</typeparam>
    public interface IPermissionFilter<PermissionFilterT>
        where PermissionFilterT : PermissionFilterInfo
    {
        /// <summary>
        /// 去过滤
        /// </summary>
        /// <param name="permissionFilter">权限筛选</param>
        void DoFilter(PermissionFilterT permissionFilter);
    }
}
