using Hzdtf.Polly.Extensions;
using Hzdtf.Utility.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 策略扩展类
    /// @ 黄振东
    /// </summary>
    public static class PolicyExtensions
    {
        /// <summary>
        /// 为Htpp客户端添加断路器包装策略
        /// </summary>
        /// <param name="services">服务收藏</param>
        /// <param name="httpClientName">客户端名称</param>
        /// <param name="options">选项配置回调</param>
        /// <returns>服务收藏</returns>
        public static IServiceCollection AddHttpClientForBreakerWrapPolicy<TException>(this IServiceCollection services, string httpClientName = NetworkUtil.DEFAULT_HTTP_CLIENT_NAME, Action<BreakerWrapPolicyOptions<HttpResponseMessage>> options = null)
            where TException : Exception
        {
            var config = new BreakerWrapPolicyOptions<HttpResponseMessage>();
            config.GetResult = () =>
            {
                return new HttpResponseMessage()
                {
                    Content = new StringContent(PolicyUtil.DEFAULT_FALLBACK_RETURN_STRING)
                };
            };
            if (options != null)
            {
                options(config);
            }

            var asyncPolicy = PolicyUtil.BuilderBreakerWrapPollicyAsync<HttpResponseMessage, TException>(op =>
            {
                op.Breaker = config.Breaker;
                op.GetResult = config.GetResult;
                op.Id = config.Id;
                op.Result = config.Result;
                op.RetryNumber = config.RetryNumber;
                op.Timeout = config.Timeout;
            });
            services.AddHttpClient(httpClientName)
                .AddPolicyHandler(asyncPolicy);

            return services;
        }
    }
}