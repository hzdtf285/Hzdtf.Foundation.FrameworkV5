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
    /// 退件流程路线服务
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class ReturnFlowRouteService : ServiceBase<int, ReturnFlowRouteInfo, IReturnFlowRoutePersistence>, IReturnFlowRouteService
    {
    }
}
