using Hzdtf.Logger.Contract;
using Hzdtf.Quartz.AspNet.Extensions;
using Hzdtf.Quartz.Extensions;
using Hzdtf.Quartz.Extensions.Data;
using Hzdtf.Quartz.Extensions.Model;
using Hzdtf.Quartz.Extensions.Scheduler;
using Hzdtf.Utility.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 时钟扩展类
    /// @ 黄振东
    /// </summary>
    public static class QuartzExtensions
    {
        /// <summary>
        /// 添加时钟
        /// </summary>
        /// <param name="services">服务收藏</param>
        /// <param name="options">配置回调</param>
        /// <returns>服务收藏</returns>
        public static IServiceCollection AddQuartz(this IServiceCollection services, Action<QuartzConfig> options = null)
        {
            var op = new QuartzConfig();
            if (options != null)
            {
                options(op);
            }

            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            if (op.JobDetailReader == null)
            {
                services.AddSingleton<IReaderAll<JobDetailInfo>, JobDetailDataJson>();
            }
            else
            {
                services.AddSingleton<IReaderAll<JobDetailInfo>>(op.JobDetailReader);
            }
            if (op.JobTaskReader == null)
            {
                services.AddSingleton<IReaderAll<JobTaskInfo>, JobTaskDataJson>();
            }
            else
            {
                services.AddSingleton<IReaderAll<JobTaskInfo>>(op.JobTaskReader);
            }
            if (op.TriggerReader == null)
            {
                services.AddSingleton<IReaderAll<TriggerInfo>, TriggerDataJson>();
            }
            else
            {
                services.AddSingleton<IReaderAll<TriggerInfo>>(op.TriggerReader);
            }
            if (op.QuartzDataFactory == null)
            {
                services.AddSingleton<IQuartzDataFactory, QuartzDataFactory>();
            }
            else
            {
                services.AddSingleton<IQuartzDataFactory>(op.QuartzDataFactory);
            }
            if (op.SchedulerWrap == null)
            {
                services.AddSingleton<ISchedulerWrap, SchedulerWrap>();
            }
            else
            {
                services.AddSingleton<ISchedulerWrap>(op.SchedulerWrap);
            }

            if (op.JobHandleException != null)
            {
                QuartzStaticConfig.JobHandleException = op.JobHandleException;
            }
            else if (!string.IsNullOrWhiteSpace(op.JobHandleExceptionAssembly))
            {
                services.RegisterAssemblyWithInterfaceMapImpls(typeof(IJobHandleException), ServiceLifetime.Singleton, op.JobHandleExceptionAssembly);
            }

            return services;
        }

        /// <summary>
        /// 使用时钟
        /// </summary>
        /// <param name="builder">应用生成</param>
        /// <param name="service">服务提供者</param>
        /// <returns>应用生成</returns>
        public static IApplicationBuilder UseQuartz(this IApplicationBuilder builder, IServiceProvider service)
        {
            if (QuartzStaticConfig.JobHandleException == null)
            {
                QuartzStaticConfig.JobHandleException = service.GetService<IJobHandleException>();
            }
            var config = service.GetService<IConfiguration>();
            if (config != null)
            {
                QuartzStaticConfig.Config = config;
            }
            var log = service.GetService<ILogable>();
            if (log != null)
            {
                QuartzStaticConfig.Log = log;
            }

            return builder;
        }
    }
}
