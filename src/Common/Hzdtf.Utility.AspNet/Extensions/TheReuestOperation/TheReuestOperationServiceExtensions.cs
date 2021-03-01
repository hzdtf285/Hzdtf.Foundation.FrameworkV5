using Hzdtf.Utility.AspNet.Extensions.TheReuestOperation;
using Hzdtf.Utility.TheOperation;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 本次请求操作服务扩展类
    /// @ 黄振东
    /// </summary>
    public static class TheReuestOperationServiceExtensions
    {
        /// <summary>
        /// 添加本次请求操作服务
        /// </summary>
        /// <param name="services">服务收藏</param>
        /// <param name="options">Api异常处理选项配置</param>
        /// <returns>服务收藏</returns>
        public static IServiceCollection AddTheReuestOperation(this IServiceCollection services, Action<TheReuestOperationOptions> options = null)
        {
            var opt = new TheReuestOperationOptions();
            if (options != null)
            {
                options(opt);
            }

            services.AddSingleton<ITheOperation, TheOperation>();
            services.AddSingleton<IOptions<TheReuestOperationOptions>>(provider =>
            {                
                return Microsoft.Extensions.Options.Options.Create<TheReuestOperationOptions>(opt);
            });

            return services;
        }


    }
}
