using Hzdtf.Example.Service.Contract;
using Hzdtf.Quartz.Extensions.Model;
using Hzdtf.Quartz.Extensions.Scheduler;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Example.Service.Impl.Quartz
{
    /// <summary>
    /// 作业处理异常
    /// @ 黄振东
    /// </summary>
    public class JobHandleException : JobHandleExceptionBase
    {
        /// <summary>
        /// 测试表单服务
        /// </summary>
        private readonly ITestFormService service;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="service">测试表单服务</param>
        public JobHandleException(ITestFormService service) { this.service = service; }

        /// <summary>
        /// 执行通知
        /// </summary>
        /// <param name="context">作业执行上下文</param>
        /// <param name="jobInstance">作业实例</param>
        /// <param name="jobEx">作业异常</param>
        protected override void ExecNotify(IJobExecutionContext context, IJob jobInstance, JobExceptionInfo jobEx)
        {
            Console.WriteLine(jobEx.ToJsonString());
        }

        /// <summary>
        /// 通知
        /// </summary>
        /// <param name="context">作业执行上下文</param>
        /// <param name="jobInstance">作业实例</param>
        /// <param name="transId">事务ID</param>
        /// <param name="ex">异常</param>
        /// <param name="idMsg">ID消息</param>
        public void Notify(IJobExecutionContext context, IJob jobInstance, long transId, Exception ex, string idMsg)
        {
            Console.WriteLine($"事务ID:{transId}.作业异常通知");
        }
    }
}
