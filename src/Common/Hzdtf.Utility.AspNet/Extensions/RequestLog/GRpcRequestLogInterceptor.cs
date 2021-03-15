using Hzdtf.Logger.Contract;
using Hzdtf.Utility.AspNet.Extensions.RequestLog;
using Hzdtf.Utility.Attr;
using Hzdtf.Utility.TheOperation;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Grpc.Core.Interceptors
{
    /// <summary>
    /// GRpc请求日志拦截器
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class GRpcRequestLogInterceptor : Interceptor
    {
        /// <summary>
        /// 请求日志选项配置
        /// </summary>
        private readonly RequestLogOptions options;

        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogable log;

        /// <summary>
        /// 本次操作
        /// </summary>
        private readonly ITheOperation theOperation;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="options">请求日志选项配置</param>
        /// <param name="log">日志</param>
        /// <param name="theOperation">本次操作</param>
        public GRpcRequestLogInterceptor(IOptions<RequestLogOptions> options, ILogable log, ITheOperation theOperation = null)
        {
            this.options = options.Value;
            this.log = log;
            this.theOperation = theOperation;
        }

        /// <summary>
        /// 服务端处理
        /// </summary>
        /// <typeparam name="TRequest">请求类型</typeparam>
        /// <typeparam name="TResponse">响应类型</typeparam>
        /// <param name="request">请求对象</param>
        /// <param name="context">服务回调上下文</param>
        /// <param name="continuation">服务方法</param>
        /// <returns>响应任务</returns>
        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            var stop = new Stopwatch();
            stop.Start();
            var path = context.GetHttpContext().Request.Path.Value;
            var re = continuation(request, context);
            stop.Stop();

            var msg = $"GRpc请求:{path} 耗时:{stop.ElapsedMilliseconds}ms";
            string eventId = null;
            if (theOperation != null)
            {
                if (string.IsNullOrWhiteSpace(theOperation.EventId))
                {
                    theOperation.EventId = context.GetHttpContext().Request.GetEventId();
                }
                eventId = theOperation.EventId;
            }
            switch (options.LogLevel)
            {
                case LogLevelEnum.TRACE:
                    _ = log.TraceAsync(msg, null, "GRpcRequestLogInterceptor", eventId: eventId, path);

                    break;

                case LogLevelEnum.DEBUG:
                    _ = log.DebugAsync(msg, null, "GRpcRequestLogInterceptor", eventId: eventId, path);

                    break;

                case LogLevelEnum.WRAN:
                    _ = log.WranAsync(msg, null, "GRpcRequestLogInterceptor", eventId: eventId, path);

                    break;

                case LogLevelEnum.INFO:
                    _ = log.InfoAsync(msg, null, "GRpcRequestLogInterceptor", eventId: eventId, path);

                    break;

                case LogLevelEnum.ERROR:
                    _ = log.ErrorAsync(msg, null, "GRpcRequestLogInterceptor", eventId: eventId, path);

                    break;

                case LogLevelEnum.FATAL:
                    _ = log.FatalAsync(msg, null, "GRpcRequestLogInterceptor", eventId: eventId, path);

                    break;
            }

            return re;
        }
    }
}
