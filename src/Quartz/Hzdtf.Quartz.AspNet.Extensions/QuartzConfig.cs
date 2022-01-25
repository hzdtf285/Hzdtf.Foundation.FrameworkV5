using Hzdtf.Quartz.Extensions.Scheduler;
using Hzdtf.Quartz.Persistence.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hzdtf.Quartz.AspNet.Extensions
{
    /// <summary>
    /// 时钟配置
    /// @ 黄振东
    /// </summary>
    public class QuartzConfig
    {
        /// <summary>
        /// 工作处理异常
        /// </summary>
        public IJobHandleException JobHandleException
        {
            get;
            set;
        }

        /// <summary>
        /// 工作处理异常程序集
        /// </summary>
        public string JobHandleExceptionAssembly
        {
            get;
            set;
        }

        /// <summary>
        /// 作业任务持久化
        /// </summary>
        public IJobTaskBasicPersistence JobTaskPersistence
        {
            get;
            set;
        }

        /// <summary>
        /// 调试器包装
        /// </summary>
        public ISchedulerWrap SchedulerWrap
        {
            get;
            set;
        }
    }
}
