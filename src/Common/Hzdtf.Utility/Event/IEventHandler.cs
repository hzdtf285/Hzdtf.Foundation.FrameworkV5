using Hzdtf.Utility.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Event
{
    /// <summary>
    /// 事件处理接口
    /// @ 黄振东
    /// </summary>
    public interface IEventHandler
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="eventData">事件数据</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        void Execute(EventData eventData, CommonUseData comData = null, string connectionId = null);
    }
}
