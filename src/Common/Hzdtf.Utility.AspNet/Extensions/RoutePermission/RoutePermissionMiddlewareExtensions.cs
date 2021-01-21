using Hzdtf.Utility.AspNet.Extensions.RoutePermission;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 路由权限中间件扩展类
    /// @ 黄振东
    /// </summary>
    public static class RoutePermissionMiddlewareExtensions
    {
        /// <summary>
        /// 使用路由权限，必须在UseRouting中间件后面执行
        /// </summary>
        /// <param name="app">应用</param>
        /// <returns>应用</returns>
        public static IApplicationBuilder UseRoutePermission<MiddleT>(this IApplicationBuilder app)
            where MiddleT : RoutePermissionMiddlewareBase
        {
            app.UseMiddleware<MiddleT>();

            return app;
        }
    }
}
