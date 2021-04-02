using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// 首位数据模型
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class ScalarModel<T>
    {
        /// <summary>
        /// 结果
        /// </summary>
        public T Result
        {
            get;
            set;
        }
    }
}
