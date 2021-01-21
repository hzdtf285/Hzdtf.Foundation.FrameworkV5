using Hzdtf.Persistence.Contract.Management;
using Hzdtf.Utility.Enums;
using Hzdtf.Workflow.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;

namespace Hzdtf.Workflow.MySql
{
    /// <summary>
    /// 工作流定义持久化
    /// @ 黄振东
    /// </summary>
    public partial class WorkflowDefinePersistence
    {
        /// <summary>
        /// 根据编码查询工作流定义信息
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>工作流定义信息</returns>
        public WorkflowDefineInfo SelectByCode(string code, string connectionId = null)
        {
            WorkflowDefineInfo result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{BasicSelectSql()} WHERE {GetFieldByProp("Code")}=@Code";
                Log.TraceAsync(sql, source: this.GetType().Name, tags: "SelectByCode");
                result = dbConn.QueryFirstOrDefault<WorkflowDefineInfo>(sql, new { Code = code });
            }, AccessMode.SLAVE);

            return result;
        }
    }
}
