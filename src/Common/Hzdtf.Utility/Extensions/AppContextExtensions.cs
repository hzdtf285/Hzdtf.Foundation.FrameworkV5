using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// APP上下文扩展类
    /// @ 黄振东
    /// </summary>
    public static class AppContextExtensions
    {
        /// <summary>
        /// 设置http2非加密是否支持
        /// 例如：GRpc如果要对http进行支持，则需要启用
        /// 执行此方法后，默认是启用
        /// </summary>
        /// <param name="enable">是否启动</param>
        public static void SetHttp2UnencryptedSupport(bool enable = true)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
        }
    }
}
