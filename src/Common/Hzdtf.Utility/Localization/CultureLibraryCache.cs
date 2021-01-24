﻿using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hzdtf.Utility.Cache;

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
        private static IDictionary<string, IDictionary<string, string>> dicCache = new Dictionary<string, IDictionary<string, string>>(1);

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
        /// 获取缓存
        /// </summary>
        /// <returns>缓存</returns>
        protected override IDictionary<string, IDictionary<string, string>> GetCache()
        {
            if (dicCache.Count == 0)
            {
                if (protoCultureLibrary == null)
                {
                    throw new NullReferenceException("原生文化库读取不能为null");
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
    }
}
