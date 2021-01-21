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
    /// 默认连接字符串工厂
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class DefaultConnectionStringFactory : ISimpleFactory<EnvironmentType, string[]>
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>产品</returns>
        public string[] Create(EnvironmentType type)
        {
            string[] result = new string[2];
            switch (type)
            {
                case EnvironmentType.PRODUCTION:
                    result[0] = App.DefaultConnectionString;
                    result[1] = App.SlaveConnectionString;

                    break;

                case EnvironmentType.TEST:
                    result[0] = App.TestDefaultConnectionString;
                    result[1] = App.TestSlaveConnectionString;

                    break;
            }

            return result;
        }
    }
}
