using Hzdtf.Quartz.Extensions.Data;
using Hzdtf.Utility.Attr;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hzdtf.Utility.Utils;
using System.Reflection;

namespace Hzdtf.Quartz.Extensions.Scheduler
{
    /// <summary>
    /// 调度器包装
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class SchedulerWrap : ISchedulerWrap, IDisposable
    {
        /// <summary>
        /// 调度器工厂
        /// </summary>
        private readonly ISchedulerFactory schedulerFactory;

        /// <summary>
        /// 时钟数据工厂
        /// </summary>
        public IQuartzDataFactory QuartzDataFactory
        {
            get;
            set;
        }

        /// <summary>
        /// 调度器
        /// </summary>
        private IScheduler scheduler;

        /// <summary>
        /// 调度器
        /// </summary>
        public IScheduler Scheduler
        {
            get => scheduler;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="schedulerFactory">调度器工厂，如果为null，则使用标准工厂(StdSchedulerFactory)</param>
        public SchedulerWrap(ISchedulerFactory schedulerFactory = null)
        {
            if (schedulerFactory == null)
            {
                schedulerFactory = new StdSchedulerFactory();
            }
            this.schedulerFactory = schedulerFactory;
        }

        /// <summary>
        /// 异步启动
        /// </summary>
        /// <returns>任务</returns>
        public async Task StartAsync()
        {
            if (scheduler != null)
            {
                return;
            }
            var jobTasks = QuartzDataFactory.Create();
            if (jobTasks.IsNullOrCount0())
            {
                return;
            }

            scheduler = await schedulerFactory.GetScheduler();
            foreach (var jobTask in jobTasks)
            {
                var jobDetailData = jobTask.JobDetail;
                var triggerData = jobTask.Trigger;
                var type = ReflectExtensions.LoadType(jobDetailData.JobFullClass);
                var jobBuilder = JobBuilder.Create(type)
                    .WithIdentity(jobDetailData.Name, jobDetailData.Group)
                    .RequestRecovery()
                    .WithDescription(jobDetailData.Description);
                if (!jobDetailData.Params.IsNullOrCount0())
                {
                    foreach (var item in jobDetailData.Params)
                    {
                        jobBuilder.UsingJobData(item.Key, item.Value);
                    }
                }

                var triggerBuilder = TriggerBuilder.Create()
                    .WithCronSchedule(jobTask.Trigger.CronSchedule);
                if (!triggerData.Params.IsNullOrCount0())
                {
                    foreach (var item in triggerData.Params)
                    {
                        triggerBuilder.UsingJobData(item.Key, item.Value);
                    }
                }

                var job = jobBuilder.Build();
                var trigger = triggerBuilder.Build();

                BeforeJobTaskToSchedule(jobTask, job, trigger);

                await scheduler.ScheduleJob(job, trigger);
            }

            BeforeScheduleStart(scheduler);

            await scheduler.Start();
        }

        /// <summary>
        /// 作业任务添加到调试之前
        /// </summary>
        /// <param name="jobTask">作业任务</param>
        /// <param name="jobDetail">作业明细</param>
        /// <param name="trigger">触发器</param>
        protected virtual void BeforeJobTaskToSchedule(Model.JobTaskInfo jobTask, IJobDetail jobDetail, ITrigger trigger) { }

        /// <summary>
        /// 调度器启动之前
        /// </summary>
        /// <param name="scheduler">调度器</param>
        protected virtual void BeforeScheduleStart(IScheduler scheduler) { }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (scheduler == null)
            {
                return;
            }

            scheduler.ResumeAll().Wait();
        }
    }
}
