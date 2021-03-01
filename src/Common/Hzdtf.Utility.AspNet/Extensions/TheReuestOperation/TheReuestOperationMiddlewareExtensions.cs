using Hzdtf.Utility.AspNet.Extensions.TheReuestOperation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 本次请求操作中间件扩展类
    /// @ 黄振东
    /// </summary>
    public static class TheReuestOperationMiddlewareExtensions
    {
        /// <summary>
        /// 使用本次请求操作
        /// </summary>
        /// <param name="app">应用</param>
        /// <returns>应用</returns>
        public static IApplicationBuilder UseTheReuestOperation(this IApplicationBuilder app)
        {
            app.UseMiddleware<TheReuestOperationMiddleware>();

            return app;
        }
    }
}
