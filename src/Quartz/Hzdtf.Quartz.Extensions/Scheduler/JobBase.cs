using Hzdtf.Logger.Contract;
using Hzdtf.Quartz.Model;
using Hzdtf.Utility;
using Hzdtf.Utility.Model.Identitys;
using Microsoft.Extensions.Configuration;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Quartz.Extensions.Scheduler
{
    /// <summary>
    /// 作业基类
    /// @ 黄振东
    /// </summary>
    public abstract class JobBase : IJob
    {
        /// <summary>
        /// 日志
        /// </summary>
        protected ILogable Log
        {
            get => QuartzStaticConfig.Log;
        }

        /// <summary>
        /// 配置
        /// </summary>
        protected IConfiguration Config
        {
            get => QuartzStaticConfig.Config;
        }

        /// <summary>
        /// ID标识
        /// </summary>
        private readonly static IIdentity<long> identity = new SnowflakeId();

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context">作业执行上下文</param>
        /// <returns>任务</returns>
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() =>
            {
                var transId = identity.New();
                var transIdStr = transId.ToString();
                var thisClass = this.GetType().Name;
                var name = context.JobDetail.Key.Name;
                var group = context.JobDetail.Key.Group;
                var idMsg = $"事务Id[{transId}],类名[{thisClass}],作业组[{group}],作业名[{name}]";
                try
                {
                    if (QuartzStaticConfig.IsTraceLog)
                    {
                        Log.TraceAsync($"{idMsg}:正在执行作业...", null, thisClass, null, transIdStr, name, group);
                    }

                    ExecBusinessHandle(context, transId);

                    // 如果设置了执行成功后移除任务
                    var successRemove = context.JobDetail.JobDataMap.GetBoolean(QuartzCodeDefine.JobTaskSuccessedRemoveKey);
                    if (successRemove)
                    {
                        ISchedulerWrap wrap = null;
                        if (QuartzStaticConfig.SchedulerWrap == null)
                        {
                            if (App.Instance == null)
                            {
                                throw new ArgumentNullException("请设置App.Instance");
                            }
                            wrap = App.GetServiceFromInstance<ISchedulerWrap>();
                        }
                        else
                        {
                            wrap = QuartzStaticConfig.SchedulerWrap;
                        }

                        wrap.CompletelyRemoveJobTaskAsync(name, group);
                    }
                }
                catch (Exception ex)
                {
                    Log.ErrorAsync($"执行作业发生异常.{idMsg}", ex, thisClass, null, transIdStr, name, group, "Execute");
                    if (QuartzStaticConfig.JobHandleException == null)
                    {
                        return;
                    }

                    QuartzStaticConfig.JobHandleException.Notify(context, this, new JobExceptionInfo()
                    {
                        Ex = ex,
                        ExMsg = ex.Message,
                        ExStackTrace = ex.StackTrace,
                        TransId = transId,
                        JobName = name,
                        JobGroup = group,
                        JobFullClass = this.GetType().FullName,
                        ServiceName = App.AppServiceName
                    });
                }
            });
        }

        /// <summary>
        /// 执行业务处理
        /// </summary>
        /// <param name="context">作业执行上下文</param>
        /// <param name="transId">事务ID</param>
        public abstract void ExecBusinessHandle(IJobExecutionContext context, long transId);
    }
}
