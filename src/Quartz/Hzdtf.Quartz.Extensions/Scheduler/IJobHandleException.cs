using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Quartz.Extensions.Scheduler
{
    /// <summary>
    /// 作业处理异常接口
    /// @ 黄振东
    /// </summary>
    public interface IJobHandleException
    {
        /// <summary>
        /// 通知
        /// </summary>
        /// <param name="context">作业执行上下文</param>
        /// <param name="jobInstance">作业实例</param>
        /// <param name="transId">事务ID</param>
        /// <param name="ex">异常</param>
        /// <param name="idMsg">ID消息</param>
        void Notify(IJobExecutionContext context, IJob jobInstance, long transId, Exception ex, string idMsg);
    }
}
