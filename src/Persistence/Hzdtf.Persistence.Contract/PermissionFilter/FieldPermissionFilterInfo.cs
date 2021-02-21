using Hzdtf.Utility.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Persistence.Contract.PermissionFilter
{
    /// <summary>
    /// 字段权限过滤信息
    /// @ 黄振东
    /// </summary>
    public class FieldPermissionFilterInfo : PermissionFilterInfo
    {
        /// <summary>
        /// 属性文本列表，key：属性名，value：属性文本
        /// </summary>
        public IList<KeyValueInfo<string, string>> PropTexts
        {
            get;
            set;
        }
    }
}
