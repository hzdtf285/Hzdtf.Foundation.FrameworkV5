using Hzdtf.Utility.Attr;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Conversion
{
    /// <summary>
    /// JSON转换类型值
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class JsonConvertTypeValue : ConvertTypeValueBase
    {
        /// <summary>
        /// 转换新值
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="targetType">目标类型</param>
        /// <returns>新值</returns>
        protected override object ToNew(object value, Type targetType) => value.ToJsonString().ToJsonObject(targetType);
    }
}
