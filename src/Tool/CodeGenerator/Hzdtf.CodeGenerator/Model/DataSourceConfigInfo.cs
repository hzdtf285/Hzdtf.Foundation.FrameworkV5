using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.CodeGenerator.Model
{
    /// <summary>
    /// 数据源配置信息
    /// @ 黄振东
    /// </summary>
    public class DataSourceConfigInfo
    {
        /// <summary>
        /// 数据源
        /// </summary>
        public Datasource DataSource { get; set; }

        /// <summary>
        /// 生成项
        /// </summary>
        public Builderitem BuilderItem { get; set; }

        /// <summary>
        /// 数据源
        /// @ 黄振东
        /// </summary>
        public class Datasource
        {
            /// <summary>
            /// 类型
            /// </summary>
            public string Type { get; set; }

            /// <summary>
            /// 类型
            /// </summary>
            public string Host { get; set; }

            /// <summary>
            /// 端口
            /// </summary>
            public int Port { get; set; }

            /// <summary>
            /// 数据库
            /// </summary>
            public string Db { get; set; }

            /// <summary>
            /// 用户
            /// </summary>
            public string User { get; set; }

            /// <summary>
            /// 密码
            /// </summary>
            public string Password { get; set; }
        }

        /// <summary>
        /// 生成项
        /// @ 黄振东
        /// </summary>
        public class Builderitem
        {
            /// <summary>
            /// 命名空间
            /// </summary>
            public string Namespace { get; set; }

            /// <summary>
            /// 主键类型
            /// </summary>
            public string PkType { get; set; }

            /// <summary>
            /// 是否租户
            /// </summary>
            public bool IsTenant { get; set; }
        }
    }
}
