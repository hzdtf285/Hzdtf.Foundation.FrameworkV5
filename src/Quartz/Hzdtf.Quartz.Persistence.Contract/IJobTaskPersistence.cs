using Hzdtf.Quartz.Model;
using System;
using System.Collections.Generic;

namespace Hzdtf.Quartz.Persistence.Contract
{
    /// <summary>
    /// 作业任务持久化接口
    /// @ 黄振东
    /// </summary>
    public interface IJobTaskPersistence
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>作业任务信息列表</returns>
        IList<JobTaskInfo> Query(string connectionId = null);

        /// <summary>
        /// 设置，如果存在则更新，否则插入
        /// </summary>
        /// <param name="jobTask">作业任务</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int Set(JobTaskInfo jobTask, string connectionId = null);

        /// <summary>
        /// 更新Cron表达式
        /// </summary>
        /// <param name="cron">cron表达式</param>
        /// <param name="name">名称</param>
        /// <param name="group">分组</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int UpdateCron(string cron, string name, string group = null, string connectionId = null);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int Delete(int id, string connectionId = null);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="group">分组</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int Delete(string name, string group = null, string connectionId = null);
    }
}
