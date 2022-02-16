using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.RemoteService.Provider
{
    /// <summary>
    /// 服务提供者基类，提供原生服务有更新触发
    /// @ 黄振东
    /// </summary>
    public abstract class ServiceProviderBase : IServicesProvider, INativeServicesProvider
    {
        /// <summary>
        /// 更新触发回调列表
        /// </summary>
        private static readonly IList<Action<string, string, string[]>> actions = new List<Action<string, string, string[]>>();

        /// <summary>
        /// 异步根据服务名获取地址数组
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <param name="tag">标签</param>
        /// <returns>地址数组任务</returns>
        public async Task<string[]> GetAddresses(string serviceName, string tag = null)
        {
            var addes = await ExecGetAddresses(serviceName, tag);
            foreach (var action in actions)
            {
                action(serviceName, tag, addes);
            }

            return addes;
        }

        /// <summary>
        /// 执行异步根据服务名获取地址数组
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <param name="tag">标签</param>
        /// <returns>地址数组任务</returns>
        protected abstract Task<string[]> ExecGetAddresses(string serviceName, string tag = null);

        /// <summary>
        /// 注册获取到地址数组后
        /// </summary>
        /// <param name="callback">回调,0：服务名，1：标签，2：地址数组</param>
        public void GetTheAddressesRegister(Action<string, string, string[]> callback)
        {
            actions.Add(callback);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public virtual void Dispose() { actions.Clear(); }
    }
}
