using Hzdtf.Persistence.Contract.Data;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Page;
using Hzdtf.Utility.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Service.Impl
{
    /// <summary>
    /// 服务基类
    /// @ 黄振东
    /// </summary>
    public abstract partial class ServiceBase<IdT, ModelT, PersistenceT> 
        where ModelT : SimpleInfo<IdT>
        where PersistenceT : IPersistence<IdT, ModelT>
    {
        #region 读取

        /// <summary>
        /// 异步根据ID查找模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        public virtual async Task<ReturnInfo<ModelT>> FindAsync(IdT id, CommonUseData comData = null, string connectionId = null)
        {
            return await Task.Run<ReturnInfo<ModelT>>(() =>
            {
                return Find(id, comData, connectionId);
            });
        }

        /// <summary>
        /// 异步根据ID集合查找模型列表
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        public virtual async Task<ReturnInfo<IList<ModelT>>> FindAsync(IdT[] ids, CommonUseData comData = null, string connectionId = null)
        {
            return await Task.Run<ReturnInfo<IList<ModelT>>>(() =>
            {
                return Find(ids, comData, connectionId);
            });
        }

        /// <summary>
        /// 异步根据ID判断模型是否存在
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        public virtual async Task<ReturnInfo<bool>> ExistsAsync(IdT id, CommonUseData comData = null, string connectionId = null)
        {
            return await Task.Run<ReturnInfo<bool>>(() =>
            {
                return Exists(id, comData, connectionId);
            });
        }

        /// <summary>
        /// 异步统计模型数
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        public virtual async Task<ReturnInfo<int>> CountAsync(CommonUseData comData = null, string connectionId = null)
        {
            return await Task.Run<ReturnInfo<int>>(() =>
            {
                return Count(comData, connectionId);
            });
        }

        /// <summary>
        /// 异步查询模型列表
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        public virtual async Task<ReturnInfo<IList<ModelT>>> QueryAsync(CommonUseData comData = null, string connectionId = null)
        {
            return await Task.Run<ReturnInfo<IList<ModelT>>>(() =>
            {
                return Query(comData, connectionId);
            });
        }

        /// <summary>
        /// 异步执行查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        public virtual async Task<ReturnInfo<PagingInfo<ModelT>>> QueryPageAsync(int pageIndex, int pageSize, FilterInfo filter = null, CommonUseData comData = null, string connectionId = null)
        {
            return await Task.Run<ReturnInfo<PagingInfo<ModelT>>>(() =>
            {
                return QueryPage(pageIndex, pageSize, filter, comData, connectionId);
            });
        }

        #endregion

        #region 写入

        /// <summary>
        /// 异步添加模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        public virtual async Task<ReturnInfo<bool>> AddAsync(ModelT model, CommonUseData comData = null, string connectionId = null)
        {
            return await Task.Run<ReturnInfo<bool>>(() =>
            {
                return Add(model, comData, connectionId);
            });
        }

        /// <summary>
        /// 异步添加模型列表
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        public virtual async Task<ReturnInfo<bool>> AddAsync(IList<ModelT> models, CommonUseData comData = null, string connectionId = null)
        {
            return await Task.Run<ReturnInfo<bool>>(() =>
            {
                return Add(models, comData, connectionId);
            });
        }

        /// <summary>
        /// 异步设置模型
        /// 如果ID存在则修改，否则添加
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        public virtual async Task<ReturnInfo<bool>> SetAsync(ModelT model, CommonUseData comData = null, string connectionId = null)
        {
            return await Task.Run<ReturnInfo<bool>>(() =>
            {
                return Set(model, comData, connectionId);
            });
        }

        /// <summary>
        /// 异步根据ID修改模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        public virtual async Task<ReturnInfo<bool>> ModifyByIdAsync(ModelT model, CommonUseData comData = null, string connectionId = null)
        {
            return await Task.Run<ReturnInfo<bool>>(() =>
            {
                return ModifyById(model, comData, connectionId);
            });
        }

        /// <summary>
        /// 异步根据ID移除模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        public virtual async Task<ReturnInfo<bool>> RemoveByIdAsync(IdT id, CommonUseData comData = null, string connectionId = null)
        {
            return await Task.Run<ReturnInfo<bool>>(() =>
            {
                return RemoveById(id, comData, connectionId);
            });
        }

        /// <summary>
        /// 异步根据ID集合移除模型
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        public virtual async Task<ReturnInfo<bool>> RemoveByIdsAsync(IdT[] ids, CommonUseData comData = null, string connectionId = null)
        {
            return await Task.Run<ReturnInfo<bool>>(() =>
            {
                return RemoveByIds(ids, comData, connectionId);
            });
        }

        /// <summary>
        /// 异步清空所有模型
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        public virtual async Task<ReturnInfo<bool>> ClearAsync(CommonUseData comData = null, string connectionId = null)
        {
            return await Task.Run<ReturnInfo<bool>>(() =>
            {
                return Clear(comData, connectionId);
            });
        }

        #endregion
    }
}
