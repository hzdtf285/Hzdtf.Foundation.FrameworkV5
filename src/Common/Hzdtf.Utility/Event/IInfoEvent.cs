using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.Event
{
    /// <summary>
    /// 信息事件接口
    /// @ 黄振东
    /// </summary>
    public interface IInfoEvent
    {
        /// <summary>
        /// 异步记录
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="eventId">事件ID</param>
        /// <param name="tags">标签</param>
        /// <returns>任务</returns>
        Task RecordAsync(string msg, Exception ex = null, string source = null, string eventId = null, params string[] tags);
    }
}
