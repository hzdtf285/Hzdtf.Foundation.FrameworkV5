using Hzdtf.Logger.Contract;
using Hzdtf.Utility.Data;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.AspNet.Extensions.ExceptionHandle
{
    /// <summary>
    /// API异常处理中间件
    /// 只有对路径是从传过来的配置前辍才处理
    /// 会对下一个中间件捕获异常，如果发生异常，会返回BasicReturnInfo对象
    /// 对于BusinessException业务异常则不会记录日志，会返回BasicReturnInfo对象
    /// @ 黄振东
    /// </summary>
    public class ApiExceptionHandleMiddleware
    {
        /// <summary>
        /// 下一个中间件处理委托
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// Api异常处理选项配置
        /// </summary>
        private readonly ApiExceptionHandleOptions options;

        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogable log;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="next">下一个中间件处理委托</param>
        /// <param name="options">Api异常处理选项配置</param>
        /// <param name="log">日志</param>
        public ApiExceptionHandleMiddleware(RequestDelegate next, IOptions<ApiExceptionHandleOptions> options, ILogable log)
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
            var path = context.Request.Path.Value.ToLower();
            if (path.StartsWith(options.PfxApiPath))
            {
                try
                {
                    await next(context);
                }
                catch (BusinessException ex) // 业务异常不记录日志
                {
                    var re = new BasicReturnInfo();
                    re.SetCodeMsg(ex.Code, ex.Msg, ex.Desc);

                    await WriteReturnInfo(context, options, re);
                }
                catch (Exception ex)
                {
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
                    msg.Append("发生异常." + ex.Message);
                    _ = log.ErrorAsync(msg.ToString(), ex, "ApiExceptionHandleMiddleware", path, controller, action);

                    var returnInfo = new BasicReturnInfo()
                    {
                        Code = options.ExceptionCode,
                        Msg = options.ExceptionMsg,
                    };
                    if (options.IsDevelopment)
                    {
                        returnInfo.Ex = ex;
                        returnInfo.Desc = ex.ToString();
                    }
                    else
                    {
                        returnInfo.Desc = ex.Message;
                    }

                    await WriteReturnInfo(context, options, returnInfo);
                }
            }
            else
            {
                await next(context);
            }
        }

        /// <summary>
        /// 写入返回信息
        /// </summary>
        /// <param name="context">http上下文</param>
        /// <param name="options">Api异常处理选项配置</param>
        /// <param name="reInfo">返回信息</param>
        /// <returns>任务</returns>
        private static async Task WriteReturnInfo(HttpContext context, ApiExceptionHandleOptions options, BasicReturnInfo reInfo)
        {
            context.Response.ContentType = "application/json;charset=UTF-8";
            context.Response.StatusCode = options.HttpStatusCode;

            await context.Response.WriteAsync(options.Serialization.Serialize(reInfo));
        }
    }

    /// <summary>
    /// Api异常处理选项配置
    /// @ 黄振东
    /// </summary>
    public class ApiExceptionHandleOptions
    {
        /// <summary>
        /// 是否开发环境
        /// </summary>
        public bool IsDevelopment
        {
            get;
            set;
        }

        /// <summary>
        /// Api路径前辍，默认是/api/
        /// </summary>
        public string PfxApiPath
        {
            get;
            set;
        } = "/api/";

        /// <summary>
        /// 序列化，默认为Json
        /// </summary>
        public ISerialization Serialization
        {
            get;
            set;
        } = new JsonConvert();

        /// <summary>
        /// 异常编码，默认为500
        /// </summary>
        public int ExceptionCode
        {
            get;
            set;
        } = 500;

        /// <summary>
        /// 异常消息，默认为“操作失败”
        /// </summary>
        public string ExceptionMsg
        {
            get;
            set;
        } = "操作失败";

        /// <summary>
        /// Http状态码，默认返回200
        /// </summary>
        public int HttpStatusCode
        {
            get;
            set;
        } = StatusCodes.Status200OK;
    }
}
