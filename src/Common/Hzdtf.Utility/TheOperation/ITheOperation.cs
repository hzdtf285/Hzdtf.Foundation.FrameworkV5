using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.TheOperation
{
    /// <summary>
    /// 本次操作接口，通常是代表UI的一次一系列进行的操作
    /// 比如在UI上添加一个数据，后台里进行一系列的处理，由多个处理完成，则代表一个TheOperation
    /// @ 黄振东
    /// </summary>
    public interface ITheOperation
    {
        /// <summary>
        /// 事件ID
        /// </summary>
        string EventId { get; set; }
    }
}
