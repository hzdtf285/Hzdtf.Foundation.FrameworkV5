﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Hzdtf.Utility.AspNet
{
    /// <summary>
    /// Web应用类
    /// @ 黄振东
    /// </summary>
    public static class WebApp
    {
        /// <summary>
        /// 当前服务提供者实例
        /// </summary>
        public static IServiceProvider Instance
        {
            get;
            set;
        }

        /// <summary>
        /// 从当前服务提供者实例获取服务
        /// </summary>
        /// <typeparam name="T">服务类型</typeparam>
        /// <returns>服务</returns>
        public static T GetServiceFromInstance<T>() => Instance != null ? Instance.GetService<T>() : default(T);
    }
}
