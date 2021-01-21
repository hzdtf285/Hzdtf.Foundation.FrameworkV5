using Hzdtf.Utility.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// HTTP请求扩展类
    /// @ 黄振东
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// 从请求头里获取原始的票据
        /// </summary>
        /// <param name="request">http请求</param>
        /// <returns>原始的票据</returns>
        public static string GetBearerOriginTokenFromHeader(this HttpRequest request)
        {
            var containerBearerToken = request.GetContainerBearerOriginTokenFromHeader();

            return containerBearerToken.GetBearerOriginalToken();
        }

        /// <summary>
        /// 从请求头里获取带有Bearer的票据
        /// </summary>
        /// <param name="request">http请求</param>
        /// <returns>带有Bearer的票据</returns>
        public static string GetContainerBearerOriginTokenFromHeader(this HttpRequest request)
        {
            if (request == null || !request.Headers.ContainsKey(AuthUtil.AUTH_KEY))
            {
                return null;
            }

            return request.Headers[AuthUtil.AUTH_KEY].ToString();
        }
    }
}
