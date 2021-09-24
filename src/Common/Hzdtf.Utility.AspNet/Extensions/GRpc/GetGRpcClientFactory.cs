using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Extensions;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Hzdtf.Utility.AspNet.Extensions.GRpc
{
    /// <summary>
    /// 获取GRpc客户端工厂
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class GetGRpcClientFactory : IGetGRpcClientFactory
    {
        /// <summary>
        /// 获取GRpc客户端
        /// </summary>
        /// <typeparam name="GRpcClientT">GRpc客户端类型</typeparam>
        /// <returns>GRpc客户端</returns>
        public GRpcClientT GetRpcClient<GRpcClientT>() where GRpcClientT : ClientBase<GRpcClientT>
        {
            return App.Instance.GetService<GRpcClientT>();
        }
    }
}
