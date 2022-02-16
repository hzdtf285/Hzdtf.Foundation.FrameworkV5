using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.RemoteService.Provider
{
    /// <summary>
    /// 原生服务提供者接口，代表着最原始的服务提供，比如直通第三方服务发现
    /// 缓存不适合本接口
    /// @ 黄振东
    /// </summary>
    public interface INativeServicesProvider
    {
        /// <summary>
        /// 注册获取到地址数组后
        /// </summary>
        /// <param name="callback">回调,0：服务名，1：标签，2：地址数组</param>
        void GetTheAddressesRegister(Action<string, string, string[]> callback);
    }
}
