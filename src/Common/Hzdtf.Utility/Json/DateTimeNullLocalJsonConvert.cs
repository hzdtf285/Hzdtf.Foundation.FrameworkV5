using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hzdtf.Utility.Json
{
    /// <summary>
    /// 日期时间Null本地Json转换
    /// @ 黄振东
    /// </summary>
    public class DateTimeNullLocalJsonConvert : JsonConverter<DateTime?>
    {
        /// <summary>
        /// 读取日期时间
        /// </summary>
        /// <param name="reader">读取</param>
        /// <param name="typeToConvert">类型转换</param>
        /// <param name="options">JSON配置选项</param>
        /// <returns>日期时间</returns>
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var str = reader.GetString();
            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }

            return DateTime.Parse(str);
        }

        /// <summary>
        /// 写入日期时间
        /// </summary>
        /// <param name="writer">写入</param>
        /// <param name="value">值</param>
        /// <param name="options">JSON配置选项</param>
        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                return;
            }

            writer.WriteStringValue(value.Value);
        }
    }
}
