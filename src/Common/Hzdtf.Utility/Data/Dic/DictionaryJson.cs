using System;
using System.Collections.Generic;
using System.IO;

namespace Hzdtf.Utility.Data.Dic
{
    /// <summary>
    /// 字典Json
    /// @ 黄振东
    /// </summary>
    public class DictionaryJson : IReader<IDictionary<string, string>>
    {
        /// <summary>
        /// Json文件
        /// </summary>
        private readonly string jsonFile;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="jsonFile">Json文件</param>
        public DictionaryJson(string jsonFile) => this.jsonFile = jsonFile;

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public IDictionary<string, string> Reader()
        {
            return jsonFile.ToJsonObjectFromFile<IDictionary<string, string>>();
        }
    }
}
