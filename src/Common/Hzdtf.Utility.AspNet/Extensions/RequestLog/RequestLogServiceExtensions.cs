using Hzdtf.Utility.AspNet.Extensions.RequestLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 请求日志扩展类
    /// @ 黄振东
    /// </summary>
    public static class RequestLogServiceExtensions
    {
        /// <summary>
        /// 添加请求日志服务
        /// </summary>
        /// <param name="services">服务收藏</param>
        /// <param name="options">请求日志选项配置</param>
        /// <returns>服务收藏</returns>
        public static IServiceCollection AddRequestLog(this IServiceCollection services, Action<RequestLogOptions> options = null)
        {
            var apiExceptionHandleOptions = new RequestLogOptions();
            if (options != null)
            {
                options(apiExceptionHandleOptions);
            }

            services.AddSingleton<Microsoft.Extensions.Options.IOptions<RequestLogOptions>>(provider =>
            {
                return Microsoft.Extensions.Options.Options.Create<RequestLogOptions>(apiExceptionHandleOptions);
            });

            return services;
        }
    }
}
