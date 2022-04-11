using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.RoutePermission
{
    /// <summary>
    /// 路由权限属性
    /// @ 黄振东
    /// </summary>
    public class RoutePermissionAttribute : PermissionAttributeBase
    {
        /// <summary>
        /// 编码
        /// </summary>
        private readonly string code;

        /// <summary>
        /// 编码
        /// </summary>
        public string Code
        {
            get => code;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="id">ID</param>
        /// <param name="disabled">是否禁用</param>
        /// <param name="extendJson">扩展属性JSON</param>
        public RoutePermissionAttribute(string code, int id = 0, bool disabled = false, string extendJson = null)
            : base(id, disabled, extendJson)
        {
            this.code = code;
        }
    }
}
