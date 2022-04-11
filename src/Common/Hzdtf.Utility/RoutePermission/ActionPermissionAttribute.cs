using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.RoutePermission
{
    /// <summary>
    /// 动作权限属性
    /// @ 黄振东
    /// </summary>
    public class ActionPermissionAttribute : PermissionAttributeBase
    {
        /// <summary>
        /// 编码数组
        /// </summary>
        private readonly string[] codes;

        /// <summary>
        /// 编码数组
        /// </summary>
        public string[] Codes
        {
            get => codes;
        }

        /// <summary>
        /// 资源键
        /// </summary>
        private readonly string resourceKey;

        /// <summary>
        /// 资源键
        /// </summary>
        public string ResourceKey
        {
            get => resourceKey;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="codes">编码数组</param>
        /// <param name="id">ID</param>
        /// <param name="disabled">是否禁用</param>
        /// <param name="extendJson">扩展属性JSON</param>
        /// <param name="resourceKey">资源键</param>
        public ActionPermissionAttribute(string[] codes, int id = 0, bool disabled = false, string extendJson = null, string resourceKey = null)
            : base(id, disabled, extendJson)
        {
            this.codes = codes;
            this.resourceKey = resourceKey;
        }
    }
}
