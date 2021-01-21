using Hzdtf.Persistence.Contract.Management;
using Hzdtf.Workflow.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Hzdtf.Utility.Enums;

namespace Hzdtf.Workflow.MySql
{
    /// <summary>
    /// 流程关卡持久化
    /// @ 黄振东
    /// </summary>
    public partial class FlowCensorshipPersistence
    {
        /// <summary>
        /// 根据流程ID查询流程关卡列表
        /// </summary>
        /// <param name="flowId">流程ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>流程关卡列表</returns>
        public IList<FlowCensorshipInfo> SelectByFlowId(int flowId, string connectionId = null)
        {
            IList<FlowCensorshipInfo> result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{BasicSelectSql()} WHERE {GetFieldByProp("FlowId")}=@FlowId";
                Log.TraceAsync(sql, source: this.GetType().Name, tags: "SelectByFlowId");
                result = dbConn.Query<FlowCensorshipInfo>(sql, new { FlowId = flowId }).AsList();
            }, AccessMode.SLAVE);

            return result;
        }
    }
}
