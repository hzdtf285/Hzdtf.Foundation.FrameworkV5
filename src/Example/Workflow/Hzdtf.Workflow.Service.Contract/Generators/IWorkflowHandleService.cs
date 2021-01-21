using Hzdtf.Workflow.Model;
using Hzdtf.Service.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Workflow.Service.Contract
{
    /// <summary>
    /// 工作流处理服务接口
    /// @ 黄振东
    /// </summary>
    public partial interface IWorkflowHandleService : IService<int, WorkflowHandleInfo>
    {
    }
}
