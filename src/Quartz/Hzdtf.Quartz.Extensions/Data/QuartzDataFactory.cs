using Hzdtf.Quartz.Extensions.Model;
using Hzdtf.Utility.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hzdtf.Utility.Utils;

namespace Hzdtf.Quartz.Extensions.Data
{
    /// <summary>
    /// 时钟数据工厂
    /// @ 黄振东
    /// </summary>
    public class QuartzDataFactory : IQuartzDataFactory
    {
        /// <summary>
        /// 作业明细读取
        /// </summary>
        private readonly IReaderAll<JobDetailInfo> jobDetailReader;

        /// <summary>
        /// 触发器读取
        /// </summary>
        private readonly IReaderAll<TriggerInfo> triggerReader;

        /// <summary>
        /// 作业任务读取
        /// </summary>
        private readonly IReaderAll<JobTaskInfo> jobTaskReader;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="jobDetailReader">作业明细读取</param>
        /// <param name="triggerReader">触发器读取</param>
        /// <param name="jobTaskReader">作业任务读取</param>
        public QuartzDataFactory(IReaderAll<JobDetailInfo> jobDetailReader, IReaderAll<TriggerInfo> triggerReader, IReaderAll<JobTaskInfo> jobTaskReader)
        {
            this.jobDetailReader = jobDetailReader;
            this.triggerReader = triggerReader;
            this.jobTaskReader = jobTaskReader;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <returns>产品</returns>
        public IList<JobTaskInfo> Create()
        {
            var jobTasks = jobTaskReader.ReaderAll();
            if (jobTasks.IsNullOrCount0())
            {
                return null;
            }

            var jobDetails = jobDetailReader.ReaderAll();
            var triggers = triggerReader.ReaderAll();

            foreach (var jt in jobTasks)
            {
                jt.FillJobDetailAndTrigger(jobDetails, triggers);
            }

            return jobTasks;
        }
    }
}
