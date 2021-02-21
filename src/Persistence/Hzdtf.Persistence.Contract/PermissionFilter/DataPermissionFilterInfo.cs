using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Persistence.Contract.PermissionFilter
{
    /// <summary>
    /// 数据权限过滤信息
    /// @ 黄振东
    /// </summary>
    public class DataPermissionFilterInfo : PermissionFilterInfo
    {
        /// <summary>
        /// 参数字典，key：参数名，value：参数值
        /// </summary>
        public IDictionary<string, object> Params
        {
            get;
            set;
        }
    }
}
