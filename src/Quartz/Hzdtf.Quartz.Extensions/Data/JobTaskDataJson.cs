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
    /// 作业任务数据Json
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class JobTaskDataJson : IReaderAll<JobTaskInfo>
    {
        /// <summary>
        /// 列表
        /// </summary>
        private readonly IList<JobTaskInfo> list;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="jsonFile">JSON文件</param>
        public JobTaskDataJson(string jsonFile = null)
        {
            if (string.IsNullOrWhiteSpace(jsonFile))
            {
                jsonFile = $"{AppContext.BaseDirectory}/Config/Quartz/jobTaskConfig.json";
            }

            list = jsonFile.ToJsonObjectFromFile<IList<JobTaskInfo>>();
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public IList<JobTaskInfo> ReaderAll()
        {
            return list;
        }
    }
}
