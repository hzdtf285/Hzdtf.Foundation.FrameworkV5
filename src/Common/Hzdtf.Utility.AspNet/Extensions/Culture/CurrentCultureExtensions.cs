using Hzdtf.Utility.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// 当前文化扩展类
    /// @ 黄振东
    /// </summary>
    public static class CurrentCultureExtensions
    {
        /// <summary>
        /// 设置当前语言文化，使用的是Cookies存储，使用之前必须启用Cookies
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="culture">文化</param>
        /// <param name="expireDay">过期天数，默认为30天</param>
        public static void SetCurrentCulture(this HttpContext context, string culture, int expireDay = 30)
        {
            context.Response.Cookies.Append("Culture", culture, new CookieOptions()
            {
                HttpOnly = false,
                Expires = DateTimeOffset.UtcNow.AddDays(expireDay)
            });
            LocalizationUtil.SetCurrentCulture(culture);
        }

        /// <summary>
        /// 获取当前语言文化，使用的是Cookies存储，使用之前必须启用Cookies
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>文化</returns>
        public static string GetCurrentCulture(this HttpContext context)
        {
            string culture;
            if (context.Request.Cookies.TryGetValue("Culture", out culture))
            {
                if (string.IsNullOrWhiteSpace(culture))
                {
                    return LocalizationUtil.GetCurrentCulture();
                }
                else
                {
                    return culture;
                }
            }

            return LocalizationUtil.GetCurrentCulture();
        }
    }
}
