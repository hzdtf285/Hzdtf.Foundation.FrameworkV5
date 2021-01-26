using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Routing
{
    /// <summary>
    /// 路由值字典扩展类
    /// @ 黄振东
    /// </summary>
    public static class RouteValueDictionaryExtensions
    {
        /// <summary>
        /// 获取控制器和动作
        /// </summary>
        /// <param name="keyValues">路由字典</param>
        /// <returns>控制器和动作，0:控制器;1:动作</returns>
        public static string[] GetControllerAction(this RouteValueDictionary keyValues)
        {
            if (keyValues == null || keyValues.Count < 2)
            {
                return null;
            }

            if (keyValues.ContainsKey("controller") && keyValues.ContainsKey("action"))
            {
                return new string[]
                {
                    keyValues["controller"].ToString(),
                    keyValues["action"].ToString(),
                };
            }

            return null;
        }
    }
}
