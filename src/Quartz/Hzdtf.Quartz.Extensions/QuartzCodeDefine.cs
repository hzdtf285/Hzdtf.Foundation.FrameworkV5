using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Quartz.Extensions
{
    /// <summary>
    /// 时钟编码定义
    /// @ 黄振东
    /// </summary>
    public static class QuartzCodeDefine
    {
        /// <summary>
        /// 作业任务ID键
        /// </summary>
        public static string JobTaskIdKey = "JobTask:Id";

        /// <summary>
        /// 作业任务执行成功后移除键
        /// </summary>
        public static string JobTaskSuccessedRemoveKey = "JobTask:SuccessedRemove";
    }
}
