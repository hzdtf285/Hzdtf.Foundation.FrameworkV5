using Hzdtf.Utility.LoadBalance;
using Hzdtf.Utility.Utils;
using Microsoft.AspNetCore.Hosting.Server.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 哈希+IP中间件拓展类
    /// @ 黄振东
    /// </summary>
    public static class HashIpPortLoadBalanceMiddlewareExtensions
    {
        /// <summary>
        /// 本地端口
        /// </summary>
        private static int localPort;

        /// <summary>
        /// 使用哈希+IP负载均衡
        /// </summary>
        /// <param name="app">应用生成器</param>
        /// <param name="port">端口</param>
        /// <returns>应用生成器</returns>
        public static IApplicationBuilder UseHashIpPortLoadBalance(this IApplicationBuilder app, int port = 0)
        {
            if (port == 0)
            {
                var add = app.ApplicationServices.GetService<IServerAddressesFeature>().Addresses.FirstOrDefault();
                localPort = NetworkUtil.GetPortFromDomain(NetworkUtil.FilterUrl(add));
            }
            else
            {
                localPort = port;
            }

            HashIpPortLoadBalance.GetPort = () => localPort;

            return app;
        }
    }
}
