using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace Hzdtf.Utility.Localization
{
    /// <summary>
    /// 本地化辅助类
    /// @ 黄振东
    /// </summary>
    public static class LocalizationUtil
    {
        /// <summary>
        /// 设置当前语言文化
        /// </summary>
        /// <param name="culture">文化</param>
        public static void SetCurrentCulture(string culture)
        {
            if (string.IsNullOrWhiteSpace(culture) || Thread.CurrentThread.CurrentUICulture.Name.Equals(culture))
            {
                return;
            }

            var c = new CultureInfo(culture);

            Thread.CurrentThread.CurrentCulture = c;
            Thread.CurrentThread.CurrentUICulture = c;
        }

        /// <summary>
        /// 获取当前语言文化
        /// </summary>
        /// <returns>文化</returns>
        public static string GetCurrentCulture() => Thread.CurrentThread.CurrentUICulture.Name;
    }
}
