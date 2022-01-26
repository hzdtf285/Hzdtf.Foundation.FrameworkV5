using Hzdtf.Quartz.Model;
using Hzdtf.Quartz.Persistence.Contract;
using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hzdtf.Utility.Utils;
using Hzdtf.Utility.Model.Page;

namespace Hzdtf.Quartz.File
{
    /// <summary>
    /// 作业任务JSON文件
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class JobTaskJsonFile : IJobTaskBasicPersistence
    {
        /// <summary>
        /// 列表
        /// </summary>
        private IList<JobTaskInfo> list;

        /// <summary>
        /// JSON文件
        /// </summary>
        private readonly string jsonFile;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="jsonFile">JSON文件</param>
        public JobTaskJsonFile(string jsonFile = null)
        {
            if (string.IsNullOrWhiteSpace(jsonFile))
            {
                jsonFile = $"{AppContext.BaseDirectory}/Config/jobTaskConfig.json";
            }
            this.jsonFile = jsonFile;

            list = jsonFile.ToJsonObjectFromFile<IList<JobTaskInfo>>();
        }

        /// <summary>
        /// 新建一个连接ID
        /// </summary>
        /// <param name="accessMode">访问模式</param>
        /// <returns>连接ID</returns>
        public string NewConnectionId(AccessMode accessMode = AccessMode.MASTER)
        {
            return null;
        }

        /// <summary>
        /// 释放连接ID
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        public void Release(string connectionId)
        { 
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>作业任务信息列表</returns>
        public IList<JobTaskInfo> Query(JobTaskFilterInfo filter = null, string connectionId = null)
        {
            var q = QueryForFilter(filter);
            if (q == null)
            {
                return null;
            }

            return q.ToList();
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
            var q = QueryForFilter(filter);
            if (q == null)
            {
                return null;
            }

            var page = new PagingInfo<JobTaskInfo>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Records = q.Count()
            };
            if (page.Records == 0)
            {
                return page;
            }

            page.Rows = q.Skip(pageIndex.GetSkipRecordIndex(pageSize)).Take(pageSize).ToList();

            return page;
        }

        /// <summary>
        /// 为过滤器获取查询表达式
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <returns>查询表达式</returns>
        private IQueryable<JobTaskInfo> QueryForFilter(JobTaskFilterInfo filter = null)
        {
            if (list.IsNullOrCount0())
            {
                return null;
            }
            var query = list.AsQueryable();
            if (filter == null)
            {
                return query;
            }
            if (!filter.Ids.IsNullOrLength0())
            {
                query = query.Where(p => filter.Ids.Contains(p.Id));
            }
            if (!filter.JtNames.IsNullOrLength0())
            {
                query = query.Where(p => filter.JtNames.Contains(p.JtName, true));
            }
            if (!filter.JtGroups.IsNullOrLength0())
            {
                query = query.Where(p => filter.JtGroups.Contains(p.JtGroup, true));
            }
            if (!filter.JobFullClasses.IsNullOrLength0())
            {
                query = query.Where(p => filter.JobFullClasses.Contains(p.JobFullClass, true));
            }
            if (!filter.TriggerCrons.IsNullOrLength0())
            {
                query = query.Where(p => filter.TriggerCrons.Contains(p.TriggerCron, true));
            }
            if (filter.SuccessedRemove != null)
            {
                query = query.Where(p => p.SuccessedRemove == filter.SuccessedRemove.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.Keyword))
            {
                query = query.Where(p => p.JtName.Contains(filter.Keyword) || p.JtGroup.Contains(filter.Keyword));
            }
            if (filter.StartCreateTime != null)
            {
                query = query.Where(p => p.CreateTime >= filter.StartCreateTime);
            }
            if (filter.EndCreateTime != null)
            {
                query = query.Where(p => p.CreateTime <= filter.EndCreateTime);
            }
            if (!string.IsNullOrWhiteSpace(filter.SortName))
            {
                query = query.OrderBy(new Dictionary<string, SortType>(1)
                {
                    { filter.SortName, filter.Sort }
                });
            }

            return query;
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
            return list.Where(p => p.JtName == name && p.JtGroup == group).FirstOrDefault();
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>作业任务</returns>
        public JobTaskInfo Find(int id, string connectionId = null)
        {
            return list.Where(p => p.Id == id).FirstOrDefault();
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
            return Find(name, group, connectionId) != null;
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>作业任务</returns>
        public bool Exists(int id, string connectionId = null)
        {
            return Find(id, connectionId) != null;
        }

        /// <summary>
        /// 设置，如果存在则更新，否则插入
        /// </summary>
        /// <param name="jobTask">作业任务</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        public int Set(JobTaskInfo jobTask, string connectionId = null)
        {
            if (list == null)
            {
                list = new List<JobTaskInfo>();
                jobTask.InitCreateTime();
                jobTask.InitModifyTime();
                list.Add(jobTask);
                jsonFile.WriteJsonFile(list);
                return 1;
            }
            var exists = list.Where(p => p.JtName == jobTask.JtName && p.JtGroup == jobTask.JtGroup).FirstOrDefault();
            if (exists == null)
            {
                list.Add(jobTask);
            }
            else
            {
                jobTask.SyncAssign<JobTaskInfo, JobTaskInfo>(exists, "Id", "Name", "Group");
            }
            jsonFile.WriteJsonFile(list);

            return 1;
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
            if (list == null)
            {
                return 0;
            }
            var exists = list.Where(p => p.JtName == name && p.JtGroup == group).FirstOrDefault();
            if (exists == null)
            {
                return 0;
            }
            else
            {
                exists.TriggerCron = cron;
                exists.InitModifyTime();
                jsonFile.WriteJsonFile(list);
                return 1;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        public int Delete(int id, string connectionId = null)
        {
            if (list == null)
            {
                return 0;
            }
            var exists = list.Where(p => p.Id == id).FirstOrDefault();
            if (exists == null)
            {
                return 0;
            }
            else
            {
                jsonFile.WriteJsonFile(list);
                return list.Remove(exists) ? 1 : 0;
            }
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
            if (list == null)
            {
                return 0;
            }

            var exists = list.Where(p => p.JtName == name && p.JtGroup == group).FirstOrDefault();
            if (exists == null)
            {
                return 0;
            }
            else
            {
                jsonFile.WriteJsonFile(list);
                return list.Remove(exists) ? 1 : 0;
            }
        }
    }
}
