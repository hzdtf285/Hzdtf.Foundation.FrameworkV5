using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    /// <summary>
    /// 异常扩展类
    /// @ 黄振东
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// 获取最里面的异常
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>最里面的异常</returns>
        public static Exception GetLastInnerException(this Exception ex)
        {
            if (ex == null || ex.InnerException == null)
            {
                return ex;
            }

            return GetLastInnerException(ex.InnerException);
        }
    }
}
