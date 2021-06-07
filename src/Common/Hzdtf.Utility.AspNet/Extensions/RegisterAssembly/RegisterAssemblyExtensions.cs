using Hzdtf.Utility.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 注册程序集扩展类
    /// @ 黄振东
    /// </summary>
    public static class RegisterAssemblyExtensions
    {
        /// <summary>
        /// 用DI批量注入接口程序集中对应的实现类
        /// </summary>
        /// <param name="service">服务收藏</param>
        /// <param name="interfaceAssemblyName">接口程序集的名称（不包含文件扩展名）</param>
        /// <param name="implementAssemblyName">实现程序集的名称（不包含文件扩展名）</param>
        /// <param name="lifecycle">生命周期，默认为瞬时</param>
        /// <param name="interfacTypeCallback">接口类型回调，key：接口类型；value：是否忽略</param>
        /// <returns>服务收藏</returns>
        public static IServiceCollection RegisterAssembly(this IServiceCollection service, string interfaceAssemblyName, string implementAssemblyName, 
            ServiceLifetime lifecycle = ServiceLifetime.Transient, Func<Type, bool> interfacTypeCallback = null)
        {            
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }
            if (string.IsNullOrEmpty(interfaceAssemblyName))
            {
                throw new ArgumentNullException(nameof(interfaceAssemblyName));
            }
            if (string.IsNullOrEmpty(implementAssemblyName))
            {
                throw new ArgumentNullException(nameof(implementAssemblyName));
            }

            var interfaceAssembly = Assembly.Load(interfaceAssemblyName);
            if (interfaceAssembly == null)
            {
                throw new DllNotFoundException($"the dll \"{interfaceAssemblyName}\" not be found");
            }

            var implementAssembly = Assembly.Load(implementAssemblyName);
            if (implementAssembly == null)
            {
                throw new DllNotFoundException($"the dll \"{implementAssemblyName}\" not be found");
            }

            //过滤掉非接口
            var types = interfaceAssembly.GetTypes().Where(t => t.GetTypeInfo().IsInterface);

            foreach (var type in types)
            {
                if (interfacTypeCallback != null && interfacTypeCallback(type))
                {
                    continue;
                }

                //过滤掉抽象类、以及非class
                var implementType = implementAssembly.DefinedTypes
                   .FirstOrDefault(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Any(b => b.Name == type.Name));
                if (implementType != null)
                {
                    switch (lifecycle)
                    {
                        case ServiceLifetime.Transient:
                            service.AddTransient(type, implementType.AsType());

                            break;

                        case ServiceLifetime.Scoped:
                            service.AddScoped(type, implementType.AsType());

                            break;

                        case ServiceLifetime.Singleton:
                            service.AddSingleton(type, implementType.AsType());

                            break;

                        default:
                            throw new NotSupportedException($"不支持的生命周期:{lifecycle}");
                    }
                }
            }

            return service;
        }

        /// <summary>
        /// 用DI批量注入批定接口类型的实现类程序集中的实现类
        /// </summary>
        /// <param name="service">服务收藏</param>
        /// <param name="interfaceType">接口类型</param>
        /// <param name="lifecycle">生命周期，默认为瞬时</param>
        /// <param name="implClassAssemblys">接口程序集的名称（不包含文件扩展名）</param>
        /// <returns>服务收藏</returns>
        public static IServiceCollection RegisterAssemblyWithInterfaceMapImpls(this IServiceCollection service, Type interfaceType,
            ServiceLifetime lifecycle = ServiceLifetime.Transient,  params string[] implClassAssemblys)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }
            if (implClassAssemblys.IsNullOrLength0())
            {
                throw new ArgumentNullException(nameof(implClassAssemblys));
            }

            Assembly[] assemblies = new Assembly[implClassAssemblys.Length];
            for (var i = 0; i < implClassAssemblys.Length; i++)
            {
                assemblies[i] = Assembly.Load(implClassAssemblys[i]);
            }

            var implTypes = ReflectExtensions.GetImplClassType(assemblies, interfaceType);
            if (implTypes.IsNullOrLength0())
            {
                return service;
            }

            foreach (var it in implTypes)
            {
                if (it.IsAbstract)
                {
                    continue;
                }

                switch (lifecycle)
                {
                    case ServiceLifetime.Transient:
                        service.AddTransient(interfaceType, it);

                        break;

                    case ServiceLifetime.Scoped:
                        service.AddScoped(interfaceType, it);

                        break;

                    case ServiceLifetime.Singleton:
                        service.AddSingleton(interfaceType, it);

                        break;

                    default:
                        throw new NotSupportedException($"不支持的生命周期:{lifecycle}");
                }
            }

            return service;
        }
    }
}
