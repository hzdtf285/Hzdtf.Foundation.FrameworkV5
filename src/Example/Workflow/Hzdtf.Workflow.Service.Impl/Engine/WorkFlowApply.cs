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
    /// 工作流申请
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class WorkflowApply : WorkflowFormBase, IWorkflowApply
    {
        /// <summary>
        /// 追加设置查找流程关卡输入信息
        /// </summary>
        /// <param name="flowIn">流程输入</param>
        /// <param name="findFlowCensorshipIn">查找流程关卡输入信息</param>
        /// <param name="currUser">当前用户</param>
        protected override void AppendSetFindFlowCensorshipIn(FlowInInfo<FlowInitInfo<PersonTimeInfo<int>>> flowIn, FlowCensorshipInInfo findFlowCensorshipIn, BasicUserInfo<int> currUser = null)
        {
            base.AppendSetFindFlowCensorshipIn(flowIn, findFlowCensorshipIn, currUser);
            findFlowCensorshipIn.ActionType = ActionType.SEND;
        }
    }
}
