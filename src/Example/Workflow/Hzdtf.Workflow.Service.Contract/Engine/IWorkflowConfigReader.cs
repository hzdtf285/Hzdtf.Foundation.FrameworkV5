using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using Hzdtf.Workflow.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Workflow.Service.Contract.Engine
{
    /// <summary>
    /// 工作流配置读取接口
    /// @ 黄振东
    /// </summary>
    public partial interface IWorkflowConfigReader
    {
        /// <summary>
        /// 根据工作流定义ID读取工作流定义信息的所有配置
        /// </summary>
        /// <param name="workflowDefineId">工作流定义ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        ReturnInfo<WorkflowDefineInfo> ReaderAllConfig(int workflowDefineId, string connectionId = null, BasicUserInfo<int> currUser = null);

        /// <summary>
        /// 根据工作流编码读取工作流定义信息的所有配置
        /// </summary>
        /// <param name="workflowCode">工作流编码</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        ReturnInfo<WorkflowDefineInfo> ReaderAllConfig(string workflowCode, string connectionId = null, BasicUserInfo<int> currUser = null);
    }
}
