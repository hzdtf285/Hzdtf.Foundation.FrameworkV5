using Hzdtf.Utility.Model;
using Hzdtf.Workflow.Model;
using Hzdtf.Workflow.Model.Expand;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Workflow.Service.Contract.Engine
{
    /// <summary>
    /// 工作流审核接口
    /// @ 黄振东
    /// </summary>
    public partial interface IWorkflowAudit : IWorkflowEngine<FlowInInfo<FlowAuditInfo>>
    {
    }
}
