using Hzdtf.Quartz.Model;
using Hzdtf.Quartz.Persistence.Contract;
using Hzdtf.Utility.Attr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
        /// 查询所有
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>作业任务信息列表</returns>
        public IList<JobTaskInfo> Query(string connectionId = null)
        {
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
