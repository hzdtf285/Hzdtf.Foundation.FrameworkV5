using Hzdtf.Utility.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.Extensions
{
    /// <summary>
    /// 渠道自定义选项配置
    /// @ 黄振东
    /// </summary>
    public class ChannelCustomerOptions
    {
        /// <summary>
        /// 是否添加Token，设置后，会调用App.GetTokenFunc获取Token
        /// 默认为是
        /// </summary>
        public bool IsAddToken
        {
            get;
            set;
        } = true;

        /// <summary>
        /// 获取Token回调
        /// </summary>
        public Func<string> GetTokenFunc
        {
            get;
            set;
        }

        /// <summary>
        /// 是否添加事件ID，设置后，会调用App.GetEventIdFunc获取事件ID
        /// 默认为是
        /// </summary>
        public bool IsAddEventId
        {
            get;
            set;
        } = true;

        /// <summary>
        /// 通用数据
        /// </summary>
        public CommonUseData ComData
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 渠道自定义选项扩展类
    /// @ 黄振东
    /// </summary>
    public static class ChannelCustomerOptionsExtensions
    {
        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="cusOptions">渠道自定义选项</param>
        /// <returns>token</returns>
        public static string GetToken(this ChannelCustomerOptions cusOptions)
        {
            if (cusOptions == null || !cusOptions.IsAddToken)
            {
                return null;
            }

            string token = cusOptions.ComData != null ? cusOptions.ComData.Token : null;
            if (string.IsNullOrWhiteSpace(token) && (cusOptions.GetTokenFunc != null || App.GetTokenFunc != null))
            {
                token = cusOptions.GetTokenFunc != null ? cusOptions.GetTokenFunc() : App.GetTokenFunc();
            }

            return token;
        }

        /// <summary>
        /// 获取事件ID
        /// </summary>
        /// <param name="cusOptions">渠道自定义选项</param>
        /// <returns>事件ID</returns>
        public static string GetEventId(this ChannelCustomerOptions cusOptions)
        {
            if (cusOptions == null || !cusOptions.IsAddEventId)
            {
                return null;
            }

            string eventId = cusOptions.ComData != null ? cusOptions.ComData.EventId : null;
            if (string.IsNullOrWhiteSpace(eventId) && App.GetEventIdFunc != null)
            {
                eventId = App.GetEventIdFunc();
            }

            return eventId;
        }
    }
}
