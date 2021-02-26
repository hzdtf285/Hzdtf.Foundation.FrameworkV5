using Hzdtf.Utility.TheOperation;
using Hzdtf.Utility.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Hzdtf.Utility.AspNet.Extensions.TheReuestOperation
{
    /// <summary>
    /// 本次请求操作中间件
    /// @ 黄振东
    /// </summary>
    public class TheReuestOperationMiddleware
    {
        /// <summary>
        /// 下一个中间件处理委托
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// 本次操作
        /// </summary>
        private readonly ITheOperation theOperation;

        /// <summary>
        /// 本次请求操作选项配置
        /// </summary>
        private readonly TheReuestOperationOptions options;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="next">下一个中间件处理委托</param>
        /// <param name="theOperation">本次操作</param>
        /// <param name="options">本次请求操作选项配置</param>
        public TheReuestOperationMiddleware(RequestDelegate next, ITheOperation theOperation, IOptions<TheReuestOperationOptions> options)
        {
            this.next = next;
            this.theOperation = theOperation;
            this.options = options.Value;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context">http上下文</param>
        /// <returns>任务</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value.ToLower();
            if (path.StartsWith(options.PfxApiPath))
            {
                var request = context.Request;
                string eventId;
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
                theOperation.EventId = eventId;
            }

            await next(context);
        }
    }

    /// <summary>
    /// 本次操作选项配置
    /// @ 黄振东
    /// </summary>
    public class TheReuestOperationOptions
    {
        /// <summary>
        /// Api路径前辍，默认是/api/
        /// </summary>
        public string PfxApiPath
        {
            get;
            set;
        } = "/api/";
    }
}
