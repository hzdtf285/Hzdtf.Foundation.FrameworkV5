using Hzdtf.Logger.Contract;
using Hzdtf.Quartz.Extensions.Job;
using Hzdtf.Quartz.Extensions.Scheduler;
using Hzdtf.Quartz.Model;
using Hzdtf.Quartz.Persistence.Contract;
using Hzdtf.Utility;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Quartz.Extensions
{
    /// <summary>
    /// 时钟静态配置
    /// @ 黄振东
    /// </summary>
    public static class QuartzStaticConfig
    {
        /// <summary>
        /// 日志
        /// </summary>
        public static ILogable Log
        {
            get;
            set;
        } = LogTool.DefaultLog;

        /// <summary>
        /// 配置
        /// </summary>
        public static IConfiguration Config
        {
            get;
            set;
        } = App.CurrConfig;

        /// <summary>
        /// 是否跟踪日志
        /// </summary>
        public static bool IsTraceLog
        {
            get
            {
                var str = Config["Quartz:IsTraceLog"];
                if (string.IsNullOrWhiteSpace(str))
                {
                    return false;
                }

                return Convert.ToBoolean(str);
            }
        }

        /// <summary>
        /// 工作处理异常
        /// </summary>
        public static IJobHandleException JobHandleException
        {
            get;
            set;
        }

        /// <summary>
        /// 作业任务持久化
        /// </summary>
        public static IJobTaskBasicPersistence JobTaskPersistence
        {
            get;
            set;
        }

        /// <summary>
        /// 调度器包装
        /// </summary>
        public static ISchedulerWrap SchedulerWrap
        {
            get;
            set;
        }

        /// <summary>
        /// 作业任务过滤器
        /// </summary>
        public static JobTaskFilterInfo JobTaskFilter
        {
            get;
            set;
        }

        /// <summary>
        /// 工作处理异常程序集
        /// </summary>
        public static string JobHandleExceptionAssembly
        {
            get;
            set;
        }
    }
}
