using Hzdtf.Utility.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hzdtf.Utility.AspNet.Extensions
{
    /// <summary>
    /// Http上下文授权票据接口
    /// @ 黄振东
    /// </summary>
    public interface IHttpContextAuthToken : IAuthToken
    {
        /// <summary>
        /// 设置Http上下文
        /// </summary>
        /// <param name="context">Http上下文</param>
        void SetHttpContext(HttpContext context);
    }
}
