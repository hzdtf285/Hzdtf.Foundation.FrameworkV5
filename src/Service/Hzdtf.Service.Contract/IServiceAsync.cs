using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Page;
using Hzdtf.Utility.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Service.Contract
{
    /// <summary>
    /// 异步服务接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    /// <typeparam name="ModelT">模型类型</typeparam>
    public partial interface IServiceAsync<IdT, ModelT> 
        where ModelT : SimpleInfo<IdT>
    {
        #region 读取

        /// <summary>
        /// 异步根据ID查找模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<ModelT>> FindAsync(IdT id, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 异步根据ID查找模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="propertyName">属性名称集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<ModelT>> FindAsync(IdT id, string[] propertyName = null, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 异步根据ID集合查找模型列表
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<IList<ModelT>>> FindAsync(IdT[] ids, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 异步根据ID查找模型列表
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="propertyName">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<IList<ModelT>>> FindAsync(IdT[] ids, string[] propertyName = null, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 异步根据ID判断模型是否存在
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<bool>> ExistsAsync(IdT id, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 异步统计模型数
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<int>> CountAsync(CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 异步查询模型列表
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<IList<ModelT>>> QueryAsync(CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 查询模型列表
        /// </summary>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<IList<ModelT>>> QueryAsync(string[] propertyNames, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 异步执行查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<PagingInfo<ModelT>>> QueryPageAsync(int pageIndex, int pageSize, FilterInfo filter = null, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 异步执行查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="filter">筛选</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<PagingInfo<ModelT>>> QueryPageAsync(int pageIndex, int pageSize, string[] propertyNames, FilterInfo filter = null, CommonUseData comData = null, string connectionId = null);

        #endregion

        #region 写入

        /// <summary>
        /// 异步添加模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<bool>> AddAsync(ModelT model, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 异步添加模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<bool>> AddAsync(ModelT model, string[] propertyNames = null, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 异步添加模型列表
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<bool>> AddAsync(IList<ModelT> models, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 异步添加模型列表
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<bool>> AddAsync(IList<ModelT> models, string[] propertyNames = null, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 异步设置模型
        /// 如果ID存在则修改，否则添加
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<bool>> SetAsync(ModelT model, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 异步设置模型
        /// 如果ID存在则修改，否则添加
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<bool>> SetAsync(ModelT model, string[] propertyNames, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 异步根据ID修改模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<bool>> ModifyByIdAsync(ModelT model, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 异步根据ID修改模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<bool>> ModifyByIdAsync(ModelT model, string[] propertyNames, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 异步根据ID移除模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<bool>> RemoveByIdAsync(IdT id, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 异步根据ID数组移除模型
        /// </summary>
        /// <param name="ids">ID数组</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<bool>> RemoveByIdsAsync(IdT[] ids, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 异步清空所有模型
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<bool>> ClearAsync(CommonUseData comData = null, string connectionId = null);

        #endregion
    }
}
