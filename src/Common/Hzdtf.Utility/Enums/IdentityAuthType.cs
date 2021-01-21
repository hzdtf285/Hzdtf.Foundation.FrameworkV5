using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Enums
{
    /// <summary>
    /// 身份认证类型
    /// @ 黄振东
    /// </summary>
    public enum IdentityAuthType : byte
    {
        /// <summary>
        /// Cookies
        /// </summary>
        COOKIES = 0,

        /// <summary>
        /// Jwt
        /// </summary>
        JWT = 1
    }
}
