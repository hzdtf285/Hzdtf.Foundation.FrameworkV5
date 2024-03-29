﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.AsyncCallbackWrap
{
    /// <summary>
    /// 异步释放包装接口
    /// 提供对本地线程阻塞，在本进程内，另处进行线程继续执行(释放)
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="KeyT">键类型</typeparam>
    public interface IAsyncReleaseWrap<KeyT>
    {
        /// <summary>
        /// 等待
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="timeout">超时，默认永不超时</param>
        /// <returns>回调的返回值。例如：如果超时，则返回false</returns>
        bool Wait(KeyT key, TimeSpan? timeout = null);

        /// <summary>
        /// 等待
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="returnValue">返回值</param>
        /// <param name="timeout">超时，默认永不超时</param>
        /// <returns>是否接收到信号。例如：如果超时，则返回false</returns>
        bool Wait(KeyT key, out object returnValue, TimeSpan? timeout = null);

        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="callbackReturnValue">回调的返回值</param>
        /// <returns>是否释放成功</returns>
        bool Release(KeyT key, object callbackReturnValue = null);
    }
}
