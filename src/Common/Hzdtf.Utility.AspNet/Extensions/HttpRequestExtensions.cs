using Hzdtf.Utility;
using Hzdtf.Utility.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// 获取事件ID，如果header不存在则新创建
        /// </summary>
        /// <param name="request">http请求</param>
        /// <returns>事件ID</returns>
        public static string GetEventId(this HttpRequest request)
        {
            string eventId = null;
            if (request != null && request.Headers != null && request.Headers.ContainsKey(App.EVENT_ID_KEY))
            {
                eventId = request.Headers[App.EVENT_ID_KEY].ToString();
                if (string.IsNullOrWhiteSpace(eventId))
                {
                    eventId = StringUtil.NewShortGuid();
                }
            }
            else
            {
                eventId = StringUtil.NewShortGuid();
            }

            return eventId;
        }

        /// <summary>
        /// 获取客户端请求IP
        /// </summary>
        /// <param name="request">http请求</param>
        /// <returns>客户端请求IP</returns>
        public static string GetClientRequestIP(this HttpRequest request)
        {
            var ip = request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }

            return ip;
        }
    }
}
