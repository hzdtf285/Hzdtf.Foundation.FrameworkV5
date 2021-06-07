using Hzdtf.Utility.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hzdtf.AUC.AspNet.JwtAuthHandler
{
    /// <summary>
    /// Cookie票据授权处理
    /// @ 黄振东
    /// </summary>
    public class CookieTokenAuthHandler : JwtTokenAuthHandlerBase
    {
        /// <summary>
        /// 获取票据
        /// </summary>
        /// <returns>票据</returns>
        public override string GetToken() => context.Request.Cookies[AuthUtil.COOKIE_AUTH_KEY];
    }
}
