using Hzdtf.Utility;
using Hzdtf.Utility.AspNet.Extensions.Culture;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 文化中间件扩展类
    /// @ 黄振东
    /// </summary>
    public static class CultureMiddlewareExtensions
    {
        /// <summary>
        /// 使用文化
        /// </summary>
        /// <param name="app">应用</param>
        /// <param name="defaultCulture">默认文化</param>
        /// <returns>应用</returns>
        public static IApplicationBuilder UseCulture(this IApplicationBuilder app, string defaultCulture = null)
        {
            if (!string.IsNullOrWhiteSpace(defaultCulture))
            {
                App.DefaultCulture = defaultCulture;
            }

            app.UseMiddleware<CultureMiddleware>();

            return app;
        }
    }
}
