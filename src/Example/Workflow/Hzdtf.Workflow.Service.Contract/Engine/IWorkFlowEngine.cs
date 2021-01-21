using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using Hzdtf.Workflow.Model.Expand;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Workflow.Service.Contract.Engine
{
    /// <summary>
    /// 工作流引擎接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="FlowInT">流程输入类型</typeparam>
    public partial interface IWorkflowEngine<FlowInT>
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="flowIn">流程输入</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> Execute(FlowInT flowIn, string connectionId = null, BasicUserInfo<int> currUser = null);
    }
}
