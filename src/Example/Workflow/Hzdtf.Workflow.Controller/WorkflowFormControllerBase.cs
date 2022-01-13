using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Factory;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using Hzdtf.Workflow.Model.Expand;
using Hzdtf.Workflow.Service.Contract.Engine;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Workflow.Controller
{
    /// <summary>
    /// 工作流表单控制器基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="FormT">表单类型</typeparam>
    public abstract class WorkflowFormControllerBase<FormT> : ControllerBase
        where FormT : PersonTimeInfo<int>
    {
        /// <summary>
        /// 工作流初始服务
        /// </summary>
        public IWorkflowFormService WorkflowInitService
        {
            get;
            set;
        }

        /// <summary>
        /// 工作流移除
        /// </summary>
        public IWorkflowRemove WorkflowRemove
        {
            get;
            set;
        }

        /// <summary>
        /// 工作流强制移除
        /// </summary>
        public IWorkflowForceRemove WorkflowForceRemove
        {
            get;
            set;
        }

        /// <summary>
        /// 工作流撤消
        /// </summary>
        public IWorkflowUndo WorkflowUndo
        {
            get;
            set;
        }

        /// <summary>
        /// 表单数据读取
        /// </summary>
        public IFormDataReader<FormT> FormDataReader
        {
            get;
            set;
        }

        /// <summary>
        /// 通用数据工厂
        /// </summary>
        public ISimpleFactory<HttpContext, CommonUseData> ComDataFactory
        {
            get;
            set;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="flowInit">流程初始</param>
        /// <returns>返回信息</returns>
        [HttpPost("Save")]
        public virtual ReturnInfo<WorkflowBasicInfo> Save(FlowInitInfo<FormT> flowInit)
        {
            var comData = HttpContext.CreateCommonUseData(ComDataFactory, functionCodes: FunCodeDefine.SAVE_CODE);
            return Execute(flowInit, () =>
            {
                return WorkflowInitService.Save(flowInit, comData);
            });
        }

        /// <summary>
        /// 申请
        /// </summary>
        /// <param name="flowInit">流程初始</param>
        /// <returns>返回信息</returns>
        [HttpPost("Apply")]
        public virtual ReturnInfo<WorkflowBasicInfo> Apply(FlowInitInfo<FormT> flowInit)
        {
            var comData = HttpContext.CreateCommonUseData(ComDataFactory, functionCodes: FunCodeDefine.APPLY);
            return Execute(flowInit, () =>
            {
                return WorkflowInitService.Apply(flowInit, comData);
            });
        }

        /// <summary>
        /// 根据工作流ID移除
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <returns>返回信息</returns>
        [HttpDelete("RemoveByWorkflowId")]
        public virtual ReturnInfo<bool> RemoveByWorkflowId(int workflowId) => WorkflowRemove.Execute(workflowId, HttpContext.CreateCommonUseData(ComDataFactory, functionCodes: FunCodeDefine.REMOVE_CODE));

        /// <summary>
        /// 根据工作流ID强制移除
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <returns>返回信息</returns>
        [HttpDelete("ForceRemoveByWorkflowId")]
        public virtual ReturnInfo<bool> ForceRemoveByWorkflowId(int workflowId) => WorkflowForceRemove.Execute(workflowId, HttpContext.CreateCommonUseData(ComDataFactory, functionCodes: FunCodeDefine.REMOVE_CODE));

        /// <summary>
        /// 根据工作流ID撤消
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <returns>返回信息</returns>
        [HttpDelete("UndoByWorkflowId")]
        public virtual ReturnInfo<bool> UndoByWorkflowId(int workflowId) => WorkflowUndo.Execute(workflowId, HttpContext.CreateCommonUseData(ComDataFactory, functionCodes: FunCodeDefine.UNDO));

        /// <summary>
        /// 根据工作流ID获取表单明细
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <returns>返回信息</returns>
        [HttpGet("GetFormDetail")]
        public virtual ReturnInfo<FormT> GetFormDetail(int workflowId)
        {
            return FormDataReader.ReaderByWorkflowId(workflowId, HttpContext.CreateCommonUseData(ComDataFactory, functionCodes: FunCodeDefine.QUERY_CODE));
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="flowInit">流程初始</param>
        /// <param name="func">回调执行</param>
        /// <returns>返回信息</returns>
        private ReturnInfo<WorkflowBasicInfo> Execute(FlowInitInfo<FormT> flowInit, Func<ReturnInfo<WorkflowBasicInfo>> func)
        {
            if (flowInit == null)
            {
                var returnInfo = new ReturnInfo<string>();

                returnInfo.SetFailureMsg("流程表单数据不能为null");
            }

            flowInit.WorkflowCode = GetWorkflowCode();

            SetFlowInitPropertys(flowInit);

            return func();
        }

        /// <summary>
        /// 获取工作流编码
        /// </summary>
        /// <returns>工作流编码</returns>
        protected abstract string GetWorkflowCode();

        /// <summary>
        /// 设置流程初始化属性
        /// </summary>
        /// <param name="flowInit">流程初始</param>
        protected virtual void SetFlowInitPropertys(FlowInitInfo<FormT> flowInit) { }
    }
}
