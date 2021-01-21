using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// 配置工厂类
    /// @ 黄振东
    /// </summary>
    public static class ConfigurationFactory
    {
        /// <summary>
        /// 生成配置，通常用在桌面端
        /// </summary>
        /// <param name="options">选项配置，如果不传则默认加载appsetting.json</param>
        /// <returns>配置对象</returns>
        public static IConfigurationRoot BuilderConfig(Action<IConfigurationBuilder> options = null)
        {
            var builder = new ConfigurationBuilder();
            if (options != null)
            {
                options(builder);
            }
            else
            {
                builder.AddJsonFile("appsettings.json");
            }

            return builder.Build();
        }
    }
}
