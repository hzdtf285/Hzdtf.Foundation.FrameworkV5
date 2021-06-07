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
        JWT = 1,

        /// <summary>
        /// Jwt_Cookie存储
        /// </summary>
        JWT_COOKIE = 2,

        /// <summary>
        ///  Jwt_Cookie存储，验证时，先从Cookie取，取不到再从Header取
        /// </summary>
        JWT_COOKIE_HEADER = 3
    }
}
