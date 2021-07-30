using Hzdtf.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.Model
{
    /// <summary>
    /// SQL属性信息
    /// @ 黄振东
    /// </summary>
    public class SqlPropInfo
    {
        /// <summary>
        /// 前面几条，如果为0，则不加此条件
        /// </summary>
        public uint Top
        {
            get;
            set;
        }

        /// <summary>
        /// 追加租戶ID，默认值为“默认”
        /// </summary>
        public BooleanType AppendTenantId
        {
            get;
            set;
        } = BooleanType.DEFAULT;

        /// <summary>
        /// 属性名称集合，如果为空则获取全部
        /// </summary>
        public string[] PropertyNames
        {
            get;
            set;
        }

        /// <summary>
        /// 条件SQL，不包含where
        /// 如果有包含属性，则用格式（区分大小写）：|/属性名/|
        /// </summary>
        public string WhereSql
        {
            get;
            set;
        }

        /// <summary>
        /// 封装属性名
        /// </summary>
        /// <param name="propName">属性名</param>
        /// <returns>封装后的属性名</returns>
        public static string PackPropName(string propName)
        {
            return string.Format("|/{0}/|", propName);
        }
    }
}
