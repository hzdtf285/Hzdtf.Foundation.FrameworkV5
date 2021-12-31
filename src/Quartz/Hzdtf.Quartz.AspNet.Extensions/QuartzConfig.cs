using Hzdtf.Quartz.Extensions.Data;
using Hzdtf.Quartz.Extensions.Model;
using Hzdtf.Quartz.Extensions.Scheduler;
using Hzdtf.Utility.Data;
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
        /// 作业明细读取
        /// </summary>
        public IReaderAll<JobDetailInfo> JobDetailReader
        {
            get;
            set;
        }

        /// <summary>
        /// 作业任务读取
        /// </summary>
        public IReaderAll<JobTaskInfo> JobTaskReader
        {
            get;
            set;
        }

        /// <summary>
        /// 触发器读取
        /// </summary>
        public IReaderAll<TriggerInfo> TriggerReader
        {
            get;
            set;
        }

        /// <summary>
        /// 时钟数据工厂
        /// </summary>
        public IQuartzDataFactory QuartzDataFactory
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
