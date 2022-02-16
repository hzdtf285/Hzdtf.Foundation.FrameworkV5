using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.GRpcStrategy
{
    /// <summary>
    /// GRpc渠道接口
    /// @ 黄振东
    /// </summary>
    public interface IGRpcChannel
    {
        /// <summary>
        /// 异步生成渠道
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <param name="options">grpc渠道配置</param>
        /// <returns>生成地址任务</returns>
        Task<GrpcChannel> BuilderAsync(string serviceName, GrpcChannelOptions options = null);
    }
}
