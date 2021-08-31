using Hzdtf.Quartz.Extensions.Model;
using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hzdtf.Utility.Utils;

namespace Hzdtf.Quartz.Extensions.Data
{
    /// <summary>
    /// 时钟数据工厂
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class QuartzDataFactory : IQuartzDataFactory
    {
        /// <summary>
        /// 作业明细读取
        /// </summary>
        public IReaderAll<JobDetailInfo> JobDetailReader
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
        /// 作业任务读取
        /// </summary>
        public IReaderAll<JobTaskInfo> JobTaskReader
        {
            get;
            set;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <returns>产品</returns>
        public IList<JobTaskInfo> Create()
        {
            var jobTasks = JobTaskReader.ReaderAll();
            if (jobTasks.IsNullOrCount0())
            {
                return null;
            }

            var jobDetails = JobDetailReader.ReaderAll();
            var triggers = TriggerReader.ReaderAll();
            foreach (var jt in jobTasks)
            {
                jt.FillJobDetailAndTrigger(jobDetails, triggers);
            }

            return jobTasks;
        }
    }
}
