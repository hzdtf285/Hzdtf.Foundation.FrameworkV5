using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hzdtf.Utility.Utils;
using System.Reflection;
using Quartz.Impl.Triggers;
using Hzdtf.Quartz.Persistence.Contract;
using Hzdtf.Quartz.Model;
using Hzdtf.Logger.Contract;
using Hzdtf.Utility.Enums;

namespace Hzdtf.Quartz.Extensions.Scheduler
{
    /// <summary>
    /// 调度器包装
    /// @ 黄振东
    /// </summary>
    public class SchedulerWrap : ISchedulerWrap, IAsyncDisposable
    {
        /// <summary>
        /// 调度器工厂
        /// </summary>
        private readonly ISchedulerFactory schedulerFactory;

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
        /// 作业任务持久化
        /// </summary>
        private readonly IJobTaskBasicPersistence persistence;

        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogable log;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="persistence">作业任务持久化</param>
        /// <param name="log">日志</param>
        /// <param name="schedulerFactory">调度器工厂，如果为null，则使用标准工厂(StdSchedulerFactory)</param>
        public SchedulerWrap(IJobTaskBasicPersistence persistence = null, ILogable log = null, ISchedulerFactory schedulerFactory = null)
        {
            if (QuartzStaticConfig.JobTaskPersistence != null)
            {
                persistence = QuartzStaticConfig.JobTaskPersistence;
            }
            if (QuartzStaticConfig.Log != null)
            {
                log = QuartzStaticConfig.Log;
            }
            this.persistence = persistence;
            if (schedulerFactory == null)
            {
                schedulerFactory = new StdSchedulerFactory();
            }
            this.schedulerFactory = schedulerFactory;
            if (log == null)
            {
                log = LogTool.DefaultLog;
            }
            this.log = log;
        }

        /// <summary>
        /// 异步启动
        /// </summary>
        /// <returns>任务</returns>
        public async Task StartAsync()
        {
            if (scheduler != null && scheduler.IsStarted)
            {
                return;
            }
            scheduler = await schedulerFactory.GetScheduler();

            // 避免出现一次性查询大量数据，使用分页查询
            var connId = persistence.NewConnectionId(AccessMode.SLAVE);
            try
            {
                PagingUtil.ForPage((pageIndex, pageSize) =>
                {
                    var page = persistence.QueryPage(pageIndex, pageSize, connectionId: connId);
                    if (page.Rows.IsNullOrCount0())
                    {
                        return page.PageCount;
                    }

                    foreach (var jobTask in page.Rows)
                    {
                        AddScheduleAsync(jobTask).Wait();
                    }

                    return page.PageCount;
                }, maxForCount: 1000000);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                persistence.Release(connId);
            }

            BeforeScheduleStart(scheduler);

            await scheduler.Start();
        }

        /// <summary>
        /// 异步暂停
        /// </summary>
        /// <returns>任务</returns>
        public async Task PauseAsync()
        {
            await scheduler.PauseAll();
        }

        /// <summary>
        /// 异步恢复
        /// </summary>
        /// <returns>任务</returns>
        public async Task ResumeAsync()
        {
            await scheduler.ResumeAll();
        }

        /// <summary>
        /// 异步停止
        /// </summary>
        /// <returns>任务</returns>
        public async Task StopAsync()
        {
            if (scheduler.IsShutdown)
            {
                return;
            }

            await scheduler.Shutdown();
        }

        /// <summary>
        /// 异步作业任务暂停
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="group">分组</param>
        /// <returns>任务</returns>
        public async Task PauseJobTaskAsync(string name, string group = null)
        {
            if (scheduler.IsShutdown)
            {
                return;
            }

            var jk = new JobKey(name, group);
            var tk = new TriggerKey(name, group);
            await scheduler.PauseTrigger(tk);
            await scheduler.PauseJob(jk);
        }

        /// <summary>
        /// 异步作业任务恢复
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="group">分组</param>
        /// <returns>任务</returns>
        public async Task ResumeJobTaskAsync(string name, string group = null)
        {
            if (scheduler.IsShutdown)
            {
                return;
            }

            var jk = new JobKey(name, group);
            var tk = new TriggerKey(name, group);
            await scheduler.ResumeJob(jk);
            await scheduler.ResumeTrigger(tk);
        }

        /// <summary>
        /// 异步作业任务停止
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="group">分组</param>
        /// <returns>任务</returns>
        public async Task StopJobTaskAsync(string name, string group = null)
        {
            if (scheduler.IsShutdown)
            {
                return;
            }

            var jk = new JobKey(name, group);
            var tk = new TriggerKey(name, group);
            await scheduler.UnscheduleJob(tk);
            await scheduler.DeleteJob(jk);
        }

        /// <summary>
        /// 异步作业任务彻底移除（包含持久化）
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="group">分组</param>
        /// <returns>任务</returns>
        public async Task CompletelyRemoveJobTaskAsync(string name, string group = null)
        {
            try
            {
                persistence.Delete(name, group);
            }
            catch (Exception ex)
            {
                await log.ErrorAsync(ex.Message, ex, this.GetType().Name, null, "CompletelyRemoveJobTaskAsync");
                return;
            }

            await StopJobTaskAsync(name, group);
        }

        /// <summary>
        /// 异步重新调度作业任务
        /// </summary>
        /// <param name="jobTask">作业任务</param>
        /// <returns>任务</returns>
        public async Task RescheduleJobTaskAsync(JobTaskInfo jobTask)
        {
            try
            {
                persistence.Set(jobTask);
            }
            catch (Exception ex)
            {
                await log.ErrorAsync(ex.Message, ex, this.GetType().Name, null, "RescheduleJobTaskAsync");
                return;
            }

            await RescheduleAsync(jobTask);
        }

        /// <summary>
        /// 异步重新调度作业任务
        /// </summary>
        /// <param name="cron">cron表达式</param>
        /// <param name="name">名称</param>
        /// <param name="group">分组</param>
        /// <returns>任务</returns>
        public async Task RescheduleJobTaskAsync(string cron, string name, string group = null)
        {
            try
            {
                persistence.UpdateCron(cron, name);
            }
            catch (Exception ex)
            {
                await log.ErrorAsync(ex.Message, ex, this.GetType().Name, null, "RescheduleJobTaskAsync");
                return;
            }

            await RescheduleAsync(cron, name, group);
        }

        /// <summary>
        /// 异步释放资源
        /// </summary>
        /// <returns>任务</returns>
        public async ValueTask DisposeAsync()
        {
            await StopAsync();
        }

        /// <summary>
        /// 作业任务添加到调试之前
        /// </summary>
        /// <param name="jobTask">作业任务</param>
        /// <param name="jobDetail">作业明细</param>
        /// <param name="trigger">触发器</param>
        protected virtual void BeforeJobTaskToSchedule(JobTaskInfo jobTask, IJobDetail jobDetail, ITrigger trigger) { }

        /// <summary>
        /// 调度器启动之前
        /// </summary>
        /// <param name="scheduler">调度器</param>
        protected virtual void BeforeScheduleStart(IScheduler scheduler) { }

        /// <summary>
        /// 异步重新调度
        /// </summary>
        /// <param name="cron">cron表达式</param>
        /// <param name="name">名称</param>
        /// <param name="group">分组</param>
        /// <returns>任务</returns>
        private async Task RescheduleAsync(string cron, string name, string group = null)
        {
            if (scheduler.IsShutdown)
            {
                return;
            }
            var tk = new TriggerKey(name, group);
            var trigger = await scheduler.GetTrigger(tk) as CronTriggerImpl;
            if (trigger == null)
            {
                return;
            }

            trigger.CronExpressionString = cron;
            await scheduler.RescheduleJob(tk, trigger);
        }

        /// <summary>
        /// 异步重新调度
        /// </summary>
        /// <param name="jobTask">作业任务</param>
        /// <returns>任务</returns>
        private async Task RescheduleAsync(JobTaskInfo jobTask)
        {
            if (scheduler.IsShutdown)
            {
                return;
            }
            var tk = new TriggerKey(jobTask.JtName, jobTask.JtGroup);
            var trigger = await scheduler.GetTrigger(tk) as CronTriggerImpl;
            // 如果不存在，则新创建调度
            if (trigger == null)
            {
                var jk = new JobKey(jobTask.JtName, jobTask.JtGroup);
                await scheduler.DeleteJob(jk);
                await AddScheduleAsync(jobTask);

                return;
            }

            trigger.CronExpressionString = jobTask.TriggerCron;
            await scheduler.RescheduleJob(tk, trigger);
        }

        /// <summary>
        /// 异步添加调度
        /// </summary>
        /// <param name="jobTask">作业任务</param>
        /// <returns>任务</returns>
        private async Task AddScheduleAsync(JobTaskInfo jobTask)
        {
            var type = ReflectExtensions.LoadType(jobTask.JobFullClass);
            var jobBuilder = JobBuilder.Create(type)
                .WithIdentity(jobTask.JtName, jobTask.JtGroup)
                .RequestRecovery()
                .WithDescription(jobTask.JtDesc);
            jobBuilder.UsingJobData(QuartzCodeDefine.JobTaskIdKey, jobTask.Id);
            jobBuilder.UsingJobData(QuartzCodeDefine.JobTaskSuccessedRemoveKey, jobTask.SuccessedRemove);
            if (!jobTask.JobParams.IsNullOrCount0())
            {
                foreach (var item in jobTask.JobParams)
                {
                    jobBuilder.UsingJobData(item.Key, item.Value);
                }
            }

            var triggerBuilder = TriggerBuilder.Create()
                .WithIdentity(jobTask.JtName, jobTask.JtGroup)
                .WithCronSchedule(jobTask.TriggerCron)
                .WithDescription(jobTask.JtDesc);
            if (!jobTask.TriggerParams.IsNullOrCount0())
            {
                foreach (var item in jobTask.TriggerParams)
                {
                    triggerBuilder.UsingJobData(item.Key, item.Value);
                }
            }

            var job = jobBuilder.Build();
            var trigger = triggerBuilder.Build();
            CronTriggerImpl s = trigger as CronTriggerImpl;
            BeforeJobTaskToSchedule(jobTask, job, trigger);
            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
