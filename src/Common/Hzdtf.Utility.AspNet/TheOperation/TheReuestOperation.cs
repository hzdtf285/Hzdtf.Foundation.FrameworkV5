﻿using Hzdtf.Utility;
using Hzdtf.Utility.Attr;
using Hzdtf.Utility.TheOperation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hzdtf.Utility.AspNet.TheOperation
{
    /// <summary>
    /// 本次请求操作
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class TheReuestOperation : ITheOperation
    {
        #region 属性与字段

        /// <summary>
        /// 上下文访问
        /// </summary>
        protected readonly IHttpContextAccessor contextAccessor;

        #endregion

        #region ITheOperation 接口

        /// <summary>
        /// 事件ID
        /// </summary>
        public virtual string EventId
        {
            get
            {
                if (contextAccessor == null || contextAccessor.HttpContext == null)
                {
                    return null;
                }

                string eventId = null;
                // 优先从http header里获取，否则返回连接ID+上下文哈希码
                var context = contextAccessor.HttpContext;
                var request = context.Request;
                if (request != null && request.Headers != null && request.Headers.ContainsKey(App.EVENT_ID_KEY))
                {
                    eventId = request.Headers[App.EVENT_ID_KEY].ToString();
                }
                if (string.IsNullOrWhiteSpace(eventId))
                {
                    return context.GetContextKey();
                }

                return eventId;
            }
        }

        #endregion

        #region 构造方法

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="contextAccessor">上下文访问</param>
        public TheReuestOperation(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        #endregion
    }
}
