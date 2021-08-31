using Hzdtf.Quartz.Extensions.Model;
using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Quartz.Extensions.Data
{
    /// <summary>
    /// 触发器数据Json
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class TriggerDataJson : IReaderAll<TriggerInfo>
    {
        /// <summary>
        /// 列表
        /// </summary>
        private readonly IList<TriggerInfo> list;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="jsonFile">JSON文件</param>
        public TriggerDataJson(string jsonFile = null)
        {
            if (string.IsNullOrWhiteSpace(jsonFile))
            {
                jsonFile = $"{AppContext.BaseDirectory}/Config/Quartz/triggerConfig.json";
            }

            list = jsonFile.ToJsonObjectFromFile<IList<TriggerInfo>>();
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public IList<TriggerInfo> ReaderAll()
        {
            return list;
        }
    }
}
