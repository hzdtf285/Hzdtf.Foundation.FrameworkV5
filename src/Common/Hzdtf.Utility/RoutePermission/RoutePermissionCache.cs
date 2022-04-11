using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.RoutePermission
{
    /// <summary>
    /// 路由权限缓存
    /// @ 黄振东
    /// </summary>
    public class RoutePermissionCache : IRoutePermissionReader
    {
        /// <summary>
        /// 配置读取
        /// </summary>
        private readonly IRoutePermissionConfigReader confifgReader;

        /// <summary>
        /// 缓存
        /// </summary>
        private static RoutePermissionInfo[] cache;

        /// <summary>
        /// 同步缓存
        /// </summary>
        private static readonly object syncCache = new object();

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="confifgReader">配置读取</param>
        public RoutePermissionCache(IRoutePermissionConfigReader confifgReader)
        {
            if (confifgReader == null)
            {
                throw new ArgumentNullException("confifgReader配置读取不能为空");
            }
            this.confifgReader = confifgReader;
        }

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

            var temp = confifgReader.Reader();
            lock (syncCache)
            {
                cache = temp;
            }

            return cache;
        }
    }
}
