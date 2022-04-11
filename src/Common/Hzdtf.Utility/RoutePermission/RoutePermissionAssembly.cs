using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hzdtf.Utility.Utils;
using System.Reflection;
using static Hzdtf.Utility.RoutePermission.RoutePermissionInfo;

namespace Hzdtf.Utility.RoutePermission
{
    /// <summary>
    /// 路由权限程序集
    /// 通过传入程序集数组，自动扫描带有RoutePermission和ActionPermission特性加入到路由权限控制数据里
    /// @ 黄振东
    /// </summary>
    public class RoutePermissionAssembly : IRoutePermissionConfigReader
    {
        /// <summary>
        /// 路由权限信息数组
        /// </summary>
        private readonly RoutePermissionInfo[] routePermissions;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="assembly">程序集</param>
        public RoutePermissionAssembly(params string[] assembly)
        {
            var asses = ReflectExtensions.Load(assembly);
            if (asses.IsNullOrLength0())
            {
                return;
            }

            // 先找到具有路由权限特性的类，再找具有动作特性的方法
            var routePerList = new List<RoutePermissionInfo>(50);
            foreach (var ass in asses)
            {
                var types = ass.GetTypes().Where(p => p.IsPublic && !p.IsAbstract && p.IsClass).ToArray();
                if (types.IsNullOrLength0())
                {
                    continue;
                }

                foreach (var classes in types)
                {
                    var routePer = classes.GetAttribute<RoutePermissionAttribute>();
                    if (routePer == null)
                    {
                        continue;
                    }

                    var routeData = new RoutePermissionInfo()
                    {
                        Id = routePer.Id,
                        Code = routePer.Code,
                        Disabled = routePer.Disabled,
                        Extend = routePer.Extend,
                    };
                    // 如果以Controller结尾，应过滤掉
                    var conLastIndex = classes.Name.LastIndexOf("Controller");
                    if (conLastIndex == -1)
                    {
                        routeData.Controller = classes.Name;
                    }
                    else
                    {
                        routeData.Controller = classes.Name.Substring(0, conLastIndex);
                    }
                    routePerList.Add(routeData);

                    var methods = classes.GetMethods().Where(p => p.IsPublic && !p.IsStatic).ToArray();
                    if (methods.IsNullOrLength0())
                    {
                        continue;
                    }

                    var actionPerList = new List<ActionInfo>(methods.Length);
                    foreach (var method in methods)
                    {
                        var actionPer = method.GetAttribute<ActionPermissionAttribute>();
                        if (actionPer == null)
                        {
                            continue;
                        }

                        var actionData = new ActionInfo()
                        {
                            Id = routePer.Id,
                            Codes = actionPer.Codes,
                            Disabled = actionPer.Disabled,
                            Extend = actionPer.Extend,
                            ResourceKey = actionPer.ResourceKey,
                            Action = method.Name
                        };
                        actionPerList.Add(actionData);
                    }
                    if (actionPerList.Count == 0)
                    {
                        continue;
                    }
                    routeData.Actions = actionPerList.ToArray();
                }
            }
            if (routePerList.Count == 0)
            {
                return;
            }

            routePermissions = routePerList.ToArray();
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public RoutePermissionInfo[] Reader()
        {
            return routePermissions;
        }
    }
}
