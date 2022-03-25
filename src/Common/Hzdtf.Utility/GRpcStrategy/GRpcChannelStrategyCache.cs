using Grpc.Net.Client;
using Hzdtf.Utility.Cache;
using Hzdtf.Utility.RemoteService.Builder;
using Hzdtf.Utility.RemoteService.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hzdtf.Utility.Utils;
using Hzdtf.Utility.Attr;

namespace Hzdtf.Utility.GRpcStrategy
{
    /// <summary>
    /// GRpc渠道策略缓存
    /// key：服务名
    /// value：地址映射渠道字典（key：地址，value：渠道）
    /// 缓存是以静态变量存储
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class GRpcChannelStrategyCache : SingleTypeLocalMemoryBase<string, IDictionary<string, GrpcChannel>>, IGRpcChannel, IAsyncDisposable
    {
        /// <summary>
        /// 字典缓存
        /// </summary>
        private static readonly IDictionary<string, IDictionary<string, GrpcChannel>> dicCache = new Dictionary<string, IDictionary<string, GrpcChannel>>();

        /// <summary>
        /// 同步字典缓存
        /// </summary>
        private static readonly object syncDicCache = new object();

        /// <summary>
        /// 服务生成器
        /// </summary>
        private readonly IUnityServicesBuilder servicesBuilder;

        /// <summary>
        /// 原生服务提供者
        /// </summary>
        private static INativeServicesProvider nativeServiceProvider;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="servicesBuilder">服务生成器</param>
        /// <param name="nativeServiceProvider">原生服务提供者</param>
        public GRpcChannelStrategyCache(IUnityServicesBuilder servicesBuilder, INativeServicesProvider nativeServiceProvider = null)
        {
            this.servicesBuilder = servicesBuilder;
            if (nativeServiceProvider != null && GRpcChannelStrategyCache.nativeServiceProvider == null)
            {
                GRpcChannelStrategyCache.nativeServiceProvider = nativeServiceProvider;
                GRpcChannelStrategyCache.nativeServiceProvider.GetTheAddressesRegister((serviceName, tag, addresses) =>
                {
                    UpdateExistsesAddress(serviceName, addresses);
                });
            }
        }

        /// <summary>
        /// 异步生成渠道
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <param name="options">grpc渠道配置</param>
        /// <returns>生成地址任务</returns>
        public async Task<GrpcChannel> BuilderAsync(string serviceName, GrpcChannelOptions options = null)
        {
            var address = await servicesBuilder.BuilderAsync(serviceName);
            if (dicCache.ContainsKey(serviceName))
            {
                var dic = dicCache[serviceName];
                if (dic.ContainsKey(address))
                {
                    return dic[address];
                }
                else
                {
                    var channel = options == null ? GrpcChannel.ForAddress(address) : GrpcChannel.ForAddress(address, options);
                    try
                    {
                        dic.Add(address, channel);
                    }
                    catch (ArgumentException)
                    {
                    }

                    return channel;
                }
            }
            else
            {
                var dic = new Dictionary<string, GrpcChannel>();
                Add(serviceName, dic);
                var channel = options == null ? GrpcChannel.ForAddress(address) : GrpcChannel.ForAddress(address, options);
                try
                {
                    dic.Add(address, channel);
                }
                catch (ArgumentException)
                {
                }

                return channel;
            }
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <returns>缓存</returns>
        protected override IDictionary<string, IDictionary<string, GrpcChannel>> GetCache() => dicCache;

        /// <summary>
        /// 获取同步的缓存对象，是为了线程安全
        /// </summary>
        /// <returns>同步的缓存对象</returns>
        protected override object GetSyncCache() => syncDicCache;

        /// <summary>
        /// 更新存在的地址，如果缓存里的地址在新的地址不存在，则移除
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <param name="newAddresses">新的地址数组</param>
        private void UpdateExistsesAddress(string serviceName, string[] newAddresses)
        {
            if (dicCache.ContainsKey(serviceName))
            {
                var dic = dicCache[serviceName];
                if (dic.Count == 0)
                {
                    return;
                }
                if (newAddresses.IsNullOrLength0())
                {
                    foreach (var item in dic)
                    {
                        try
                        {
                            item.Value.ShutdownAsync();
                        }
                        catch { }
                    }
                    dic.Clear();
                    return;
                }

                IList<string> tempNewAddes = new List<string>(newAddresses.Length);
                foreach (var newAdd in newAddresses)
                {
                    tempNewAddes.Add(servicesBuilder.BuilderByBaseAddress(serviceName, newAdd));
                }

                // 需要删除的地址列表
                var needRemoveAddes = new List<KeyValuePair<string, GrpcChannel>>(); 
                foreach (var existsAdd in dic)
                {
                    if (tempNewAddes.Contains(existsAdd.Key))
                    {
                        continue;
                    }
                    needRemoveAddes.Add(existsAdd);
                }
                if (needRemoveAddes.Count == 0)
                {
                    return;
                }

                foreach (var item in needRemoveAddes)
                {
                    try
                    {
                        item.Value.ShutdownAsync();
                    }
                    catch { }

                    dic.RemoveKey(item.Key);
                }
            }
        }

        /// <summary>
        /// 异步释放资源
        /// </summary>
        /// <returns>任务</returns>
        public async ValueTask DisposeAsync()
        {
            foreach (var item in dicCache)
            {
                if (item.Value.IsNullOrCount0())
                {
                    continue;
                }

                foreach (var item2 in item.Value)
                {
                    await item2.Value.ShutdownAsync();
                }
                item.Value.Clear();
            }

            dicCache.Clear();
        }
    }
}
