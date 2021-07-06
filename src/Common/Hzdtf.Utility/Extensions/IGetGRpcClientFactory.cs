using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.Extensions
{
    /// <summary>
    /// 获取GRpc客户端工厂
    /// @ 黄振东
    /// </summary>
    public interface IGetGRpcClientFactory
    {
        /// <summary>
        /// 获取GRpc客户端
        /// </summary>
        /// <typeparam name="GRpcClientT">GRpc客户端类型</typeparam>
        /// <returns>GRpc客户端</returns>
        GRpcClientT GetRpcClient<GRpcClientT>() where GRpcClientT : ClientBase<GRpcClientT>;
    }
}
