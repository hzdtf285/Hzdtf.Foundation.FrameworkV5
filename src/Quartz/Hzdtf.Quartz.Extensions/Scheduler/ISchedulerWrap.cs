using Hzdtf.Quartz.Model;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Quartz.Extensions.Scheduler
{
    /// <summary>
    /// 调度器包装接口
    /// @ 黄振东
    /// </summary>
    public interface ISchedulerWrap
    {
        /// <summary>
        /// 调度器
        /// </summary>
        IScheduler Scheduler
        {
            get;
        }

        /// <summary>
        /// 异步启动
        /// </summary>
        /// <returns>任务</returns>
        Task StartAsync();

        /// <summary>
        /// 异步暂停
        /// </summary>
        /// <returns>任务</returns>
        Task PauseAsync();

        /// <summary>
        /// 异步恢复
        /// </summary>
        /// <returns>任务</returns>
        Task ResumeAsync();

        /// <summary>
        /// 异步停止
        /// </summary>
        /// <returns>任务</returns>
        Task StopAsync();

        /// <summary>
        /// 异步作业任务暂停
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="group">分组</param>
        /// <returns>任务</returns>
        Task PauseJobTaskAsync(string name, string group = null);

        /// <summary>
        /// 异步作业任务恢复
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="group">分组</param>
        /// <returns>任务</returns>
        Task ResumeJobTaskAsync(string name, string group = null);

        /// <summary>
        /// 异步作业任务停止
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="group">分组</param>
        /// <returns>任务</returns>
        Task StopJobTaskAsync(string name, string group = null);

        /// <summary>
        /// 异步作业任务彻底移除（包含持久化）
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="group">分组</param>
        /// <returns>任务</returns>
        Task CompletelyRemoveJobTaskAsync(string name, string group = null);

        /// <summary>
        /// 异步重新调度作业任务
        /// </summary>
        /// <param name="jobTask">作业任务</param>
        /// <returns>任务</returns>
        Task RescheduleJobTaskAsync(JobTaskInfo jobTask);

        /// <summary>
        /// 异步重新调度作业任务
        /// </summary>
        /// <param name="cron">cron表达式</param>
        /// <param name="name">名称</param>
        /// <param name="group">分组</param>
        /// <returns>任务</returns>
        Task RescheduleJobTaskAsync(string cron, string name, string group = null);
    }
}
