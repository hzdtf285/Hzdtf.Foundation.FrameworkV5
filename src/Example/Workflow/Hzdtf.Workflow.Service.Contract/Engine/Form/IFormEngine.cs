using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using Hzdtf.Workflow.Model.Expand.Diversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Workflow.Service.Contract.Engine.Form
{
    /// <summary>
    /// 表单引擎接口
    /// @ 黄振东
    /// </summary>
    public partial interface IFormEngine
    {
        /// <summary>
        /// 执行流程前
        /// </summary>
        /// <param name="flowCensorshipOut">流程关卡输出</param>
        /// <param name="flowIn">流程输入</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> BeforeExecFlow(FlowCensorshipOutInfo flowCensorshipOut, object flowIn, string connectionId = null, BasicUserInfo<int> currUser = null);

        /// <summary>
        /// 执行流程后
        /// </summary>
        /// <param name="flowCensorshipOut">流程关卡输出</param>
        /// <param name="flowIn">流程输入</param>
        /// <param name="isSuccess">是否成功</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> AfterExecFlow(FlowCensorshipOutInfo flowCensorshipOut, object flowIn, bool isSuccess, string connectionId = null, BasicUserInfo<int> currUser = null);
    }
}
