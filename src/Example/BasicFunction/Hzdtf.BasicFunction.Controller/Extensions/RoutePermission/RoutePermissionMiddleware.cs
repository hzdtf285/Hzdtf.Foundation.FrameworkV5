using Hzdtf.BasicFunction.Service.Contract;
using Hzdtf.Utility.ApiPermission;
using Hzdtf.Utility.AspNet.Extensions.RoutePermission;
using Hzdtf.Utility.Data;
using Hzdtf.Utility.Factory;
using Hzdtf.Utility.Localization;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using Hzdtf.Utility.RequestResource;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Hzdtf.Utility.ApiPermission.RoutePermissionInfo;

namespace Hzdtf.BasicFunction.Controller.Extensions.RoutePermission
{
    /// <summary>
    /// 路由权限中间件
    /// @ 黄振东
    /// </summary>
    public class RoutePermissionMiddleware : RoutePermissionMiddlewareBase
    {
        /// <summary>
        /// 用户服务
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// 通用数据工厂
        /// </summary>
        private readonly ISimpleFactory<HttpContext, CommonUseData> comDataFactory;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="next">下一个中间件处理委托</param>
        /// <param name="options">路由权限选项配置</param>
        /// <param name="reader">读取API权限配置</param>
        /// <param name="userService">用户服务</param>
        /// <param name="localize">本地化</param>
        /// <param name="comDataFactory">通用数据工厂</param>
        public RoutePermissionMiddleware(RequestDelegate next, IOptions<RoutePermissionOptions> options, IReader<RoutePermissionInfo[]> reader,
            IUserService userService, ILocalization localize,
            ISimpleFactory<HttpContext, CommonUseData> comDataFactory = null)
            : base(next, options, reader, localize)
        {
            this.userService = userService;
            this.comDataFactory = comDataFactory;
        }

        /// <summary>
        /// 判断是否拥有权限
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="controller">控制器信息</param>
        /// <param name="action">动作信息</param>
        /// <param name="basicReturn">基本返回</param>
        /// <returns>获取是否拥有权限</returns>
        protected override bool IsHavePermission(HttpContext context, RoutePermissionInfo controller, ActionInfo action, BasicReturnInfo basicReturn)
        {
            if (string.IsNullOrWhiteSpace(controller.Code) || string.IsNullOrWhiteSpace(action.Code))
            {
                return true;
            }
            ReturnInfo<bool> perReInfo = userService.IsCurrUserPermission(controller.Code, action.Code, comData: context.CreateCommonUseData(comDataFactory, key: action.ResourceKey, menuCode: controller.Code, functionCode: action.Code));
            if (perReInfo.Failure())
            {
                basicReturn.FromBasic(perReInfo);
                return false;
            }

            return perReInfo.Data;
        }
    }
}
