using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using Hzdtf.Workflow.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Utils;
using Hzdtf.Utility.Attr.ParamAttr;
using Hzdtf.Utility.Attr;

namespace Hzdtf.Workflow.Service.Impl
{
    /// <summary>
    /// 工作流处理服务
    /// @ 黄振东
    /// </summary>
    public partial class WorkflowHandleService
    {
        /// <summary>
        /// 根据ID修改为已读
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<bool> ModifyToReadedById([DisplayName2("ID"), Id] int id, CommonUseData comData = null, string connectionId = null)
        {
            var currUser = UserTool<int>.GetCurrUser(comData);
            return ExecReturnFuncAndConnectionId<bool>((reInfo, connId) =>
            {
                WorkflowHandleInfo wh = Persistence.Select(id, comData, connId);
                if (wh == null)
                {
                    reInfo.SetFailureMsg("找不到该工作流处理记录");
                    return false;
                }

                var user = UserTool<int>.GetCurrUser(currUser);
                if (wh.HandlerId != user.Id)
                {
                    reInfo.SetFailureMsg("Sorry!不是您处理的无权限修改");
                    return false;
                }
                if (wh.IsReaded)
                {
                    return false;
                }

                wh.IsReaded = true;
                wh.SetModifyInfo(currUser);

                return Persistence.UpdateIsReadedById(wh, connId) > 0;
            }, null, connectionId);
        }

        /// <summary>
        /// 根据处理人ID是否存在审核且未处理的个数
        /// </summary>
        /// <param name="handlerId">处理人ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<bool> ExistsAuditAndUnhandleByHandleId([DisplayName2("处理人ID"), Id] int handlerId, CommonUseData comData = null, string connectionId = null)
        {
            return ExecReturnFunc<bool>((reInfo) =>
            {
                return Persistence.CountAuditAndUnhandleByHandleId(handlerId, connectionId) > 0;
            });
        }

        /// <summary>
        /// 根据处理人ID集合是否存在审核且未处理的个数
        /// </summary>
        /// <param name="handlerIds">处理人ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<bool[]> ExistsAuditAndUnhandleByHandleIds([DisplayName2("处理人ID集合"), ArrayNotEmpty] int[] handlerIds, CommonUseData comData = null, string connectionId = null)
        {
            return ExecReturnFuncAndConnectionId<bool[]>((reInfo, connId) =>
            {
                bool[] result = new bool[handlerIds.Length];
                for (var i = 0; i < handlerIds.Length; i++)
                {
                    result[i] = Persistence.CountAuditAndUnhandleByHandleId(handlerIds[i], connId) > 0;
                }

                return result;
            }, null, connectionId);
        }

        /// <summary>
        /// 根据工作流人ID、流程关卡ID和处理人ID查找工作流处理信息
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <param name="flowCensorshipId">流程关卡ID</param>
        /// <param name="handleId">处理人ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<WorkflowHandleInfo> FindByWorkflowIdAndFlowCensorshipIdAndHandlerId([DisplayName2("工作流ID"), Id] int workflowId, [DisplayName2("流程关卡ID"), Id] int flowCensorshipId, [DisplayName2("处理人ID"), Id] int handleId, CommonUseData comData = null, string connectionId = null)
        {
            return ExecReturnFuncAndConnectionId<WorkflowHandleInfo>((reInfo, connId) =>
            {
                return Persistence.SelectByWorkflowIdAndFlowCensorshipIdAndHandlerId(workflowId, flowCensorshipId, handleId, connId);
            }, null, connectionId);
        }
    }
}
