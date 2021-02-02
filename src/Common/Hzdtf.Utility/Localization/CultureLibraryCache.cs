using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hzdtf.Utility.Cache;
using Hzdtf.Utility.Utils;

namespace Hzdtf.Utility.Localization
{
    /// <summary>
    /// 文化库缓存
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class CultureLibraryCache : SingleTypeLocalMemoryBase<string, IDictionary<string, string>>, ICultureLibrary, ISetObject<ICultureLibrary>
    {
        #region 属性与字段

        /// <summary>
        /// 字典缓存
        /// </summary>
        private static IDictionary<string, IDictionary<string, string>> dicCache = new Dictionary<string, IDictionary<string, string>>(20);

        /// <summary>
        /// 同步字典缓存
        /// </summary>
        private static readonly object syncDicCache = new object();

        /// <summary>
        /// 原生文化库读取
        /// </summary>
        protected ICultureLibrary protoCultureLibrary;

        #endregion

        #region 重写父类的方法

        /// <summary>
        /// 根据键获取值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public override IDictionary<string, string> Get(string key)
        {
            if (Exists(key))
            {
                return dicCache[key];
            }
            else
            {
                if (dicCache.Count == 0)
                {
                    GetCache();
                    if (dicCache.ContainsKey(key))
                    {
                        return dicCache[key];
                    }

                    return null;
                }
                else
                {
                    var values = protoCultureLibrary.Get(key);
                    if (values == null)
                    {
                        return null;
                    }
                    Add(key, values);

                    return values;
                }
            }
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <returns>缓存</returns>
        protected override IDictionary<string, IDictionary<string, string>> GetCache()
        {
            if (dicCache.Count == 0)
            {
                if (protoCultureLibrary == null)
                {
                    return dicCache;
                }

                var temp = protoCultureLibrary.Reader();
                lock (syncDicCache)
                {
                    dicCache = temp;
                }
            }

            return dicCache;
        }

        /// <summary>
        /// 获取同步的缓存对象，是为了线程安全
        /// </summary>
        /// <returns>同步的缓存对象</returns>
        protected override object GetSyncCache() => syncDicCache;

        #endregion

        #region ISetObject<ICultureLibrary> 接口

        /// <summary>
        /// 设置对象
        /// </summary>
        /// <param name="obj">对象</param>
        public void Set(ICultureLibrary obj)
        {
            this.protoCultureLibrary = obj;
        }

        #endregion

        #region ICultureLibrary 接口

        /// <summary>
        /// 根据键数组获取值字典
        /// </summary>
        /// <param name="keys">键数组</param>
        /// <returns>值字典</returns>
        public IDictionary<string, IDictionary<string, string>> Get(string[] keys)
        {
            if (keys.IsNullOrLength0())
            {
                return null;
            }

            var result = new Dictionary<string, IDictionary<string, string>>(keys.Length);
            // 不在缓存里的键列表
            var notCacheKeys = new List<string>();
            foreach (var k in keys)
            {
                if (dicCache.ContainsKey(k))
                {
                    result.Add(k, dicCache[k]);
                }
                else
                {
                    notCacheKeys.Add(k);
                }
            }
            // 不在缓存里则从原生获取
            if (notCacheKeys.Count > 0)
            {
                var keyValues = protoCultureLibrary.Get(notCacheKeys.ToArray());
                if (keyValues.IsNullOrCount0())
                {
                    return result;
                }

                lock (syncDicCache)
                {
                    foreach (var kv in keyValues)
                    {
                        try
                        {
                            result.Add(kv.Key, kv.Value);
                            if (dicCache.ContainsKey(kv.Key))
                            {
                                continue;
                            }
                            dicCache.Add(kv.Key, kv.Value);
                        }
                        catch (ArgumentException) { }
                    }
                }
            }

            return result;
        }

        #endregion
    }
}
