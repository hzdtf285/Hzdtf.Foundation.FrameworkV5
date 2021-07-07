using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace System
{
    /// <summary>
    /// JSON扩展类
    /// @ 黄振东
    /// </summary>
    public static class JsonExtensions
    {
        /// <summary>
        /// 将对象转换为JSON字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="isIgnoreNull">是否忽略null值，默认为是</param>
        /// <param name="isUseCamel">是否使用驼蜂风格（即首字母小写），默认为是</param>
        /// <param name="format">格式化，默认为无</param>
        /// <returns>JSON字符串</returns>
        public static string ToJsonString(this object obj, bool isIgnoreNull = true, bool isUseCamel = true, Formatting format = Formatting.None)
        {
            if (obj == null)
            {
                return null;
            }
            var jSetting = new JsonSerializerSettings();
            if (isIgnoreNull)
            {
                jSetting.NullValueHandling = NullValueHandling.Ignore;
            }
            if (isUseCamel)
            {
                jSetting.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            return JsonConvert.SerializeObject(obj, format, jSetting);
        }

        /// <summary>
        /// 将JSON字符串转换为JSON对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="jsonString">JSON字符串</param>
        /// <returns>JSON对象</returns>
        public static T ToJsonObject<T>(this string jsonString)
        {
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        /// <summary>
        /// 将JSON字符串转换为JSON对象
        /// </summary>
        /// <param name="jsonString">JSON字符串</param>
        /// <param name="type">类型</param>
        /// <returns>JSON对象</returns>
        public static object ToJsonObject(this string jsonString, Type type)
        {
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                return null;
            }

            return JsonConvert.DeserializeObject(jsonString, type);
        }

        /// <summary>
        /// 从文件里转换为JSON对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="fileName">文件名</param>
        /// <returns>JSON对象</returns>
        public static T ToJsonObjectFromFile<T>(this string fileName) => ReaderFileContent(fileName, str => ToJsonObject<T>(str));

        /// <summary>
        /// 从文件里转换为JSON对象
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="type">类型</param>
        /// <returns>JSON对象</returns>
        public static object ToJsonObjectFromFile(this string fileName, Type type) => ReaderFileContent(fileName, str => ToJsonObject(str, type));

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="fileName">文件名</param>
        /// <param name="action">读取完内容后回调</param>
        /// <returns>对象</returns>
        private static T ReaderFileContent<T>(string fileName, Func<string, T> action)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return default(T);
            }

            return action(File.ReadAllText(fileName, Encoding.UTF8));
        }
    }
}
