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
        /// 设置当前语言文化，使用的是Session存储，使用之前必须启用Session
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="culture">文化</param>
        public static void SetCurrentCulture(this HttpContext context, string culture)
        {
            context.Session.SetString("Culture", culture);
            LocalizationUtil.SetCurrentCulture(culture);
        }

        /// <summary>
        /// 获取当前语言文化，使用的是Session存储，使用之前必须启用Session
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>文化</returns>
        public static string GetCurrentCulture(this HttpContext context)
        {
            var culture = context.Session.GetString("Culture");
            if (string.IsNullOrWhiteSpace(culture))
            {
                return LocalizationUtil.GetCurrentCulture();
            }

            return culture;
        }
    }
}
