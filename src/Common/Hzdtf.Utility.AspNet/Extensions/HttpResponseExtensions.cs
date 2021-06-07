using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// Http响应扩展类
    /// @ 黄振东
    /// </summary>
    public static class HttpResponseExtensions
    {
        /// <summary>
        /// 为跨域Cookie添加必要的头
        /// </summary>
        /// <param name="response">http响应</param>
        public static void AddCorsCookieHeader(this HttpResponse response)
        {
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Allow-Methods", "*");
            response.Headers.Add("Access-Control-Allow-Credentials", "true");
            response.Headers.Add("Access-Control-Allow-Headers", "X-Requested-With");
        }
    }
}
