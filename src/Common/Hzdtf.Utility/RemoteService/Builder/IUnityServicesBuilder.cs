using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.RemoteService.Builder
{
    /// <summary>
    /// 统一服务生成器接口
    /// @ 黄振东
    /// </summary>
    public interface IUnityServicesBuilder
    {
        /// <summary>
        /// 异步生成地址
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <param name="path">路径</param>
        /// <param name="tag">标签</param>
        /// <returns>生成地址任务</returns>
        Task<string> BuilderAsync(string serviceName, string path = null, string tag = null);

        /// <summary>
        /// 根据基地址生成地址
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <param name="baseAddress">基地址</param>
        /// <param name="tag">标签</param>
        /// <param name="path">路径</param>
        /// <returns>生成地址</returns>
        string BuilderByBaseAddress(string serviceName, string baseAddress, string tag = null, string path = null);
    }
}
