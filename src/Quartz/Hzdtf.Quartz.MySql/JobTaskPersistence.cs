using Dapper;
using Hzdtf.Persistence.Contract.Management;
using Hzdtf.Quartz.Model;
using Hzdtf.Quartz.Persistence.Contract;
using Hzdtf.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Hzdtf.Utility.Utils;
using Hzdtf.Utility.Model.Page;
using System.Text;

namespace Hzdtf.Quartz.MySql
{
    /// <summary>
    /// 作业任务持久化
    /// @ 黄振东
    /// </summary>
    public partial class JobTaskPersistence : IJobTaskBasicPersistence
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>作业任务信息列表</returns>
        public IList<JobTaskInfo> Query(JobTaskFilterInfo filter = null, string connectionId = null)
        {
            var sql = $"{BasicSelectSql()} {GetWhereSql(ref filter)}";
            if (!string.IsNullOrWhiteSpace(filter.SortName))
            {
                sql += $" ORDER BY {PfxEscapeChar}{filter.SortName}{SufxEscapeChar} {filter.Sort.ToString()}";
            }

            IList<JobTaskInfo> result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = ExecRecordSqlLog<IList<JobTaskInfo>>(sql, () =>
                {
                    return dbConn.Query<JobTaskInfo>(sql, filter, GetDbTransaction(connId, AccessMode.SLAVE)).ToList();
                }, tag: "Query");
            }, AccessMode.SLAVE);
            DbToModelFullOtherProps(result);

            return result;
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">页码，从0开始</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">过滤器</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>分页信息</returns>
        public PagingInfo<JobTaskInfo> QueryPage(int pageIndex, int pageSize, JobTaskFilterInfo filter = null, string connectionId = null)
        {
            var whereSql = GetWhereSql(ref filter);
            if (string.IsNullOrWhiteSpace(filter.SortName))
            {
                filter.SortName = "CreateTime";
                filter.Sort = SortType.ASC;
            }

            var orderSql = new StringBuilder($"ORDER BY {PfxEscapeChar}{filter.SortName}{SufxEscapeChar} {filter.Sort.ToString()}");
            if (!"Id".Equals(filter.SortName))
            {
                orderSql.AppendFormat(",{0}", GetFieldByProp("Id"));
            }

            var whereSqlStr = whereSql.ToString();
            var countSql = $"{BasicCountSql()} {whereSqlStr}";
            var selectSql = $"{BasicSelectSql()} {whereSqlStr} {orderSql.ToString()} {GetPartPageSql(pageIndex, pageSize)}";

            PagingInfo<JobTaskInfo> result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                var trans = GetDbTransaction(connId, AccessMode.SLAVE);
                result = PagingUtil.ExecPage<JobTaskInfo>(pageIndex, pageSize, () =>
                {
                    return ExecRecordSqlLog<int>(countSql, () =>
                    {
                        return dbConn.ExecuteScalar<int>(countSql, filter, transaction: trans);
                    }, tag: "QueryPage");
                }, () =>
                {
                    return ExecRecordSqlLog<IList<JobTaskInfo>>(selectSql, () =>
                    {
                        return dbConn.Query<JobTaskInfo>(selectSql, filter, transaction: trans).AsList();
                    }, tag: "QueryPage");
                });
            }, AccessMode.SLAVE);
            DbToModelFullOtherProps(result.Rows);

            return result;
        }

        /// <summary>
        /// 获取条件SQL
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <returns>条件SQL</returns>
        private string GetWhereSql(ref JobTaskFilterInfo filter)
        {
            var sql = CreateWhereSql();
            if (filter == null)
            {
                filter = new JobTaskFilterInfo();
                return sql.ToString();
            }

            var nameField = GetFieldByProp("JtName");
            var groupField = GetFieldByProp("JtGroup");
            if (!filter.Ids.IsNullOrLength0())
            {
                sql.AppendFormat(" AND {0}{1}{2} IN({3})", PfxEscapeChar, GetFieldByProp("Id"), SufxEscapeChar, filter.Ids.ToMergeString(","));
            }
            if (!filter.JtNames.IsNullOrLength0())
            {
                sql.AppendFormat(" AND {0}{1}{2} IN({3})", PfxEscapeChar, nameField, SufxEscapeChar, filter.JtNames.ToMergeString(",", "'"));
            }
            if (!filter.JtGroups.IsNullOrLength0())
            {
                sql.AppendFormat(" AND {0}{1}{2} IN({3})", PfxEscapeChar, groupField, SufxEscapeChar, filter.JtGroups.ToMergeString(",", "'"));
            }
            if (!filter.JobFullClasses.IsNullOrLength0())
            {
                sql.AppendFormat(" AND {0}{1}{2} IN({3})", PfxEscapeChar, GetFieldByProp("JobFullClasse"), SufxEscapeChar, filter.JobFullClasses.ToMergeString(",", "'"));
            }
            if (!filter.TriggerCrons.IsNullOrLength0())
            {
                sql.AppendFormat(" AND {0}{1}{2} IN({3})", PfxEscapeChar, GetFieldByProp("TriggerCron"), SufxEscapeChar, filter.TriggerCrons.ToMergeString(",", "'"));
            }
            if (filter.SuccessedRemove != null)
            {
                sql.AppendFormat(" AND {0}{1}{2}=@SuccessedRemove", PfxEscapeChar, GetFieldByProp("SuccessedRemove"), SufxEscapeChar);
            }

            if (!string.IsNullOrWhiteSpace(filter.Keyword))
            {
                sql.AppendFormat(" AND ({0}{1}{2} LIKE '%{3}%' OR {0}{4}{2} LIKE '%{3}%')",
                    PfxEscapeChar, nameField, SufxEscapeChar, filter.Keyword.FillSqlValue(), groupField);
            }
            if (filter.StartCreateTime != null)
            {
                sql.AppendFormat(" AND {0}{1}{2}>=@StartCreateTime", PfxEscapeChar, GetFieldByProp("CreateTime"), SufxEscapeChar);
            }
            if (filter.EndCreateTime != null)
            {
                sql.AppendFormat(" AND {0}{1}{2}>=@EndCreateTime", PfxEscapeChar, GetFieldByProp("CreateTime"), SufxEscapeChar);
            }

            return sql.ToString();
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
