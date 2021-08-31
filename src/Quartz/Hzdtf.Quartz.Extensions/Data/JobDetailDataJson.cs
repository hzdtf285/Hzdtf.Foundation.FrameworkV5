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
    /// 作业明细数据Json
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class JobDetailDataJson : IReaderAll<JobDetailInfo>
    {
        /// <summary>
        /// 列表
        /// </summary>
        private readonly IList<JobDetailInfo> list;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="jsonFile">JSON文件</param>
        public JobDetailDataJson(string jsonFile = null)
        {
            if (string.IsNullOrWhiteSpace(jsonFile))
            {
                jsonFile = $"{AppContext.BaseDirectory}/Config/Quartz/jobDetailConfig.json";
            }

            list = jsonFile.ToJsonObjectFromFile<IList<JobDetailInfo>>();
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public IList<JobDetailInfo> ReaderAll()
        {
            return list;
        }
    }
}
