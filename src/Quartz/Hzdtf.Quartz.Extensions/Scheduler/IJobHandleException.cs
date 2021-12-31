using Hzdtf.Quartz.Extensions.Model;
﻿using Quartz;
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
        /// <param name="jobEx">作业异常</param>
        void Notify(IJobExecutionContext context, IJob jobInstance, JobExceptionInfo jobEx);
    }
}
