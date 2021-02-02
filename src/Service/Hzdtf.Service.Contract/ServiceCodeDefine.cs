using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Service.Contract
{
    /// <summary>
    /// 服务编码定义
    /// 平台服务编码范围1000-9999
    /// 业务服务范围>10000
    /// @ 黄振东
    /// </summary>
    public static class ServiceCodeDefine
    {
        /// <summary>
        /// 数据已经被别人修改过
        /// </summary>
        public const int DATA_MODIFIED = 1000;

        /// <summary>
        /// 数据已经被别人修改过文化键
        /// </summary>
        public const string DATA_MODIFIED_CULTURE_KEY = "Common.Platform.M..DataModifed";
    }
}
