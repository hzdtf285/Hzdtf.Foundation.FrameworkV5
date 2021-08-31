using Hzdtf.Logger.Contract;
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
                }
                catch (Exception ex)
                {
                    Log.ErrorAsync($"执行作业发生异常.{idMsg}", ex, thisClass, null, transIdStr, name, group, "Execute");
                    if (QuartzStaticConfig.JobHandleException != null)
                    {
                        QuartzStaticConfig.JobHandleException.Notify(context, this, transId, ex, idMsg);
                    }
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
