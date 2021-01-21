using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hzdtf.Logger.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 策略中间件扩展类
    /// @ 黄振东
    /// </summary>
    public static class PolicyMiddlewareExtensions
    {
        /// <summary>
        /// 使用为Htpp客户端添加断路器包装策略
        /// </summary>
        /// <param name="app">应用生成器</param>
        /// <returns>应用生成器</returns>
        public static IApplicationBuilder UseHttpClientForBreakerWrapPolicy(this IApplicationBuilder app)
        {
            var log = app.ApplicationServices.GetService<ILogable>();
            if (log != null)
            {
                LogTool.DefaultLog = log;
            }

            return app;
        }
    }
}
