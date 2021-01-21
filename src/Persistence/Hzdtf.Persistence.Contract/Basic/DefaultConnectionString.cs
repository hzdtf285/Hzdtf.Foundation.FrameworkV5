using Hzdtf.Utility;
using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Enums;
using Hzdtf.Utility.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Persistence.Contract.Basic
{
    /// <summary>
    /// 默认连接字符串
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class DefaultConnectionString : IDefaultConnectionString
    {
        /// <summary>
        /// 连接环境工厂
        /// </summary>
        public ISimpleFactory<EnvironmentType, string[]> ConnEnvironmentFactory
        {
            get;
            set;
        }

        /// <summary>
        /// 连接字符串集合
        /// </summary>
        public string[] Connections
        {
            get => ConnEnvironmentFactory.Create(App.CurrEnvironmentType);
        }
    }
}
