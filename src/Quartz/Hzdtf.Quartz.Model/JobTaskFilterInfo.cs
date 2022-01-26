using Hzdtf.Utility.Model;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Quartz.Model
{
    /// <summary>
    /// 作业任务过滤信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class JobTaskFilterInfo : KeywordFilterInfo
    {
        /// <summary>
        /// ID数组
        /// </summary>
        [JsonProperty("ids")]
        [MessagePack.Key("ids")]
        public int[] Ids
        {
            get;
            set;
        }

        /// <summary>
        /// 名称数组
        /// </summary>
        [JsonProperty("jtNames")]
        [MessagePack.Key("jtNames")]
        public string[] JtNames
        {
            get;
            set;
        }

        /// <summary>
        /// 分组数组
        /// </summary>
        [JsonProperty("jtGroups")]
        [MessagePack.Key("jtGroups")]
        public string[] JtGroups
        {
            get;
            set;
        }

        /// <summary>
        /// 作业全路径数组
        /// </summary>
        [JsonProperty("jobFullClasses")]
        [MessagePack.Key("jobFullClasses")]
        public string[] JobFullClasses
        {
            get;
            set;
        }

        /// <summary>
        /// 触发器表达式数组
        /// </summary>
        [JsonProperty("triggerCrons")]
        [MessagePack.Key("triggerCrons")]
        public string[] TriggerCrons
        {
            get;
            set;
        }

        /// <summary>
        /// 成功后移除
        /// </summary>
        [JsonProperty("successedRemove")]
        [MessagePack.Key("successedRemove")]
        public bool? SuccessedRemove
        {
            get;
            set;
        }
    }
}
