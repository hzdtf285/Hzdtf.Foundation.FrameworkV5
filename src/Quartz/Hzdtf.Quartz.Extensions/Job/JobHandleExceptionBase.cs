using Hzdtf.Logger.Contract;
using Hzdtf.Quartz.Model;
using Microsoft.Extensions.Configuration;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Quartz.Extensions.Job
{
    /// <summary>
    /// 作业处理异常
    /// @ 黄振东
    /// </summary>
    public abstract class JobHandleExceptionBase : IJobHandleException
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
        /// 通知
        /// </summary>
        /// <param name="context">作业执行上下文</param>
        /// <param name="jobInstance">作业实例</param>
        /// <param name="jobEx">作业异常</param>
        public void Notify(IJobExecutionContext context, IJob jobInstance, JobExceptionInfo jobEx)
        {
            try
            {
                ExecNotify(context, jobInstance, jobEx);
            }
            catch (Exception outEx)
            {
                Log.ErrorAsync($"执行作业处理异常程序时又发生异常", outEx, this.GetType().Name, null, jobEx.TransId.ToString(), "Notify");
            }
        }

        /// <summary>
        /// 执行通知
        /// </summary>
        /// <param name="context">作业执行上下文</param>
        /// <param name="jobInstance">作业实例</param>
        /// <param name="jobEx">作业异常</param>
        protected abstract void ExecNotify(IJobExecutionContext context, IJob jobInstance, JobExceptionInfo jobEx);
    }
}
