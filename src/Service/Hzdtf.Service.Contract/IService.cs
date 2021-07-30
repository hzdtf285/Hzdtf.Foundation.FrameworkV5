using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Page;
using Hzdtf.Utility.Model.Return;
using System;
using System.Collections.Generic;

namespace Hzdtf.Service.Contract
{
    /// <summary>
    /// 服务接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    /// <typeparam name="ModelT">模型类型</typeparam>
    public partial interface IService<IdT, ModelT> : IServiceAsync<IdT, ModelT> 
        where ModelT : SimpleInfo<IdT>
    {
        #region 读取 

        /// <summary>
        /// 根据ID查找模型前事件
        /// </summary>
        event Action<ReturnInfo<ModelT>, IdT, CommonUseData, string> Finding;

        /// <summary>
        /// 根据ID查找模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        ReturnInfo<ModelT> Find(IdT id, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 根据ID查找模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="propertyName">属性名称集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        ReturnInfo<ModelT> Find(IdT id, string[] propertyName = null, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 根据ID查找模型后事件
        /// </summary>
        event Action<ReturnInfo<ModelT>, IdT, CommonUseData, string> Finded;

        /// <summary>
        /// 根据ID查找模型列表前事件
        /// </summary>
        event Action<ReturnInfo<IList<ModelT>>, IdT[], CommonUseData, string> Findsing;

        /// <summary>
        /// 根据ID查找模型列表
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<IList<ModelT>> Find(IdT[] ids, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 根据ID查找模型列表
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="propertyName">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<IList<ModelT>> Find(IdT[] ids, string[] propertyName = null, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 根据ID查找模型列表后事件
        /// </summary>
        event Action<ReturnInfo<IList<ModelT>>, IdT[], CommonUseData, string> Findsed;

        /// <summary>
        /// 根据ID判断模型是否存在前事件
        /// </summary>
        event Action<ReturnInfo<bool>, IdT, CommonUseData, string> Existsing;

        /// <summary>
        /// 根据ID判断模型是否存在
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> Exists(IdT id, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 根据ID判断模型是否存在后事件
        /// </summary>
        event Action<ReturnInfo<bool>, IdT, CommonUseData, string> Existsed;

        /// <summary>
        /// 统计模型数前事件
        /// </summary>
        event Action<ReturnInfo<int>, CommonUseData, string> Counting;

        /// <summary>
        /// 统计模型数
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<int> Count(CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 统计模型数后事件
        /// </summary>
        event Action<ReturnInfo<int>, CommonUseData, string> Counted;

        /// <summary>
        /// 查询模型列表前事件
        /// </summary>
        event Action<ReturnInfo<IList<ModelT>>, CommonUseData, string> Querying;

        /// <summary>
        /// 查询模型列表
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<IList<ModelT>> Query(CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 查询模型列表
        /// </summary>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<IList<ModelT>> Query(string[] propertyNames, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 查询模型列表后事件
        /// </summary>
        event Action<ReturnInfo<IList<ModelT>>, CommonUseData, string> Queryed;

        /// <summary>
        /// 执行查询模型列表并分页前事件
        /// </summary>
        event Action<ReturnInfo<PagingInfo<ModelT>>, int, int, FilterInfo, CommonUseData, string> QueryPaging;

        /// <summary>
        /// 执行查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<PagingInfo<ModelT>> QueryPage(int pageIndex, int pageSize, FilterInfo filter = null, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 执行查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="filter">筛选</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<PagingInfo<ModelT>> QueryPage(int pageIndex, int pageSize, string[] propertyNames, FilterInfo filter = null, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 执行查询模型列表并分页后事件
        /// </summary>
        event Action<ReturnInfo<PagingInfo<ModelT>>, int, int, FilterInfo, CommonUseData, string> QueryPaged;

        #endregion

        #region 写入

        /// <summary>
        /// 添加模型前事件
        /// </summary>
        event Action<ReturnInfo<bool>, ModelT, CommonUseData, string> Adding;

        /// <summary>
        /// 添加模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> Add(ModelT model, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 添加模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> Add(ModelT model, string[] propertyNames, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 添加模型后事件
        /// </summary>
        event Action<ReturnInfo<bool>, ModelT, CommonUseData, string> Added;

        /// <summary>
        /// 添加模型列表前事件
        /// </summary>
        event Action<ReturnInfo<bool>, IList<ModelT>, CommonUseData, string> Addsing;

        /// <summary>
        /// 添加模型列表
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> Add(IList<ModelT> models, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 添加模型列表
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> Add(IList<ModelT> models, string[] propertyNames, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 添加模型列表后事件
        /// </summary>
        event Action<ReturnInfo<bool>, IList<ModelT>, CommonUseData, string> Addsed;

        /// <summary>
        /// 设置模型前事件
        /// </summary>
        event Action<ReturnInfo<bool>, ModelT, CommonUseData, string> Seting;

        /// <summary>
        /// 设置模型
        /// 如果ID存在则修改，否则添加
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> Set(ModelT model, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 设置模型
        /// 如果ID存在则修改，否则添加
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> Set(ModelT model, string[] propertyNames, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 设置模型后事件
        /// </summary>
        event Action<ReturnInfo<bool>, ModelT, CommonUseData, string> Seted;

        /// <summary>
        /// 根据ID修改模型前事件
        /// </summary>
        event Action<ReturnInfo<bool>, ModelT, CommonUseData, string> ModifyByIding;

        /// <summary>
        /// 根据ID修改模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> ModifyById(ModelT model, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 根据ID修改模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> ModifyById(ModelT model, string[] propertyNames, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 根据ID修改模型后事件
        /// </summary>
        event Action<ReturnInfo<bool>, ModelT, CommonUseData, string> ModifyByIded;

        /// <summary>
        /// 根据ID移除模型前事件
        /// </summary>
        event Action<ReturnInfo<bool>, IdT, CommonUseData, string> RemoveByIding;

        /// <summary>
        /// 根据ID移除模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> RemoveById(IdT id, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 根据ID移除模型后事件
        /// </summary>
        event Action<ReturnInfo<bool>, IdT, CommonUseData, string> RemoveByIded;

        /// <summary>
        /// 根据ID数组移除模型前事件
        /// </summary>
        event Action<ReturnInfo<bool>, IdT[], CommonUseData, string> RemoveByIdsing;

        /// <summary>
        /// 根据ID数组移除模型
        /// </summary>
        /// <param name="ids">ID数组</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> RemoveByIds(IdT[] ids, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 根据ID数组移除模型后事件
        /// </summary>
        event Action<ReturnInfo<bool>, IdT[], CommonUseData, string> RemoveByIdsed;

        /// <summary>
        /// 清空所有模型前事件
        /// </summary>
        event Action<ReturnInfo<bool>, CommonUseData, string> Clearing;

        /// <summary>
        /// 清空所有模型
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> Clear(CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 清空所有模型后事件
        /// </summary>
        event Action<ReturnInfo<bool>, CommonUseData, string> Cleared;

        #endregion
    }
}
