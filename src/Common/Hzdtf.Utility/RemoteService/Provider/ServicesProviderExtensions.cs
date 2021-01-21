﻿using Hzdtf.Utility.RemoteService.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.RemoteService.Provider
{
    /// <summary>
    /// 服务提供者扩展类
    /// @ 黄振东
    /// </summary>
    public static class ServicesProviderExtensions
    {
        /// <summary>
        /// 创建服务生成器
        /// </summary>
        /// <param name="service">服务提供者</param>
        /// <param name="config">配置回调</param>
        /// <returns>服务生成器</returns>
        public static IServicesBuilder CreateServiceBuilder(this IServicesProvider service, Action<IServicesBuilder> config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("配置回调不能为null");
            }

            var builder = new HttpServicesBuilder();
            builder.ServiceProvider = service;
            config(builder);

            return builder;
        }
    }
}
