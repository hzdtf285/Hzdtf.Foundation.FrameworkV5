using Hzdtf.Utility.Attr;
using Hzdtf.Workflow.Model.Expand;
using Hzdtf.Workflow.Service.Contract.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Workflow.Service.Impl.Engine
{
    /// <summary>
    /// 工作流强制移除
    /// </summary>
    [Inject]
    public class WorkflowForceRemove : WorkflowRemoveBase, IWorkflowForceRemove
    {
        /// <summary>
        /// 获取移除类型
        /// </summary>
        /// <returns>移除类型</returns>
        protected override RemoveType GetRemoveType() => RemoveType.FORCE_REMOVE;
    }
}
