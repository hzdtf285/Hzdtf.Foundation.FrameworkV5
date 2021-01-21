using Hzdtf.Utility.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Utils;
using Hzdtf.Utility.Attr;

namespace Hzdtf.Utility.ApiPermission
{
    /// <summary>
    /// 路由权限缓存
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class RoutePermissionCache : IReader<RoutePermissionInfo[]>, ISetObject<IReader<RoutePermissionInfo[]>>
    {
        /// <summary>
        /// 原生读取
        /// </summary>
        private IReader<RoutePermissionInfo[]> protoReader = new RoutePermissionJson();

        /// <summary>
        /// 缓存
        /// </summary>
        private static RoutePermissionInfo[] cache;

        /// <summary>
        /// 同步缓存
        /// </summary>
        private static readonly object syncCache = new object();

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public RoutePermissionInfo[] Reader()
        {
            if (cache != null)
            {
                return cache;
            }

            var temp = protoReader.Reader();
            lock (syncCache)
            {
                cache = temp;
            }

            return cache;
        }

        /// <summary>
        /// 设置对象
        /// </summary>
        /// <param name="obj">对象</param>
        public void Set(IReader<RoutePermissionInfo[]> obj)
        {
            this.protoReader = obj;
        }
    }
}
