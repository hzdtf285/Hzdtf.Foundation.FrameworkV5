using Hzdtf.Utility.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hzdtf.Utility.Utils;

namespace Hzdtf.Quartz.Extensions.Model
{
    /// <summary>
    /// 作业任务信息
    /// 由作业明细和触发器组成
    /// @ 黄振东
    /// </summary>
    public class JobTaskInfo : SimpleInfo<int>
    {
        /// <summary>
        /// 作业明细ID
        /// </summary>
        public int JobDetailId
        {
            get;
            set;
        }

        /// <summary>
        /// 触发器ID
        /// </summary>
        public int TriggerId
        {
            get;
            set;
        }

        /// <summary>
        /// 作业明细
        /// </summary>
        public JobDetailInfo JobDetail
        {
            get;
            set;
        }

        /// <summary>
        /// 触发器
        /// </summary>
        public TriggerInfo Trigger
        {
            get;
            set;
        }

        /// <summary>
        /// 填充作业明细和触发器
        /// </summary>
        /// <param name="jobDetails">作业明细列表</param>
        /// <param name="triggers">触发器列表</param>
        public void FillJobDetailAndTrigger(IList<JobDetailInfo> jobDetails, IList<TriggerInfo> triggers)
        {
            if (JobDetail == null && !jobDetails.IsNullOrCount0())
            {
                JobDetail = jobDetails.Where(p => p.Id == JobDetailId).FirstOrDefault();
            }
            if (Trigger == null && !jobDetails.IsNullOrCount0())
            {
                Trigger = triggers.Where(p => p.Id == TriggerId).FirstOrDefault();
            }
        }
    }
}
