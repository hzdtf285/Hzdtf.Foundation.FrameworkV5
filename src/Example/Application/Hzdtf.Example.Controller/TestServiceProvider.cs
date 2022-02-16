using Hzdtf.Utility.RemoteService.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hzdtf.Example.Controller
{
    /// <summary>
    /// 测试服务提供者
    /// @ 黄振东
    /// </summary>
    public class TestServiceProvider : ServiceProviderBase
    {
        /// <summary>
        /// 异步根据服务名获取地址数组
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <param name="tag">标签</param>
        /// <returns>地址数组任务</returns>
        protected override Task<string[]> ExecGetAddresses(string serviceName, string tag = null)
        {
            return Task<string[]>.FromResult(new string[] { "localhost:5000", "localhost:5002" });
        }
    }
}
