using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// http上下文扩展类
    /// @ 黄振东
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// 获取上下文键
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>上下文键</returns>
        public static string GetContextKey(this HttpContext context)
        {
            if (context.Connection == null)
            {
                return context.GetHashCode().ToString();
            }

            return $"{context.Connection.Id}_{context.GetHashCode()}";
        }
    }
}
