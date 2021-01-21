using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using Hzdtf.Workflow.Model;
using Hzdtf.Workflow.Model.Expand;
using Hzdtf.Workflow.Service.Contract.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Workflow.Service.Impl.Engine
{
    /// <summary>
    /// 工作流移除
    /// </summary>
    [Inject]
    public class WorkflowRemove : WorkflowRemoveBase, IWorkflowRemove
    {
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="workflow">工作流</param>
        /// <param name="currUser">当前用户</param>
        protected override void Vali(ReturnInfo<bool> returnInfo, WorkflowInfo workflow, BasicUserInfo<int> currUser = null)
        {
            var user = UserTool<int>.GetCurrUser(currUser);
            if (workflow.CreaterId != user.Id)
            {
                returnInfo.SetFailureMsg("Sorry，您不是此流程的发起者，故不能移除");

                return;
            }
            if (workflow.FlowStatus != FlowStatusEnum.DRAFT)
            {
                returnInfo.SetFailureMsg("Sorry，只有草稿状态才能移除");

                return;
            }
        }

        /// <summary>
        /// 获取移除类型
        /// </summary>
        /// <returns>移除类型</returns>
        protected override RemoveType GetRemoveType() => RemoveType.REMOVE;
    }
}
