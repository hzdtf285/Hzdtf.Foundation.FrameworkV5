using Hzdtf.Utility.Enums;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Page;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Persistence.Contract.Data
{
    /// <summary>
    /// 持久化接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    /// <typeparam name="ModelT">模型类型</typeparam>
    public interface IPersistence<IdT, ModelT> : IPersistenceAsync<IdT, ModelT> 
        where ModelT : SimpleInfo<IdT>
    {
        #region 读取方法

        /// <summary>
        /// 根据ID查询模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型</returns>
        ModelT Select(IdT id, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 根据ID查询模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型</returns>
        ModelT Select(IdT id, string[] propertyNames, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 根据SQL属性查询模型
        /// </summary>
        /// <param name="sqlProp">SQL属性</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型</returns>
        //ModelT SelectFristBy(SqlPropInfo sqlProp, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 根据ID集合查询模型
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型</returns>
        IList<ModelT> Select(IdT[] ids, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 根据ID集合查询模型
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型</returns>
        IList<ModelT> Select(IdT[] ids, string[] propertyNames, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 根据ID统计模型数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型数</returns>
        int Count(IdT id, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 统计模型数
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型数</returns>
        int Count(CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 查询模型列表
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型列表</returns>
        IList<ModelT> Select(CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 查询模型列表
        /// </summary>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型列表</returns>
        IList<ModelT> Select(string[] propertyNames, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>分页信息</returns>
        PagingInfo<ModelT> SelectPage(int pageIndex, int pageSize, FilterInfo filter = null, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="filter">筛选</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>分页信息</returns>
        PagingInfo<ModelT> SelectPage(int pageIndex, int pageSize, string[] propertyNames, FilterInfo filter = null, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        /// <returns>所有字段映射集合</returns>
        string[] AllFieldMapProps();

        /// <summary>
        /// 根据ID和大于修改时间查询修改信息（多用于乐观锁的判断，以修改时间为判断）
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="mode">访问模式，默认为主库</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>只有修改信息的模型</returns>
        ModelT SelectModifyInfoByIdAndGeModifyTime(ModelT model, AccessMode mode = AccessMode.MASTER, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 根据ID和大于修改时间查询修改信息列表（多用于乐观锁的判断，以修改时间为判断）
        /// </summary>
        /// <param name="models">模型数组</param>
        /// <param name="mode">访问模式，默认为主库</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>只有修改信息的模型列表</returns>
        IList<ModelT> SelectModifyInfosByIdAndGeModifyTime(ModelT[] models, AccessMode mode = AccessMode.MASTER, CommonUseData comData = null, string connectionId = null);

        #endregion

        #region 写入方法

        /// <summary>
        /// 插入模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int Insert(ModelT model, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 插入模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int Insert(ModelT model, string[] propertyNames, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 插入模型列表
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int Insert(IList<ModelT> models, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 插入模型列表
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int Insert(IList<ModelT> models, string[] propertyNames = null, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 根据ID更新模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int UpdateById(ModelT model, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 根据ID更新模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int UpdateById(ModelT model, string[] propertyNames, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 根据ID删除模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int DeleteById(IdT id, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 根据ID数组删除模型
        /// </summary>
        /// <param name="ids">ID数组</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int DeleteByIds(IdT[] ids, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 删除所有模型
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int Delete(CommonUseData comData = null, string connectionId = null);

        #endregion

        /// <summary>
        /// 严格判断异常是否主键重复
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>异常是否主键重复</returns>
        bool StrictnessIsExceptionPkRepeat(Exception ex);
    }
}
