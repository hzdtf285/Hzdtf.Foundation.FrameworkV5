using Hzdtf.Utility.Model;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Workflow.Model.Expand
{
    /// <summary>
    /// 流程输入信息
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="FlowT">流程类型</typeparam>
    [MessagePackObject]
    public class FlowInInfo<FlowT>
    {
        /// <summary>
        /// 流程
        /// </summary>
        [JsonProperty("flow")]
        [MessagePack.Key("flow")]
        public FlowT Flow
        {
            get;
            set;
        }

        /// <summary>
        /// 表单
        /// </summary>
        [JsonProperty("form")]
        [MessagePack.Key("form")]
        public PersonTimeInfo<int> Form
        {
            get;
            set;
        }
    }
}
