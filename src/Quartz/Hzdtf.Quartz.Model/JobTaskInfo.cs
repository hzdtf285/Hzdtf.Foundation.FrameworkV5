using Hzdtf.Utility.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MessagePack;
using Hzdtf.Utility.Utils;

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
        [Key("jtName")]
        [JsonProperty("jtName")]
        public string JtName
        {
            get;
            set;
        }

        /// <summary>
        /// 分组
        /// </summary>
        [Key("jtGroup")]
        [JsonProperty("jtGroup")]
        public string JtGroup
        {
            get;
            set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        [Key("jtDesc")]
        [JsonProperty("jtDesc")]
        public string JtDesc
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

        /// <summary>
        /// 是否匹配过滤器
        /// </summary>
        /// <param name="filter">过滤器，如果为null，则返回true</param>
        /// <param name="ignoreFilterPropNames">忽略过滤器的属性</param>
        /// <returns>是否匹配过滤器</returns>
        public bool EqualFilter(JobTaskFilterInfo filter, params string[] ignoreFilterPropNames)
        {
            if (filter == null)
            {
                return true;
            }

            if (!filter.Ids.IsNullOrLength0() && IsNeedFilter(ignoreFilterPropNames, "Ids") && !filter.Ids.Contains(Id))
            {
                return false;
            }
            if (!filter.JtNames.IsNullOrLength0() && IsNeedFilter(ignoreFilterPropNames, "JtNames") && !filter.JtNames.Contains(JtName, true))
            {
                return false;
            }
            if (!filter.JtGroups.IsNullOrLength0() && IsNeedFilter(ignoreFilterPropNames, "JtGroups") && !filter.JtGroups.Contains(JtGroup, true))
            {
                return false;
            }
            if (!filter.JobFullClasses.IsNullOrLength0() && IsNeedFilter(ignoreFilterPropNames, "JobFullClasses") && !filter.JobFullClasses.Contains(JobFullClass, true))
            {
                return false;
            }
            if (!filter.TriggerCrons.IsNullOrLength0() && IsNeedFilter(ignoreFilterPropNames, "TriggerCrons") && !filter.TriggerCrons.Contains(TriggerCron, true))
            {
                return false;
            }
            if (filter.SuccessedRemove != null && IsNeedFilter(ignoreFilterPropNames, "SuccessedRemove") && SuccessedRemove != filter.SuccessedRemove)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(filter.Keyword) && IsNeedFilter(ignoreFilterPropNames, "Keyword"))
            {
                if (string.IsNullOrWhiteSpace(JtName) && string.IsNullOrWhiteSpace(JtGroup))
                {
                    return false;
                }

                var isName = !string.IsNullOrWhiteSpace(JtName) && JtName.Contains(filter.Keyword);
                var isGroup = !string.IsNullOrWhiteSpace(JtGroup) && JtGroup.Contains(filter.Keyword);
                if (!(isName || isGroup))
                {
                    return false;
                }
            }

            InitCreateTime();
            InitModifyTime();
            if (filter.StartCreateTime != null && IsNeedFilter(ignoreFilterPropNames, "StartCreateTime") && CreateTime < filter.StartCreateTime)
            {
                return false;
            }
            if (filter.EndCreateTime != null && IsNeedFilter(ignoreFilterPropNames, "EndCreateTime") && CreateTime > filter.EndCreateTime)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 是否需要过滤
        /// </summary>
        /// <param name="ignoreFilterPropNames">忽略过滤器的属性</param>
        /// <param name="prop">属性名</param>
        /// <returns>是否需要过滤</returns>
        private bool IsNeedFilter(string[] ignoreFilterPropNames, string prop)
        {
            if (ignoreFilterPropNames.IsNullOrLength0())
            {
                return true;
            }

            return !ignoreFilterPropNames.Contains(prop);
        }

        /// <summary>
        /// 初始化创建时间，如果创建时间不是最小时间，则忽略
        /// </summary>
        public void InitCreateTime()
        {
            if (CreateTime != DateTime.MinValue)
            {
                CreateTime = DateTime.Now;
            }
        }

        /// <summary>
        /// 初始化修改时间，如果修改时间不是最小时间，则忽略
        /// </summary>
        public void InitModifyTime()
        {
            if (ModifyTime != DateTime.MinValue)
            {
                ModifyTime = DateTime.Now;
            }
        }
    }
}
