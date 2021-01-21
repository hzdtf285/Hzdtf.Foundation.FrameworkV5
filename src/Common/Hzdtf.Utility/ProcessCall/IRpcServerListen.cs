using Hzdtf.Utility.Model;
using Hzdtf.Utility.Release;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.ProcessCall
{
    /// <summary>
    /// Rpc服务监听接口
    /// @ 黄振东
    /// </summary>
    public interface IRpcServerListen : ICloseable, IDisposable
    {
        /// <summary>
        /// 接收中错误
        /// </summary>
        event Action<string, Exception> ReceivingError;

        /// <summary>
        /// 监听
        /// </summary>
        void Listen();

        /// <summary>
        /// 异步监听
        /// </summary>
        void ListenAsync();
    }
}
