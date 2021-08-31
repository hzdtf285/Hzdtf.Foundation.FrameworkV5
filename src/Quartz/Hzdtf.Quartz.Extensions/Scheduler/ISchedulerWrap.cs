using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Quartz.Extensions.Scheduler
{
    /// <summary>
    /// 调度器包装接口
    /// @ 黄振东
    /// </summary>
    public interface ISchedulerWrap
    {
        /// <summary>
        /// 异步启动
        /// </summary>
        /// <returns>任务</returns>
        Task StartAsync();
    }
}
