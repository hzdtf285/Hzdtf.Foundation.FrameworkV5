using Hzdtf.Utility.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.RoutePermission
{
    /// <summary>
    /// 路由权限Json
    /// @ 黄振东
    /// </summary>
    public class RoutePermissionJson : IRoutePermissionConfigReader
    {
        /// <summary>
        /// Json文件
        /// </summary>
        private readonly string jsonFile;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="jsonFile">Json文件</param>
        public RoutePermissionJson(string jsonFile = "Config/routePermissionConfig.json")
        {
            this.jsonFile = jsonFile;
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public RoutePermissionInfo[] Reader()
        {
            var wrapApiPer = jsonFile.ToJsonObjectFromFile<WrapRoutePermissionInfo>();
            return wrapApiPer.Config;
        }
    }

    /// <summary>
    /// 包装路由权限信息
    /// @ 黄振东
    /// </summary>
    public class WrapRoutePermissionInfo
    {
        /// <summary>
        /// 配置
        /// </summary>
        public RoutePermissionInfo[] Config
        {
            get;
            set;
        }
    }
}
