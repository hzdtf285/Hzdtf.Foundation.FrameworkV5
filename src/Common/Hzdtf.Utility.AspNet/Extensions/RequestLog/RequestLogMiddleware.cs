using Hzdtf.Logger.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.AspNet.Extensions.RequestLog
{
    /// <summary>
    /// 请求日志中间件
    /// @ 黄振东
    /// </summary>
    public class RequestLogMiddleware
    {
        /// <summary>
        /// 下一个中间件处理委托
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// 请求日志选项配置
        /// </summary>
        private readonly RequestLogOptions options;

        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogable log;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="next">下一个中间件处理委托</param>
        /// <param name="options">请求日志选项配置</param>
        /// <param name="log">日志</param>
        public RequestLogMiddleware(RequestDelegate next, IOptions<RequestLogOptions> options, ILogable log)
        {
            this.next = next;
            this.options = options.Value;
            this.log = log;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context">http上下文</param>
        /// <returns>任务</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var stop = new Stopwatch();
            stop.Start();
            var path = context.Request.Path.Value.ToLower();
            await next(context);
            stop.Stop();

            var routeValue = context.Request.RouteValues;
            var routes = routeValue.GetControllerAction();
            var msg = new StringBuilder($"请求:{path} method:{context.Request.Method} ");
            string controller = null, action = null;
            if (routes != null && routes.Length == 2)
            {
                controller = routes[0];
                action = routes[1];
                msg.AppendFormat("controller:{0},action:{1}.", controller, action);
            }

            msg.Append($"耗时:{stop.ElapsedMilliseconds}ms");
            var msgStr = msg.ToString();
            switch (options.LogLevel)
            {
                case LogLevelEnum.TRACE:
                    _ = log.TraceAsync(msgStr, null, "RequestLogMiddleware", path, controller, action);

                    break;

                case LogLevelEnum.DEBUG:
                    _ = log.DebugAsync(msgStr, null, "RequestLogMiddleware", path, controller, action);

                    break;

                case LogLevelEnum.WRAN:
                    _ = log.WranAsync(msgStr, null, "RequestLogMiddleware", path, controller, action);

                    break;

                case LogLevelEnum.INFO:
                    _ = log.InfoAsync(msgStr, null, "RequestLogMiddleware", path, controller, action);

                    break;

                case LogLevelEnum.ERROR:
                    _ = log.ErrorAsync(msgStr, null, "RequestLogMiddleware", path, controller, action);

                    break;

                case LogLevelEnum.FATAL:
                    _ = log.FatalAsync(msgStr, null, "RequestLogMiddleware", path, controller, action);

                    break;
            }
        }
    }

    /// <summary>
    /// 请求日志选项
    /// @ 黄振东
    /// </summary>
    public class RequestLogOptions
    {
        /// <summary>
        /// 日志等级，默认是Trace
        /// </summary>
        public LogLevelEnum LogLevel
        {
            get;
            set;
        } = LogLevelEnum.TRACE;
    }
}
