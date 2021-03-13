using Hzdtf.Utility.Utils;
using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using Hzdtf.Workflow.Model.Expand;
using Hzdtf.Workflow.Service.Contract.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Workflow.Service.Impl.Engine
{
    /// <summary>
    /// 工作流表单服务
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class WorkflowFormService : IWorkflowFormService
    {
        /// <summary>
        /// 工作流申请
        /// </summary>
        public IWorkflowApply WorkflowApply
        {
            get;
            set;
        }

        /// <summary>
        /// 工作流保存
        /// </summary>
        public IWorkflowSave WorkflowSave
        {
            get;
            set;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <typeparam name="FormT">表单类型</typeparam>
        /// <param name="flowInit">流程初始</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<WorkflowBasicInfo> Save<FormT>(FlowInitInfo<FormT> flowInit, CommonUseData comData = null)
            where FormT : PersonTimeInfo<int> => Execute(flowInit, WorkflowSave, comData);

        /// <summary>
        /// 申请
        /// </summary>
        /// <typeparam name="FormT">表单类型</typeparam>
        /// <param name="flowInit">流程初始</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<WorkflowBasicInfo> Apply<FormT>(FlowInitInfo<FormT> flowInit, CommonUseData comData = null)
            where FormT : PersonTimeInfo<int> => Execute(flowInit, WorkflowApply, comData);

        /// <summary>
        /// 执行
        /// </summary>
        /// <typeparam name="FormT">表单类型</typeparam>
        /// <param name="flowInit">流程初始</param>
        /// <param name="workflowInit">工作流初始</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        private ReturnInfo<WorkflowBasicInfo> Execute<FormT>(FlowInitInfo<FormT> flowInit, IWorkflowForm workflowInit, CommonUseData comData = null)
            where FormT : PersonTimeInfo<int>
        {
            var returnInfo = new ReturnInfo<WorkflowBasicInfo>();
            var currUser = UserTool<int>.GetCurrUser(comData);
            if (string.IsNullOrWhiteSpace(flowInit.ApplyNo))
            {
                flowInit.ApplyNo = BuilderApplyNo(flowInit, returnInfo, comData);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }
            }

            var flowInfo = flowInit.ToFlowIn();
            ReturnInfo<bool> reWorkflow = workflowInit.Execute(flowInfo, comData);
            returnInfo.FromBasic(reWorkflow);
            if (reWorkflow.Failure())
            {
                return returnInfo;
            }

            returnInfo.Data = new WorkflowBasicInfo()
            {
                Id = flowInit.Id,
                ApplyNo = flowInit.ApplyNo
            };

            return returnInfo;
        }

        /// <summary>
        /// 生成申请单号
        /// </summary>
        /// <typeparam name="FormT">表单类型</typeparam>
        /// <param name="flowInit">流程初始化</param>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="comData">通用数据</param>
        /// <returns>申请单号</returns>
        protected virtual string BuilderApplyNo<FormT>(FlowInitInfo<FormT> flowInit, ReturnInfo<WorkflowBasicInfo> returnInfo, CommonUseData comData = null)
            where FormT : PersonTimeInfo<int> => DateTime.Now.ToLongDateTimeNumString();
    }
}
