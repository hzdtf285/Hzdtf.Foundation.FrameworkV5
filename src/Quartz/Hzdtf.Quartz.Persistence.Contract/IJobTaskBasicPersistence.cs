using Hzdtf.Quartz.Model;
using Hzdtf.Utility.Enums;
using Hzdtf.Utility.Model.Page;
using System;
using System.Collections.Generic;

namespace Hzdtf.Quartz.Persistence.Contract
{
    /// <summary>
    /// 作业任务基本持久化接口
    /// @ 黄振东
    /// </summary>
    public partial interface IJobTaskBasicPersistence
    {
        /// <summary>
        /// 新建一个连接ID
        /// </summary>
        /// <param name="accessMode">访问模式</param>
        /// <returns>连接ID</returns>
        string NewConnectionId(AccessMode accessMode = AccessMode.MASTER);

        /// <summary>
        /// 释放连接ID
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        void Release(string connectionId);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>作业任务信息列表</returns>
        IList<JobTaskInfo> Query(JobTaskFilterInfo filter = null, string connectionId = null);

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">页码，从0开始</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">过滤器</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>分页信息</returns>
        PagingInfo<JobTaskInfo> QueryPage(int pageIndex, int pageSize, JobTaskFilterInfo filter = null, string connectionId = null);

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="group">分组</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>作业任务</returns>
        JobTaskInfo Find(string name, string group = null, string connectionId = null);

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>作业任务</returns>
        JobTaskInfo Find(int id, string connectionId = null);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="group">分组</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>作业任务</returns>
        bool Exists(string name, string group = null, string connectionId = null);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>作业任务</returns>
        bool Exists(int id, string connectionId = null);

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
