using Hzdtf.Utility.Attr.ParamAttr;
using Hzdtf.Utility.Enums;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using Hzdtf.Workflow.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Workflow.Service.Impl
{
    /// <summary>
    /// 流程关卡服务接口
    /// @ 黄振东
    /// </summary>
    public partial class FlowCensorshipService
    {
        /// <summary>
        /// 根据流程ID查询流程关卡列表
        /// </summary>
        /// <param name="flowId">流程ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<IList<FlowCensorshipInfo>> QueryByFlowId([DisplayName2("流程ID"), Id] int flowId, string connectionId = null, BasicUserInfo<int> currUser = null)
        {
            return ExecReturnFuncAndConnectionId<IList<FlowCensorshipInfo>>((reInfo, connId) =>
            {
                return Persistence.SelectByFlowId(flowId, connId);
            }, null, connectionId, AccessMode.SLAVE);
        }
    }
}
