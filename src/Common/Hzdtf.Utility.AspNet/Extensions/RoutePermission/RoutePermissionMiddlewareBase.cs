using Hzdtf.Utility.ApiPermission;
using Hzdtf.Utility.Data;
using Hzdtf.Utility.Model.Return;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hzdtf.Utility.Utils;
using System.Linq;
using static Hzdtf.Utility.ApiPermission.RoutePermissionInfo;
using Microsoft.AspNetCore.Routing;
using Hzdtf.Utility.Localization;
using Hzdtf.Utility.RequestResource;

namespace Hzdtf.Utility.AspNet.Extensions.RoutePermission
{
    /// <summary>
    /// 路由权限中间件基类
    /// @ 黄振东
    /// </summary>
    public abstract class RoutePermissionMiddlewareBase
    {
        /// <summary>
        /// 下一个中间件处理委托
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// 读取API权限配置
        /// </summary>
        private readonly IReader<RoutePermissionInfo[]> reader;

        /// <summary>
        /// API权限选项配置
        /// </summary>
        private readonly RoutePermissionOptions options;

        /// <summary>
        /// 本地化
        /// </summary>
        private readonly ILocalization localize;

        /// <summary>
        /// 请求资源
        /// </summary>
        private readonly IRequestResource requestSource;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="next">下一个中间件处理委托</param>
        /// <param name="options">路由权限选项配置</param>
        /// <param name="reader">读取API权限配置</param>
        /// <param name="localize">本地化</param>
        /// <param name="requestResource">请求资源</param>
        public RoutePermissionMiddlewareBase(RequestDelegate next, IOptions<RoutePermissionOptions> options, 
            IReader<RoutePermissionInfo[]> reader, ILocalization localize, IRequestResource requestResource)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("读API权限配置[IReader<ApiPermissionInfo[]>]不能为空");
            }

            this.next = next;
            this.options = options.Value;
            this.reader = reader;
            this.localize = localize;
            this.requestSource = requestResource;
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
                var routeValue = context.Request.RouteValues;
                var routes = routeValue.GetControllerAction();
                if (routes.IsNullOrLength0())
                {
                    await next(context);
                    return;
                }

                var routePermisses = reader.Reader();
                if (routePermisses.IsNullOrLength0())
                {
                    await next(context);
                    return;
                }

                var controllerConfig = routePermisses.Where(p => p.Controller == routes[0]).FirstOrDefault();
                if (controllerConfig == null)
                {
                    await next(context);
                    return;
                }
                if (controllerConfig.Disabled)
                {
                    var tempReturn = new BasicReturnInfo();
                    tempReturn.SetFailureMsg(localize.Get(CommonCodeDefine.DISABLED_ACCESS_CULTURE_KEY, "此功能已禁用"));
                    await WriteContent(context, tempReturn);

                    return;
                }
                if (controllerConfig.Actions.IsNullOrLength0())
                {
                    await next(context);
                    return;
                }

                var actionConfig = controllerConfig.Actions.Where(p => p.Action == routes[1]).FirstOrDefault();
                if (actionConfig == null)
                {
                    await next(context);
                    return;
                }
                if (actionConfig.Disabled)
                {
                    var tempReturn = new BasicReturnInfo();
                    tempReturn.SetFailureMsg(localize.Get(CommonCodeDefine.DISABLED_ACCESS_CULTURE_KEY, "此功能已禁用"));
                    await WriteContent(context, tempReturn);

                    return;
                }

                var basicReturn = new BasicReturnInfo();
                var isPer = IsHavePermission(controllerConfig, actionConfig, basicReturn);
                if (basicReturn.Failure())
                {
                    await WriteContent(context, basicReturn);
                    return;
                }
                if (isPer)
                {
                    string requestKey = null;
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(actionConfig.ResourceKey))
                        {
                            requestKey = context.GetContextKey();
                            requestSource.Add(requestKey, actionConfig.ResourceKey);
                        }
                        await next(context);
                        return;
                    }
                    finally
                    {
                        if (requestKey != null)
                        {
                            requestSource.Remove(requestKey);
                        }
                    }
                }
                else
                {
                    await NotPermissionHandle(context);
                }
            }
            else
            {
                await next(context);
            }
        }

        /// <summary>
        /// 判断是否拥有权限
        /// </summary>
        /// <param name="controller">控制器信息</param>
        /// <param name="action">动作信息</param>
        /// <param name="basicReturn">基本返回</param>
        /// <returns>获取是否拥有权限</returns>
        protected abstract bool IsHavePermission(RoutePermissionInfo controller, ActionInfo action, BasicReturnInfo basicReturn);

        /// <summary>
        /// 无权限处理
        /// </summary>
        /// <param name="context">http上下文</param>
        /// <returns>任务</returns>
        protected virtual async Task NotPermissionHandle(HttpContext context)
        {
            var basicReturn = new BasicReturnInfo();
            basicReturn.SetCodeMsg(CommonCodeDefine.NOT_PERMISSION, localize.Get(CommonCodeDefine.NOT_PERMISSION_CULTURE_KEY, "对不起，您没有权限"));

            await WriteContent(context, basicReturn);
        }

        /// <summary>
        /// 写入内容
        /// </summary>
        /// <param name="context">http上下文</param>
        /// <param name="basicReturn">基本返回</param>
        /// <returns>任务</returns>
        protected async Task WriteContent(HttpContext context, BasicReturnInfo basicReturn)
        {
            context.Response.ContentType = "application/json;charset=UTF-8";
            await context.Response.WriteAsync(basicReturn.ToJsonString());
        }
    }

    /// <summary>
    /// 路由权限选项配置
    /// @ 黄振东
    /// </summary>
    public class RoutePermissionOptions
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
