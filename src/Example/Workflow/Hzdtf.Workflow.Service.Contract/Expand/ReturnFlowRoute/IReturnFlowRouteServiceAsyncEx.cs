using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using Hzdtf.Workflow.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Workflow.Service.Contract
{
    /// <summary>
    /// 退件流程路线服务异步接口
    /// @ 黄振东
    /// </summary>
    public partial interface IReturnFlowRouteServiceAsync
    {
        /// <summary>
        /// 异步根据流程关卡ID查询退件流程路线列表
        /// </summary>
        /// <param name="flowCensorshipId">流程关卡ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>任务</returns>
        Task<ReturnInfo<IList<ReturnFlowRouteInfo>>> QueryByFlowCensorshipIdAsync(int flowCensorshipId, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 异步根据流程关卡ID数组查询退件流程路线列表
        /// </summary>
        /// <param name="flowCensorshipIds">流程关卡ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>任务</returns>
        Task<ReturnInfo<IList<ReturnFlowRouteInfo>>> QueryByFlowCensorshipIdsAsync(int[] flowCensorshipIds, CommonUseData comData = null, string connectionId = null);
    }
}
