using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.Enums
{
    /// <summary>
    /// 通讯方式
    /// @ 黄振东
    /// </summary>
    public enum CommunicationMode : byte
    {
        /// <summary>
        /// 无
        /// </summary>
        NONE = 0,

        /// <summary>
        /// HTTP
        /// </summary>
        HTTP = 1,

        /// <summary>
        /// GRPC
        /// </summary>
        GRPC = 2,        
    }
}
