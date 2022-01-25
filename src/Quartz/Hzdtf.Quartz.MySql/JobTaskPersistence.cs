using Dapper;
using Hzdtf.Persistence.Contract.Management;
using Hzdtf.Quartz.Model;
using Hzdtf.Quartz.Persistence.Contract;
using Hzdtf.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hzdtf.Utility.Utils;

namespace Hzdtf.Quartz.MySql
{
    /// <summary>
    /// 作业任务持久化
    /// @ 黄振东
    /// </summary>
    public partial class JobTaskPersistence : IJobTaskBasicPersistence
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>作业任务信息列表</returns>
        public IList<JobTaskInfo> Query(string connectionId = null)
        {
            var list = this.Select(connectionId: connectionId);
            DbToModelFullOtherProps(list);

            return list;
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="group">分组</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>作业任务</returns>
        public JobTaskInfo Find(string name, string group = null, string connectionId = null)
        {
            var sql = $"{BasicSelectSql()} WHERE {GetNameAndGroupSql()}";
            JobTaskInfo result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = ExecRecordSqlLog<JobTaskInfo>(sql, () =>
                {
                    return dbConn.QueryFirstOrDefault<JobTaskInfo>(sql, new
                    {
                        Name = name,
                        Group = group
                    }, GetDbTransaction(connId, AccessMode.SLAVE));
                }, tag: "Find");
            }, AccessMode.SLAVE);
            DbToModelFullOtherProps(result);

            return result;
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>作业任务</returns>
        public JobTaskInfo Find(int id, string connectionId = null)
        {
            var sql = $"{BasicSelectSql()} WHERE {PfxEscapeChar}{GetFieldByProp("Id")}{SufxEscapeChar}=@Id";
            JobTaskInfo result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = ExecRecordSqlLog<JobTaskInfo>(sql, () =>
                {
                    return dbConn.QueryFirstOrDefault<JobTaskInfo>(sql, new
                    {
                        Id = id,
                    }, GetDbTransaction(connId, AccessMode.SLAVE));
                }, tag: "Find");
            }, AccessMode.SLAVE);
            DbToModelFullOtherProps(result);

            return result;
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="group">分组</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>作业任务</returns>
        public bool Exists(string name, string group = null, string connectionId = null)
        {
            var sql = $"{BasicCountSql()} WHERE {GetNameAndGroupSql()}";
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = ExecRecordSqlLog<int>(sql, () =>
                {
                    return dbConn.ExecuteScalar<int>(sql, new
                    {
                        Name = name,
                        Group = group
                    }, GetDbTransaction(connId, AccessMode.SLAVE));
                }, tag: "Exists");
            }, AccessMode.SLAVE);

            return result > 0;
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>作业任务</returns>
        public bool Exists(int id, string connectionId = null)
        {
            var sql = $"{BasicCountSql()} WHERE {PfxEscapeChar}{GetFieldByProp("Id")}{SufxEscapeChar}=@Id";
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = ExecRecordSqlLog<int>(sql, () =>
                {
                    return dbConn.ExecuteScalar<int>(sql, new
                    {
                        Id = id,
                    }, GetDbTransaction(connId, AccessMode.SLAVE));
                }, tag: "Exists");
            }, AccessMode.SLAVE);

            return result > 0;
        }

        /// <summary>
        /// 设置，如果存在则更新，否则插入
        /// </summary>
        /// <param name="jobTask">作业任务</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        public int Set(JobTaskInfo jobTask, string connectionId = null)
        {
            int result = 0;
            ModelToDbFullOtherProps(jobTask);
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                if (Exists(jobTask.JtName, jobTask.JtGroup, connId))
                {
                    result = UpdateCron(jobTask.TriggerCron, jobTask.JtName, jobTask.JtGroup, connId);
                }
                else
                {
                    jobTask.CreateTime = jobTask.ModifyTime = DateTime.Now;
                    result = Insert(jobTask, connectionId: connId);
                }
            });

            return result;
        }

        /// <summary>
        /// 更新Cron表达式
        /// </summary>
        /// <param name="cron">cron表达式</param>
        /// <param name="name">名称</param>
        /// <param name="group">分组</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        public int UpdateCron(string cron, string name, string group = null, string connectionId = null)
        {
            var sql = $"UPDATE {PfxEscapeChar}{Table}{SufxEscapeChar} SET {PfxEscapeChar}{GetFieldByProp("TriggerCron")}{SufxEscapeChar}=@TriggerCron,{PfxEscapeChar}{GetFieldByProp("ModifyTime")}{SufxEscapeChar}=NOW()" +
                $" WHERE {GetNameAndGroupSql()}";
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = ExecRecordSqlLog<int>(sql, () =>
                {
                    return dbConn.Execute(sql, new
                    {
                        TriggerCron = cron,
                        Name = name,
                        Group = group
                    }, GetDbTransaction(connId));
                }, tag: "UpdateCron");
            });

            return result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        public int Delete(int id, string connectionId = null)
        {
            return this.DeleteById(id, connectionId: connectionId);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="group">分组</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        public int Delete(string name, string group = null, string connectionId = null)
        {
            var sql = $"{BasicDeleteSql()} WHERE {GetNameAndGroupSql()}";
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = ExecRecordSqlLog<int>(sql, () =>
                {
                    return dbConn.Execute(sql, new
                    {
                        Name = name,
                        Group = group
                    }, GetDbTransaction(connId));
                }, tag: "Delete");
            });

            return result;
        }

        /// <summary>
        /// 数据库到模型填充其他属性
        /// </summary>
        /// <param name="jobTasks">作业任务列表</param>
        private void DbToModelFullOtherProps(IList<JobTaskInfo> jobTasks)
        {
            if (jobTasks.IsNullOrCount0())
            {
                return;
            }

            foreach (var jt in jobTasks)
            {
                DbToModelFullOtherProps(jt);
            }
        }

        /// <summary>
        /// 数据库到模型填充其他属性
        /// </summary>
        /// <param name="jobTask">作业任务</param>
        private void DbToModelFullOtherProps(JobTaskInfo jobTask)
        {
            if (jobTask == null)
            {
                return;
            }
            jobTask.JobParams = jobTask.JobParamsJsonString.ToJsonObject<IDictionary<string, string>>();
            jobTask.TriggerParams = jobTask.TriggerParamsJsonString.ToJsonObject<IDictionary<string, string>>();
        }

        /// <summary>
        /// 模型到数据库填充其他属性
        /// </summary>
        /// <param name="jobTasks">作业任务列表</param>
        private void ModelToDbFullOtherProps(IList<JobTaskInfo> jobTasks)
        {
            if (jobTasks.IsNullOrCount0())
            {
                return;
            }

            foreach (var jt in jobTasks)
            {
                ModelToDbFullOtherProps(jt);
            }
        }

        /// <summary>
        /// 模型到数据库填充其他属性
        /// </summary>
        /// <param name="jobTask">作业任务</param>
        private void ModelToDbFullOtherProps(JobTaskInfo jobTask)
        {
            if (jobTask == null)
            {
                return;
            }
            jobTask.JobParamsJsonString = jobTask.JobParams.ToJsonString();
            jobTask.TriggerParamsJsonString = jobTask.TriggerParams.ToJsonString();
        }

        /// <summary>
        /// 获取名称和分组SQL
        /// </summary>
        /// <returns>名称和分组SQL</returns>
        private string GetNameAndGroupSql()
        {
            return $"{PfxEscapeChar}{GetFieldByProp("JtName")}{SufxEscapeChar}=@Name AND" +
                $" {PfxEscapeChar}{GetFieldByProp("JtGroup")}{SufxEscapeChar}=@Group";
        }
    }
}
