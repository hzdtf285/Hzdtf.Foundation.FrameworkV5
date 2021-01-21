using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 哈希+IP负载均衡扩展类
    /// @ 黄振东
    /// </summary>
    public static class HashIpPortLoadBalanceExtensions
    {
        /// <summary>
        /// 添加哈希+IP负载均衡
        /// </summary>
        /// <param name="services">服务</param>
        /// <returns>服务</returns>
        public static IServiceCollection AddHashIpPortLoadBalance(this IServiceCollection services)
        {
            // 添加一个能获取本服务地址的服务
            services.AddSingleton(serviceProvider =>
            {
                var server = serviceProvider.GetRequiredService<IServer>();
                return server.Features.Get<IServerAddressesFeature>();
            });

            return services;
        }
    }
}