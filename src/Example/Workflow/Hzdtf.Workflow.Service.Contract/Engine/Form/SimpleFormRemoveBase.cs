using Hzdtf.Utility.Model.Return;
using Hzdtf.Workflow.Model;
using Hzdtf.Workflow.Model.Expand;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Utils;
using Hzdtf.Utility.Model;
using System.Reflection;

namespace Hzdtf.Workflow.Service.Contract.Engine.Form
{
    /// <summary>
    /// 简单表单移除基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="ConcreteFormInfoT">具体表单信息类型</typeparam>
    public abstract class SimpleFormRemoveBase<ConcreteFormInfoT> : BasicFormRemoveBase
        where ConcreteFormInfoT : ConcreteFormInfo
    {
        /// <summary>
        /// 表单服务
        /// </summary>
        public IFormService<ConcreteFormInfoT> FormService
        {
            get;
            set;
        }

        /// <summary>
        /// 执行流程后
        /// </summary>
        /// <param name="workflow">工作流</param>
        /// <param name="removeType">移除类型</param>
        /// <param name="isSuccess">是否成功</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        public override ReturnInfo<bool> AfterExecFlow(WorkflowInfo workflow, RemoveType removeType, bool isSuccess, CommonUseData comData = null, string connectionId = null)
        {
            if (isSuccess)
            {
                var currUser = UserTool<int>.GetCurrUser(comData);
                switch (removeType)
                {
                    case RemoveType.REMOVE:
                    case RemoveType.FORCE_REMOVE:

                        return FormService.RemoveByWorkflowId(workflow.Id, connectionId : connectionId, comData: comData);

                    case RemoveType.UNDO:
                        ConcreteFormInfoT form = typeof(ConcreteFormInfoT).CreateInstance<ConcreteFormInfoT>();
                        form.WorkflowId = workflow.Id;
                        form.FlowStatus = FlowStatusEnum.REVERSED;
                        form.SetModifyInfo(currUser);

                        return FormService.ModifyFlowStatusByWorkflowId(form, connectionId : connectionId, comData: comData);
                }
            }

            return new ReturnInfo<bool>();
        }
    }
}
