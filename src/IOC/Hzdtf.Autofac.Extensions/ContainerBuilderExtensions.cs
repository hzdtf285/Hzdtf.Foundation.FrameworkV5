using Hzdtf.Utility.Utils;
using System;
using System.Reflection;
using Autofac.Extras.DynamicProxy;
using Hzdtf.Utility.Enums;
using System.Collections.Generic;
using Hzdtf.Utility.AutoMapperExtensions;
using Hzdtf.Autofac.Extensions;

namespace Autofac
{
    /// <summary>
    /// 容器生成器扩展类
    /// @ 黄振东
    /// </summary>
    public static class ContainerBuilderExtensions
    {
        /// <summary>
        /// 统一注册服务程序集
        /// </summary>
        /// <param name="containerBuilder">容器生成器</param>
        /// <param name="param">参数</param>
        /// <param name="isExecBuilderContainer">是否执行生成容器，如果为false，则返回值为null</param>
        /// <returns>容器</returns>
        public static IContainer UnifiedRegisterAssemblys(this ContainerBuilder containerBuilder, BuilderParam param, bool isExecBuilderContainer = true)
        {
            var assemblyList = new List<Assembly>();
            foreach (var assembly in param.AssemblyServices)
            {
                Assembly[] assemblies = ReflectExtensions.Load(assembly.Names);
                if (assemblies.IsNullOrLength0())
                {
                    return null;
                }
                assemblyList.AddRange(assemblies);

                var registerBuilder = containerBuilder.RegisterAssemblyTypes(assemblies)
                       .PropertiesAutowired()
                       .AsImplementedInterfaces()
                       .Where(AutofacUtil.CanInject)
                       .AsSelf();

                if (!assembly.InterceptedTypes.IsNullOrLength0())
                {
                    foreach (Type type in assembly.InterceptedTypes)
                    {
                        containerBuilder.RegisterType(type);
                    }

                    registerBuilder.InterceptedBy(assembly.InterceptedTypes).EnableClassInterceptors();
                }

                switch (assembly.Lifecycle)
                {
                    case LifecycleType.DEPENDENCY:
                        registerBuilder.InstancePerDependency();

                        break;

                    case LifecycleType.LIFETIME_SCOPE:
                        registerBuilder.InstancePerLifetimeScope();

                        break;

                    case LifecycleType.MATCH_LIFETIME_SCOPE:
                        registerBuilder.InstancePerMatchingLifetimeScope(assembly.MatchTagNames);

                        break;

                    case LifecycleType.REQUEST:
                        registerBuilder.InstancePerRequest();

                        break;

                    case LifecycleType.SIGNLETON:
                        registerBuilder.SingleInstance();

                        break;
                }
            }

            if (param.RegisteringServiceAction != null)
            {
                param.RegisteringServiceAction();
            }

            if (isExecBuilderContainer)
            {
                AutofacTool.Container = containerBuilder.Build();
            }
            else
            {
                containerBuilder.RegisterBuildCallback(scope =>
                {
                    AutofacTool.LifetimeScope = scope;
                });
            }

            if (param.IsLoadAutoMapperConfig)
            {
                AutoMapperUtil.AutoRegisterConfig(assemblyList.ToArray());
            }

            return AutofacTool.Container;
        }

        /// <summary>
        /// 统一注册服务程序集,如果是net core 3.1以上，请在StartUp类里增加ConfigureContainer(ContainerBuilder builder)方法，在该方法内执行
        /// </summary>
        /// <param name="containerBuilder">容器生成器</param>
        /// <param name="param">参数</param>
        public static void UnifiedRegisterAssemblysForWeb(this ContainerBuilder containerBuilder, BuilderParam param)
        {
            UnifiedRegisterAssemblys(containerBuilder, new BuilderParam()
            {
                AssemblyServices = param.AssemblyServices,
                IsLoadAutoMapperConfig = param.IsLoadAutoMapperConfig,
                RegisteringServiceAction = () =>
                {
                    if (param.RegisteringServiceAction != null)
                    {
                        param.RegisteringServiceAction();
                    }
                }
            }, false);
        }
    }
}
