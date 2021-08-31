using Hzdtf.Utility.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Quartz.Extensions.Model
{
    /// <summary>
    /// 触发器信息
    /// @ 黄振东
    /// </summary>
    public class TriggerInfo : SimpleInfo<int>
    {
        /// <summary>
        /// Cron表达式
        /// </summary>
        public string CronSchedule
        {
            get;
            set;
        }

        /// <summary>
        /// 参数
        /// </summary>
        public IDictionary<string, string> Params
        {
            get;
            set;
        }
    }
}
