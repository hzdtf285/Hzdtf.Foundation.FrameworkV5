using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Model;
using Hzdtf.Workflow.Model.Expand;
using Hzdtf.Workflow.Model.Expand.Diversion;
using Hzdtf.Workflow.Service.Contract.Engine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Workflow.Service.Impl.Engine
{
    /// <summary>
    /// 工作流保存
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class WorkflowSave : WorkflowFormBase, IWorkflowSave
    {
        /// <summary>
        /// 追加设置查找流程关卡输入信息
        /// </summary>
        /// <param name="flowIn">流程输入</param>
        /// <param name="findFlowCensorshipIn">查找流程关卡输入信息</param>
        /// <param name="comData">通用数据</param>
        protected override void AppendSetFindFlowCensorshipIn(FlowInInfo<FlowInitInfo<PersonTimeInfo<int>>> flowIn, FlowCensorshipInInfo findFlowCensorshipIn, CommonUseData comData = null)
        {
            base.AppendSetFindFlowCensorshipIn(flowIn, findFlowCensorshipIn, comData);
            findFlowCensorshipIn.ActionType = ActionType.SAVE;
        }
    }
}
