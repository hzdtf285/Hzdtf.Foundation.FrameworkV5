using Hzdtf.BasicFunction.Service.Contract.Expand.Sequence;
using Hzdtf.Utility.Attr;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Utils;
using Hzdtf.Utility.Model;

namespace Hzdtf.BasicFunction.Service.Impl.Expand.Sequence
{
    /// <summary>
    /// 日期序列规则
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class DateSequenceRule : ISequenceRule
    {
        /// <summary>
        /// 生成序列号
        /// </summary>
        /// <param name="seqType">序列类型</param>
        /// <param name="noLength">序列号长度</param>
        /// <param name="increment">增量</param>
        /// <param name="comData">通用数据</param>
        /// <returns>序列号</returns>
        public string BuilderNo(string seqType, byte noLength, int increment, CommonUseData comData = null)
        {
            string dateStr = DateTime.Now.ToCompactFixedShortDate();
            StringBuilder result = new StringBuilder($"{seqType}{dateStr}");
            int existsLength = result.Length;
            int length = noLength - existsLength - increment.ToString().Length;
            for (var i = 0; i < length; i++)
            {
                result.Append("0");
            }
            result.Append(increment.ToString());

            return result.ToString();
        }
    }
}
