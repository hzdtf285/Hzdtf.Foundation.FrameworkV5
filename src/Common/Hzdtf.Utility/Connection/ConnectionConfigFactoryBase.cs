using Hzdtf.Utility.Safety;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Connection
{
    /// <summary>
    /// 连接带有配置的工厂基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="ConnectionT">连接类型</typeparam>
    /// <typeparam name="ConnectionInfoT">连接信息类型</typeparam>
    /// <typeparam name="ConnectionWrapInfoT">连接包装信息类型</typeparam>
    public abstract class ConnectionConfigFactoryBase<ConnectionT, ConnectionInfoT, ConnectionWrapInfoT> : ConnectionFactoryBase<ConnectionT, ConnectionInfoT, ConnectionWrapInfoT>
        where ConnectionT : IConnection<ConnectionInfoT>
        where ConnectionInfoT : ConnectionInfo
        where ConnectionWrapInfoT : ConnectionWrapInfo<ConnectionInfoT>
    {
        /// <summary>
        /// 配置
        /// </summary>
        protected readonly IConfiguration config;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="symmetricalEncryption">加密</param>
        /// <param name="config">配置</param>
        public ConnectionConfigFactoryBase(ISymmetricalEncryption symmetricalEncryption = null, IConfiguration config = null)
            : base(symmetricalEncryption)
        {
            if (config == null)
            {
                config = App.CurrConfig;
                if (config == null)
                {
                    throw new ArgumentNullException("未注入配置对象，请在构造里传入或者设置App.CurrConfig");
                }
            }
            this.config = config;
        }

        /// <summary>
        /// 根据配置名称获取连接字符串
        /// </summary>
        /// <param name="configName">配置名称</param>
        /// <returns>连接字符串</returns>
        protected override string GetConnectionStringByConfigName(string configName) => config[configName];
    }
}
