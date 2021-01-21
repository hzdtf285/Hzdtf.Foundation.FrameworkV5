using Hzdtf.Utility.AspNet.Extensions.RequestLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 请求日志中间件扩展类
    /// @ 黄振东
    /// </summary>
    public static class RequestLogMiddlewareExtensions
    {
        /// <summary>
        /// 使用请求日志
        /// </summary>
        /// <param name="app">应用</param>
        /// <returns>应用</returns>
        public static IApplicationBuilder UseRequestLog(this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestLogMiddleware>();

            return app;
        }
    }
}
