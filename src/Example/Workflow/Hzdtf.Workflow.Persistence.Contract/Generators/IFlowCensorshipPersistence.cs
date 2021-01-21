using Hzdtf.Persistence.Contract.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Workflow.Model;

namespace Hzdtf.Workflow.Persistence.Contract
{
    /// <summary>
    /// 流程关卡持久化接口
    /// @ 黄振东
    /// </summary>
    public partial interface IFlowCensorshipPersistence : IPersistence<int, FlowCensorshipInfo>
    {
    }
}
