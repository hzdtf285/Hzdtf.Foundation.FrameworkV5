using Grpc.Core;
using Hzdtf.Utility;
using Hzdtf.Utility.Extensions;
using Hzdtf.Utility.GRpcStrategy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Grpc.Net.Client
{
    /// <summary>
    /// GRpc渠道辅助类
    /// @ 黄振东
    /// </summary>
    public static partial class GRpcChannelUtil
    {
        /// <summary>
        /// 获取策略中的GRpc客户端
        /// </summary>
        /// <typeparam name="GRpcClientT">GRpc客户端类型</typeparam>
        /// <param name="serviceName">服务名</param>
        /// <param name="createGRpcClient">创建GRpc客户端</param>
        /// <param name="action">回调业务处理方法</param>
        /// <param name="exAction">发生异常回调，如果为null，则不会捕获异常</param>
        /// <param name="customerOptions">自定义选项配置</param>
        /// <param name="options">grpc选项配置</param>
        /// <returns>GRpc客户端</returns>
        public static GRpcClientT GetGRpcClientFormStrategy<GRpcClientT>(string serviceName, Func<GrpcChannel, GRpcClientT> createGRpcClient, Action<GRpcClientT, Metadata> action, Action<RpcException> exAction = null, Action<ChannelCustomerOptions> customerOptions = null, GrpcChannelOptions options = null)
            where GRpcClientT : ClientBase<GRpcClientT>
        {
            var grpcChannel = App.GetServiceFromInstance<IGRpcChannel>();
            var channel = grpcChannel.BuilderAsync(serviceName, options).Result;
            var client = createGRpcClient(channel);
            ExecGRpcClient(client, action, exAction, customerOptions);

            return client;
        }
    }
}
