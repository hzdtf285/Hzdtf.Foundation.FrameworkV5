using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Logger.Contract
{
    /// <summary>
    /// 日志基类
    /// @ 黄振东
    /// </summary>
    public partial class LogBase
    {
        /// <summary>
        /// 异步跟踪
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="eventId">事件ID</param>
        /// <param name="tags">标签</param>
        /// <returns>任务</returns>
        public Task TraceAsync(string msg, Exception ex = null, string source = null, string eventId = null, params string[] tags)
        {
            return Task.Factory.StartNew(() => BeforeWriteStorage("trace", msg, GetEventId(eventId), ex, source, tags));
        }

        /// <summary>
        /// 异步调试
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="eventId">事件ID</param>
        /// <param name="tags">标签</param>
        /// <returns>任务</returns>
        public Task DebugAsync(string msg, Exception ex = null, string source = null, string eventId = null, params string[] tags)
        {
            return Task.Factory.StartNew(() => BeforeWriteStorage("debug", msg, GetEventId(eventId), ex, source, tags));
        }

        /// <summary>
        /// 异步信息
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="eventId">事件ID</param>
        /// <param name="tags">标签</param>
        /// <returns>任务</returns>
        public Task InfoAsync(string msg, Exception ex = null, string source = null, string eventId = null, params string[] tags)
        {
            return Task.Factory.StartNew(() => BeforeWriteStorage("info", msg, GetEventId(eventId), ex, source, tags));
        }

        /// <summary>
        /// 异步警告
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="eventId">事件ID</param>
        /// <param name="tags">标签</param>
        /// <returns>任务</returns>
        public Task WranAsync(string msg, Exception ex = null, string source = null, string eventId = null, params string[] tags)
        {
            return Task.Factory.StartNew(() => BeforeWriteStorage("wran", msg, GetEventId(eventId), ex, source, tags));
        }

        /// <summary>
        /// 异步错误
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="eventId">事件ID</param>
        /// <param name="tags">标签</param>
        /// <returns>任务</returns>
        public Task ErrorAsync(string msg, Exception ex = null, string source = null, string eventId = null, params string[] tags)
        {
            return Task.Factory.StartNew(() => BeforeWriteStorage("error", msg, GetEventId(eventId), ex, source, tags));
        }

        /// <summary>
        /// 异步致命
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="eventId">事件ID</param>
        /// <param name="tags">标签</param>
        /// <returns>任务</returns>
        public Task FatalAsync(string msg, Exception ex = null, string source = null, string eventId = null, params string[] tags)
        {
            return Task.Factory.StartNew(() => BeforeWriteStorage("fatal", msg, GetEventId(eventId), ex, source, tags));
        }

        #region IInfoEvent 接口

        /// <summary>
        /// 异步记录
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="eventId">事件ID</param>
        /// <param name="tags">标签</param>
        /// <returns>任务</returns>
        public Task RecordAsync(string msg, Exception ex = null, string source = null, string eventId = null, params string[] tags)
        {
            return ex == null ? TraceAsync(msg, ex, source, eventId, tags) : ErrorAsync(msg, ex, source, eventId, tags);
        }

        #endregion
    }
}
