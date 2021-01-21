using Hzdtf.Utility.Model;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Workflow.Model.Expand
{
    /// <summary>
    /// 具体关卡信息
    /// </summary>
    [MessagePackObject]
    public partial class ConcreteCensorshipInfo : CodeNameInfo<int>
    {
        /// <summary>
        /// 流程关卡
        /// </summary>
        [JsonProperty("flowCensorship")]
        [MessagePack.Key("flowCensorship")]
        public FlowCensorshipInfo FlowCensorship
        {
            get;
            set;
        }

        /// <summary>
        /// 工作流处理者数组
        /// </summary>
        [JsonProperty("workflowHandles")]
        [MessagePack.Key("workflowHandles")]
        public WorkflowHandleInfo[] WorkflowHandles
        {
            get;
            set;
        }
    }
}
