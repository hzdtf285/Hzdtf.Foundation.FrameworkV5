using Hzdtf.Redis.Extensions;
using Hzdtf.Utility.Attr;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Hzdtf.Utility;

namespace Hzdtf.Redis.ExtensionsCore
{
    /// <summary>
    /// Redis配置选项解析
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class RedisConfigOptionParse : IRedisConfigOptionParse
    {
        /// <summary>
        /// 解析
        /// </summary>
        /// <returns>Redis配置选项</returns>
        public RedisConfigOptions Parse()
        {
            return App.CurrConfig.GetSection("Redis").Get<RedisConfigOptions>();
        }
    }
}
