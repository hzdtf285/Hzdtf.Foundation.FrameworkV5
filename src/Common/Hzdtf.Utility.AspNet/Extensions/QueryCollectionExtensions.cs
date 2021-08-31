using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hzdtf.Utility.Utils;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// 查询收藏扩展类
    /// @ 黄振东
    /// </summary>
    public static class QueryCollectionExtensions
    {
        /// <summary>
        /// 转换为字典
        /// </summary>
        /// <param name="query">查询收藏</param>
        /// <param name="ignoreKeys">忽略键数组</param>
        /// <returns>字典</returns>
        public static IDictionary<string, StringValues> ToDictionary(this IQueryCollection query, params string[] ignoreKeys)
        {
            if (query == null || query.Count == 0)
            {
                return null;
            }

            var dic = new Dictionary<string, StringValues>(query.Count);
            foreach (var item in query)
            {
                if (!ignoreKeys.IsNullOrLength0() && ignoreKeys.Contains(item.Key))
                {
                    continue;
                }

                dic.Add(item.Key, item.Value);
            }

            return dic;
        }
    }
}
