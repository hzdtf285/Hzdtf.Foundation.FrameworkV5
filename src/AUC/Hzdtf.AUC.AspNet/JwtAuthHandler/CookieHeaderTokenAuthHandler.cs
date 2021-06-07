using Hzdtf.Utility.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hzdtf.AUC.AspNet.JwtAuthHandler
{
    /// <summary>
    /// Cookie Header票据授权处理
    /// 先从Cookie获取，未找到再从Header获取
    /// @ 黄振东
    /// </summary>
    public class CookieHeaderTokenAuthHandler : CookieTokenAuthHandler
    {
        /// <summary>
        /// 获取票据
        /// </summary>
        /// <returns>票据</returns>
        public override string GetToken()
        {
            var token = base.GetToken();
            if (string.IsNullOrWhiteSpace(token))
            {
                return AuthUtil.GetBearerOriginalToken(context.Request.Headers[AuthUtil.AUTH_KEY]);
            }

            return token;
        }
    }
}
