using Hzdtf.Persistence.Contract.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Workflow.Model;

namespace Hzdtf.Workflow.Persistence.Contract
{
    /// <summary>
    /// 标准关卡持久化接口
    /// @ 黄振东
    /// </summary>
    public partial interface IStandardCensorshipPersistence : IPersistence<int, StandardCensorshipInfo>
    {
    }
}
