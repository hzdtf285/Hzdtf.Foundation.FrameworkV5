using System;
using System.Threading;

namespace Hzdtf.Utility.TheOperation
{
    /// <summary>
    /// 本次操作
    /// 使用AsyncLocal进行存储，在本次操作范围内，最好不要跨线程，因为AsyncLocal是线程不安全
    /// @ 黄振东
    /// </summary>
    public class TheOperation : ITheOperation
    {
        #region 属性与字段

        /// <summary>
        /// 本地缓存
        /// </summary>
        private static readonly AsyncLocal<string> local = new AsyncLocal<string>();

        #endregion

        #region ITheOperation 接口

        /// <summary>
        /// 事件ID
        /// </summary>
        public virtual string EventId
        {
            get
            {
                return local.Value; 
            }
            set
            {
                local.Value = value; 
            }
        }

        #endregion
    }
}
