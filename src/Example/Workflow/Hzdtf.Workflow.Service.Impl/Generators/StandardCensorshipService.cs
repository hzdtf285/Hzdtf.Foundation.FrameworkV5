using Hzdtf.Workflow.Model;
using Hzdtf.Workflow.Persistence.Contract;
using Hzdtf.Workflow.Service.Contract;
using Hzdtf.Service.Impl;
using Hzdtf.Utility.Attr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Workflow.Service.Impl
{
    /// <summary>
    /// 标准关卡服务
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class StandardCensorshipService : ServiceBase<int, StandardCensorshipInfo, IStandardCensorshipPersistence>, IStandardCensorshipService
    {
    }
}
