using Hzdtf.Utility.Model;
using Hzdtf.Workflow.Model.Expand;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Workflow.Service.Contract.Engine
{
    /// <summary>
    /// 流程表单接口
    /// @ 黄振东
    /// </summary>
    public interface IWorkflowForm : IWorkflowEngine<FlowInInfo<FlowInitInfo<PersonTimeInfo<int>>>>
    {
    }
}
