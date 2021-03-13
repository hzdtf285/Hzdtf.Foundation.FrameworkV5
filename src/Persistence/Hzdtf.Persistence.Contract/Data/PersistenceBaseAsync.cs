using Hzdtf.Persistence.Contract.Management;
using Hzdtf.Utility.Enums;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Page;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Persistence.Contract.Data
{
    /// <summary>
    /// 异步持久化基类
    /// @ 黄振东
    /// </summary>
    public abstract partial class PersistenceBase<IdT, ModelT> 
        where ModelT : SimpleInfo<IdT>
    {
        #region 读取方法

        /// <summary>
        /// 异步根据ID查询模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>模型任务</returns>
        public virtual Task<ModelT> SelectAsync(IdT id, ref string connectionId, CommonUseData comData = null)
        {
            Task<ModelT> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<ModelT>(connId, isClose, dbConn, (dbTrans) =>
                {
                    return Select(id, dbConn, dbTrans, comData: comData);
                }, AccessMode.SLAVE);
            }, accessMode: AccessMode.SLAVE);

            return task;
        }

        /// <summary>
        /// 异步根据ID查询模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>模型任务</returns>
        public virtual Task<ModelT> SelectAsync(IdT id, string[] propertyNames, ref string connectionId, CommonUseData comData = null)
        {
            Task<ModelT> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<ModelT>(connId, isClose, dbConn, (dbTrans) =>
                {
                    return Select(id, dbConn, propertyNames: propertyNames, dbTransaction: dbTrans, comData: comData);
                }, AccessMode.SLAVE);
            }, accessMode: AccessMode.SLAVE);

            return task;
        }

        /// <summary>
        /// 异步根据ID集合查询模型
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>模型任务</returns>
        public virtual Task<IList<ModelT>> SelectAsync(IdT[] ids, ref string connectionId, CommonUseData comData = null)
        {
            Task<IList<ModelT>> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<IList<ModelT>>(connId, isClose, dbConn, (dbTrans) =>
                {
                    return Select(ids, dbConn, dbTransaction: dbTrans, comData: comData);
                }, AccessMode.SLAVE);
            }, accessMode: AccessMode.SLAVE);

            return task;
        }

        /// <summary>
        /// 异步根据ID集合查询模型
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>模型任务</returns>
        public virtual Task<IList<ModelT>> SelectAsync(IdT[] ids, string[] propertyNames, ref string connectionId, CommonUseData comData = null)
        {
            Task<IList<ModelT>> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<IList<ModelT>>(connId, isClose, dbConn, (dbTrans) =>
                {
                    return Select(ids, dbConn, propertyNames: propertyNames, dbTransaction: dbTrans, comData: comData);
                }, AccessMode.SLAVE);
            }, accessMode: AccessMode.SLAVE);

            return task;
        }

        /// <summary>
        /// 异步根据ID统计模型数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>模型数任务</returns>
        public virtual Task<int> CountAsync(IdT id, ref string connectionId, CommonUseData comData = null)
        {
            Task<int> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<int>(connId, isClose, dbConn, (dbTrans) =>
                {
                    return Count(id, dbConn, dbTransaction: dbTrans, comData: comData);
                }, AccessMode.SLAVE);
            }, accessMode: AccessMode.SLAVE);

            return task;
        }

        /// <summary>
        /// 异步统计模型数
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>模型数任务</returns>
        public virtual Task<int> CountAsync(ref string connectionId, CommonUseData comData = null)
        {
            Task<int> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<int>(connId, isClose, dbConn, (dbTrans) =>
                {
                    return Count(dbConn, dbTransaction: dbTrans, comData: comData);
                }, AccessMode.SLAVE);
            }, accessMode: AccessMode.SLAVE);

            return task;
        }

        /// <summary>
        /// 异步查询模型列表
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>模型列表任务</returns>
        public virtual Task<IList<ModelT>> SelectAsync(ref string connectionId, CommonUseData comData = null)
        {
            Task<IList<ModelT>> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<IList<ModelT>>(connId, isClose, dbConn, (dbTrans) =>
                {
                    return Select(dbConn, dbTransaction: dbTrans, comData: comData);
                }, AccessMode.SLAVE);
            }, accessMode: AccessMode.SLAVE);

            return task;
        }

        /// <summary>
        /// 异步查询模型列表
        /// </summary>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>模型列表任务</returns>
        public virtual Task<IList<ModelT>> SelectAsync(string[] propertyNames, ref string connectionId, CommonUseData comData = null)
        {
            Task<IList<ModelT>> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<IList<ModelT>>(connId, isClose, dbConn, (dbTrans) =>
                {
                    return Select(dbConn, propertyNames: propertyNames, dbTransaction: dbTrans, comData: comData);
                }, AccessMode.SLAVE);
            }, accessMode: AccessMode.SLAVE);

            return task;
        }

        /// <summary>
        /// 异步查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="filter">筛选</param>
        /// <param name="comData">通用数据</param>
        /// <returns>分页信息任务</returns>
        public virtual Task<PagingInfo<ModelT>> SelectPageAsync(int pageIndex, int pageSize, ref string connectionId, FilterInfo filter = null, CommonUseData comData = null)
        {
            Task<PagingInfo<ModelT>> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<PagingInfo<ModelT>>(connId, isClose, dbConn, (dbTrans) =>
                {
                    return SelectPage(pageIndex, pageSize, dbConn, filter, dbTransaction: dbTrans, comData: comData);
                }, AccessMode.SLAVE);
            }, accessMode: AccessMode.SLAVE);

            return task;
        }

        /// <summary>
        /// 异步查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="filter">筛选</param>
        /// <param name="comData">通用数据</param>
        /// <returns>分页信息任务</returns>
        public virtual Task<PagingInfo<ModelT>> SelectPageAsync(int pageIndex, int pageSize, string[] propertyNames, ref string connectionId, FilterInfo filter = null, CommonUseData comData = null)
        {
            Task<PagingInfo<ModelT>> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<PagingInfo<ModelT>>(connId, isClose, dbConn, (dbTrans) =>
                {
                    return SelectPage(pageIndex, pageSize, dbConn, filter, propertyNames: propertyNames, dbTransaction: dbTrans, comData: comData);
                }, AccessMode.SLAVE);
            }, accessMode: AccessMode.SLAVE);

            return task;
        }

        /// <summary>
        /// 异步根据ID和大于修改时间查询修改信息（多用于乐观锁的判断，以修改时间为判断）
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="mode">访问模式，默认为主库</param>
        /// <param name="comData">通用数据</param>
        /// <returns>只有修改信息的模型任务</returns>
        public virtual Task<ModelT> SelectModifyInfoByIdAndGeModifyTimeAsync(ModelT model, ref string connectionId, AccessMode mode = AccessMode.MASTER, CommonUseData comData = null)
        {
            Task<ModelT> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<ModelT>(connId, isClose, dbConn, (dbTrans) =>
                {
                    return SelectModifyInfoByIdAndGeModifyTime(model: model, mode: mode, connectionId: connId);
                }, mode);
            }, accessMode: mode);

            return task;
        }

        /// <summary>
        /// 异步根据ID和大于修改时间查询修改信息列表（多用于乐观锁的判断，以修改时间为判断）
        /// </summary>
        /// <param name="models">模型数组</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="mode">访问模式，默认为主库</param>
        /// <param name="comData">通用数据</param>
        /// <returns>只有修改信息的模型列表任务</returns>
        public virtual Task<IList<ModelT>> SelectModifyInfosByIdAndGeModifyTimeAsync(ModelT[] models, ref string connectionId, AccessMode mode = AccessMode.MASTER, CommonUseData comData = null)
        {
            Task<IList<ModelT>> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<IList<ModelT>>(connId, isClose, dbConn, (dbTrans) =>
                {
                    return SelectModifyInfosByIdAndGeModifyTime(models: models, mode: mode, connectionId: connId);
                }, mode);
            }, accessMode: mode);

            return task;
        }

        #endregion

        #region 写入方法

        /// <summary>
        /// 异步插入模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>影响行数任务</returns>
        public virtual Task<int> InsertAsync(ModelT model, ref string connectionId, CommonUseData comData = null)
        {
            Task<int> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<int>(connId, isClose, dbConn, (dbTrans) =>
                {
                    return Insert(model, dbConn, dbTransaction: dbTrans, comData: comData);
                });
            });

            return task;
        }

        /// <summary>
        /// 异步插入模型列表
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>影响行数任务</returns>
        public virtual Task<int> InsertAsync(IList<ModelT> models, ref string connectionId, CommonUseData comData = null)
        {
            Task<int> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<int>(connId, isClose, dbConn, (dbTrans) =>
                {
                    return Insert(models, dbConn, dbTransaction: dbTrans, comData: comData);
                });
            });

            return task;
        }

        /// <summary>
        /// 异步根据ID更新模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>影响行数任务</returns>
        public virtual Task<int> UpdateByIdAsync(ModelT model, ref string connectionId, CommonUseData comData = null)
        {
            Task<int> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<int>(connId, isClose, dbConn, (dbTrans) =>
                {
                    return UpdateById(model, dbConn, dbTransaction: dbTrans, comData: comData);
                });
            });

            return task;
        }

        /// <summary>
        /// 异步根据ID更新模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>影响行数任务</returns>
        public virtual Task<int> UpdateByIdAsync(ModelT model, string[] propertyNames, ref string connectionId, CommonUseData comData = null)
        {
            Task<int> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<int>(connId, isClose, dbConn, (dbTrans) =>
                {
                    return UpdateById(model, dbConn, propertyNames: propertyNames, dbTransaction: dbTrans, comData: comData);
                });
            });

            return task;
        }

        /// <summary>
        /// 异步根据ID删除模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>影响行数任务</returns>
        public virtual Task<int> DeleteByIdAsync(IdT id, ref string connectionId, CommonUseData comData = null)
        {
            Task<int> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<int>(connId, isClose, dbConn, (dbTrans) =>
                {
                    return DeleteById(id, dbConn, dbTransaction: dbTrans, comData: comData);
                });
            });

            return task;
        }

        /// <summary>
        /// 异步根据ID数组删除模型
        /// </summary>
        /// <param name="ids">ID数组</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>影响行数任务</returns>
        public virtual Task<int> DeleteByIdsAsync(IdT[] ids, ref string connectionId, CommonUseData comData = null)
        {
            Task<int> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<int>(connId, isClose, dbConn, (dbTrans) =>
                {
                    return DeleteByIds(ids, dbConn, dbTransaction: dbTrans, comData: comData);
                });
            });

            return task;
        }

        /// <summary>
        /// 异步删除所有模型
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>影响行数任务</returns>
        public virtual Task<int> DeleteAsync(ref string connectionId, CommonUseData comData = null)
        {
            Task<int> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<int>(connId, isClose, dbConn, (dbTrans) =>
                {
                    return Delete(dbConn, dbTransaction: dbTrans, comData: comData);
                });
            });

            return task;
        }

        #endregion

        #region 受保护的方法

        /// <summary>
        /// 异步执行
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="connectionId">连接ID</param>
        /// <param name="isClose">是否关闭</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="func">函数</param>
        /// <param name="mode">访问模式</param>
        /// <returns>结果任务</returns>
        protected async Task<TResult> ExecAsync<TResult>(string connectionId, bool isClose, IDbConnection dbConnection, Func<IDbTransaction, TResult> func, AccessMode mode = AccessMode.MASTER)
        {
            var dbTrans = DbConnectionManager.GetDbTransaction(connectionId, this, mode);
            return await Task.Run<TResult>(() =>
            {
                TResult result = func(dbTrans);
                if (isClose)
                {
                    DbConnectionManager.Release(connectionId, dbConnection);
                }

                return result;
            });
        }

        #endregion
    }
}
