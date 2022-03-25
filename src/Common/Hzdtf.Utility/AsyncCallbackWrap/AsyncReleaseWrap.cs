using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hzdtf.Utility.AsyncCallbackWrap
{
    /// <summary>
    /// 异步释放包装
    /// 提供对本地线程阻塞，在本进程内，另处进行线程继续执行(释放)
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="KeyT">键类型</typeparam>
    [Inject]
    public class AsyncReleaseWrap<KeyT> : SingleTypeLocalMemoryBase<KeyT, AutoResetReturnInfo>, IAsyncReleaseWrap<KeyT>
    {
        /// <summary>
        /// 缓存键
        /// </summary>
        private static readonly IDictionary<KeyT, AutoResetReturnInfo> dicCaches = new Dictionary<KeyT, AutoResetReturnInfo>();

        /// <summary>
        /// 同步缓存键
        /// </summary>
        private static readonly object syncDicCaches = new object();

        /// <summary>
        /// 等待
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="timeout">超时，默认永不超时</param>
        /// <returns>回调的返回值</returns>
        public object Wait(KeyT key, TimeSpan? timeout = null)
        {
            var autoReset = new AutoResetReturnInfo()
            {
                AutoReset = new AutoResetEvent(false)
            };
            try
            {
                Add(key, autoReset);
                if (timeout == null)
                {
                    autoReset.AutoReset.WaitOne();
                }
                else
                {
                    autoReset.AutoReset.WaitOne(timeout.Value);
                }

                return autoReset.CallbackReturn;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                Remove(key);
            }
        }

        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="callbackReturnValue">回调的返回值</param>
        public void Release(KeyT key, object callbackReturnValue = null)
        {
            if (dicCaches.ContainsKey(key))
            {
                var autoReset = dicCaches[key];
                autoReset.CallbackReturn = callbackReturnValue;
                autoReset.AutoReset.Set();
            }
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <returns>缓存对象</returns>
        protected override IDictionary<KeyT, AutoResetReturnInfo> GetCache()
        {
            return dicCaches;
        }

        /// <summary>
        /// 获取同步缓存对象
        /// </summary>
        /// <returns>同步缓存对象</returns>
        protected override object GetSyncCache()
        {
            return syncDicCaches;
        }
    }

    /// <summary>
    /// 自动重置返回信息
    /// @ 黄振东
    /// </summary>
    public class AutoResetReturnInfo
    {
        /// <summary>
        /// 自动重置事件
        /// </summary>
        public AutoResetEvent AutoReset
        {
            get;
            set;
        }

        /// <summary>
        /// 回调返回
        /// </summary>
        public object CallbackReturn
        {
            get;
            set;
        }
    }
}
