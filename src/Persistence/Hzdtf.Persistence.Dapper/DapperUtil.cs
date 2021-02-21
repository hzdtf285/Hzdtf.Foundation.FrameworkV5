using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Persistence.Dapper
{
    /// <summary>
    /// Dapper辅助类
    /// @ 黄振东
    /// </summary>
    public static class DapperUtil
    {
        /// <summary>
        /// 合并参数
        /// </summary>
        /// <param name="param1">参数1</param>
        /// <param name="param2">参数2</param>
        /// <returns>合并后的参数</returns>
        public static DynamicParameters MergeParams(DynamicParameters param1, DynamicParameters param2)
        {
            if (param1 == null && param2 == null)
            {
                return null;
            }
            if (param1 != null && param2 == null)
            {
                return param1;
            }
            if (param2 != null && param1 == null)
            {
                return param2;
            }

            var names = param2.ParameterNames;
            if (names == null)
            {
                return param1;
            }
            foreach (var name in names)
            {
                param1.Add(name, param2.Get<object>(name));
            }

            return param1;
        }
    }
}
