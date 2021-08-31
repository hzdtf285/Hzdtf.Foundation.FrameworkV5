using Hzdtf.Quartz.Extensions.Scheduler;
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
    }
}
