﻿using Autofac;
using Hzdtf.Utility.Proxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Autofac.Extensions
{
    /// <summary>
    /// Autofac工具
    /// @ 黄振东
    /// </summary>
    public static class AutofacTool
    {
        /// <summary>
        /// 容器
        /// </summary>
        public static IContainer Container { get; set; }

        /// <summary>
        /// 生效的容器
        /// </summary>
        public static ILifetimeScope LifetimeScope { get; set; }

        /// <summary>
        /// 获取服务回调
        /// </summary>
        public static Func<Type, object> ResolveFunc;

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <returns>服务</returns>
        public static TService Resolve<TService>()
            where TService : class
        {
            if (ResolveFunc != null)
            {
                return ResolveFunc(typeof(TService)) as TService;
            }

            if (LifetimeScope != null)
            {
                try
                {
                    return LifetimeScope.Resolve<TService>();
                }
                catch (ObjectDisposedException) { }
            }

            if (Container != null)
            {
                using (var scope = Container.BeginLifetimeScope())
                { 
                    return scope.Resolve<TService>();
                }
            }

            return default(TService);
        }

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>服务</returns>
        public static object Resolve(Type type)
        {
            if (ResolveFunc != null)
            {
                return ResolveFunc(type);
            }

            if (LifetimeScope != null)
            {
                try
                {
                    return LifetimeScope.Resolve(type);
                }
                catch (ObjectDisposedException) { }
            }

            if (Container != null)
            {
                using (var scope = Container.BeginLifetimeScope())
                {
                    return scope.Resolve(type);
                }
            }

            return null;
        }

        /// <summary>
        /// 解析来自业务动态代理创建的对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>由业务动态代理创建的对象</returns>
        public static T ResolveFromBusinessDispatchProxy<T>() where T : class
        {
            return Resolve<IBusinessDispatchProxy>().Create<T>();
        }
    }
}
