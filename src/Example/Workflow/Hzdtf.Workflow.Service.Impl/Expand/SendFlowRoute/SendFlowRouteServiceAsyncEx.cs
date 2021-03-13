using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using Hzdtf.Workflow.Model;
using Hzdtf.Workflow.Service.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Workflow.Service.Impl
{
    /// <summary>
    /// 送件流程路线服务
    /// @ 黄振东
    /// </summary>
    public partial class SendFlowRouteService : ISendFlowRouteServiceAsync
    {
        /// <summary>
        /// 异步根据流程关卡ID查询送件流程路线列表
        /// </summary>
        /// <param name="flowCensorshipId">流程关卡ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>任务</returns>
        public virtual async Task<ReturnInfo<IList<SendFlowRouteInfo>>> QueryByFlowCensorshipIdAsync(int flowCensorshipId, CommonUseData comData = null, string connectionId = null)
        {
            return await Task.Run<ReturnInfo<IList<SendFlowRouteInfo>>>(() =>
            {
                return QueryByFlowCensorshipId(flowCensorshipId, connectionId : connectionId, comData: comData);
            });
        }

        /// <summary>
        /// 异步根据流程关卡ID数组查询送件流程路线列表
        /// </summary>
        /// <param name="flowCensorshipIds">流程关卡ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>任务</returns>
        public virtual async Task<ReturnInfo<IList<SendFlowRouteInfo>>> QueryByFlowCensorshipIdsAsync(int[] flowCensorshipIds, CommonUseData comData = null, string connectionId = null)
        {
            return await Task.Run<ReturnInfo<IList<SendFlowRouteInfo>>>(() =>
            {
                return QueryByFlowCensorshipIds(flowCensorshipIds, connectionId : connectionId, comData: comData);
            });
        }
    }
}
