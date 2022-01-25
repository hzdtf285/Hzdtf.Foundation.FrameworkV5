using Hzdtf.Utility.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MessagePack;

namespace Hzdtf.Quartz.Model
{
    /// <summary>
    /// 作业任务信息
    /// 由作业明细和触发器组成
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class JobTaskInfo : TimeInfo<int>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Key("name")]
        [JsonProperty("name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 分组
        /// </summary>
        [Key("name")]
        [JsonProperty("group")]
        public string Group
        {
            get;
            set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        [Key("description")]
        [JsonProperty("description")]
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// 作业全路径类
        /// </summary>
        [Key("jobFullClass")]
        [JsonProperty("jobFullClass")]
        public string JobFullClass
        {
            get;
            set;
        }

        /// <summary>
        /// 作业参数字典
        /// </summary>
        [Key("jobParams")]
        [JsonProperty("jobParams")]
        public IDictionary<string, string> JobParams
        {
            get;
            set;
        }

        /// <summary>
        /// 作业参数JSON字符串
        /// </summary>
        [Key("jobParamsJsonString")]
        [JsonProperty("jobParamsJsonString")]
        public string JobParamsJsonString
        {
            get;
            set;
        }

        /// <summary>
        /// 触发器表达式
        /// </summary>
        [Key("triggerCron")]
        [JsonProperty("triggerCron")]
        public string TriggerCron
        {
            get;
            set;
        }

        /// <summary>
        /// 触发器参数字典
        /// </summary>
        [Key("triggerParams")]
        [JsonProperty("triggerParams")]
        public IDictionary<string, string> TriggerParams
        {
            get;
            set;
        }

        /// <summary>
        /// 触发器参数JSON字符串
        /// </summary>
        [Key("triggerParamsJsonString")]
        [JsonProperty("triggerParamsJsonString")]
        public string TriggerParamsJsonString
        {
            get;
            set;
        }

        /// <summary>
        /// 执行成功后移除任务
        /// 默认为否
        /// </summary>
        [Key("successedRemove")]
        [JsonProperty("successedRemove")]
        public bool SuccessedRemove
        {
            get;
            set;
        }
    }
}
