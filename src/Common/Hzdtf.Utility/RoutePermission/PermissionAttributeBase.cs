using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.RoutePermission
{
    /// <summary>
    /// 权限属性基类
    /// @ 黄振东
    /// </summary>
    public abstract class PermissionAttributeBase : Attribute
    {
        /// <summary>
        /// ID
        /// </summary>
        private readonly int id;

        /// <summary>
        /// ID
        /// </summary>
        public int Id
        {
            get => id;
        }

        /// <summary>
        /// 是否禁用
        /// </summary>
        private readonly bool disabled;

        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool Disabled
        {
            get => disabled;
        }

        /// <summary>
        /// 扩展属性
        /// </summary>
        private readonly IDictionary<string, string> extend;

        /// <summary>
        /// 扩展属性
        /// </summary>
        public IDictionary<string, string> Extend
        {
            get => extend;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="disabled">是否禁用</param>
        /// <param name="extendJson">扩展属性JSON</param>
        public PermissionAttributeBase(int id = 0, bool disabled = false, string extendJson = null)
        {
            this.id = id;
            this.disabled = disabled;
            if (string.IsNullOrWhiteSpace(extendJson))
            {
                return;
            }

            this.extend = extendJson.ToJsonObject<IDictionary<string, string>>();
        }
    }
}
