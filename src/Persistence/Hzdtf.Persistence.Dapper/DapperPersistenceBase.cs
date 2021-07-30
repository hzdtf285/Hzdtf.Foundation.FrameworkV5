using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Page;
using System;
using System.Collections.Generic;
using Dapper;
using System.Data;
using Hzdtf.Utility.Utils;
using Hzdtf.Persistence.Contract.Data;
using Hzdtf.Persistence.Contract.PermissionFilter;
using System.Linq;

namespace Hzdtf.Persistence.Dapper
{
    /// <summary>
    /// Dapper持久化基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    /// <typeparam name="ModelT">模型类型</typeparam>
    public abstract partial class DapperPersistenceBase<IdT, ModelT> : PersistenceBase<IdT, ModelT>
        where ModelT : SimpleInfo<IdT>
    {
        #region 属性与字段

        /// <summary>
        /// 表
        /// </summary>
        public abstract string Table { get; }

        #endregion

        #region 读取方法

        /// <summary>
        /// 根据ID查询模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <returns>模型</returns>
        protected override ModelT Select(IdT id, IDbConnection dbConnection, IDbTransaction dbTransaction = null, string[] propertyNames = null, CommonUseData comData = null)
        {
            DynamicParameters dataParameter = null;
            var dataPermissionSql = ExecDataPermissionFilter("Select", dbConnection, dbTransaction, ref dataParameter, comData: comData);

            bool sqlEmptyNotFilter;
            var fieldPermissionSql = ExecFieldPermissionFilter("Select", dbConnection, dbTransaction, out sqlEmptyNotFilter, comData: comData);
            if (string.IsNullOrWhiteSpace(fieldPermissionSql) && !sqlEmptyNotFilter)
            {
                return null;
            }

            var sql = SelectSql(id, dataPermissionSql, fieldPermissionSql, propertyNames, comData: comData);
            if (dataParameter == null)
            {
                dataParameter = new DynamicParameters();
            }
            dataParameter.Add("@Id", id);

            return ExecRecordSqlLog<ModelT>(sql, () =>
            {
                return dbConnection.QueryFirstOrDefault<ModelT>(sql, dataParameter, dbTransaction);
            }, comData: comData, "Select");
        }

        /// <summary>
        /// 根据ID查询模型
        /// </summary>
        /// <param name="sqlProp">SQL属性</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="comData">通用数据</param>
        /// <returns>模型</returns>
        //protected override ModelT SelectFristBy(SqlPropInfo sqlProp, IDbConnection dbConnection, IDbTransaction dbTransaction = null, CommonUseData comData = null)
        //{

        //}

        /// <summary>
        /// 根据ID集合查询模型
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <returns>模型列表</returns>
        protected override IList<ModelT> Select(IdT[] ids, IDbConnection dbConnection, IDbTransaction dbTransaction = null, string[] propertyNames = null, CommonUseData comData = null)
        {
            DynamicParameters dataParameter = null;
            var dataPermissionSql = ExecDataPermissionFilter("Select", dbConnection, dbTransaction, ref dataParameter, comData: comData);

            bool sqlEmptyNotFilter;
            var fieldPermissionSql = ExecFieldPermissionFilter("Select", dbConnection, dbTransaction, out sqlEmptyNotFilter, comData: comData);
            if (string.IsNullOrWhiteSpace(fieldPermissionSql) && !sqlEmptyNotFilter)
            {
                return null;
            }

            DynamicParameters parameters;
            var sql = SelectSql(ids, dataPermissionSql, fieldPermissionSql, out parameters, propertyNames, comData: comData);

            return ExecRecordSqlLog<IList<ModelT>>(sql, () =>
            {
                return dbConnection.Query<ModelT>(sql, DapperUtil.MergeParams(dataParameter, parameters), dbTransaction).AsList();
            }, comData: comData, "Select");
        }

        /// <summary>
        /// 根据ID统计模型数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="comData">通用数据</param>
        /// <returns>模型数</returns>
        protected override int Count(IdT id, IDbConnection dbConnection, IDbTransaction dbTransaction = null, CommonUseData comData = null)
        {
            DynamicParameters dataParameter = null;
            var dataPermissionSql = ExecDataPermissionFilter("Count", dbConnection, dbTransaction, ref dataParameter, comData: comData);
            var sql = CountSql(id, dataPermissionSql, comData: comData);

            if (dataParameter == null)
            {
                dataParameter = new DynamicParameters();
            }
            dataParameter.Add("@Id", id);

            return ExecRecordSqlLog<int>(sql, () =>
            {
                return dbConnection.ExecuteScalar<int>(sql, dataParameter, dbTransaction);
            }, comData: comData, "Count");
        }

        /// <summary>
        /// 统计模型数
        /// </summary>
        /// <returns>模型数</returns>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="comData">通用数据</param>
        protected override int Count(IDbConnection dbConnection, IDbTransaction dbTransaction = null, CommonUseData comData = null)
        {
            DynamicParameters dataParameter = null;
            var dataPermissionSql = ExecDataPermissionFilter("Count", dbConnection, dbTransaction, ref dataParameter, comData: comData);
            var sql = CountSql(dataPermissionSql: dataPermissionSql, comData: comData);

            return ExecRecordSqlLog<int>(sql, () =>
            {
                return dbConnection.ExecuteScalar<int>(sql, dataParameter, dbTransaction);
            }, comData: comData, "Count");
        }

        /// <summary>
        /// 查询模型列表
        /// </summary>
        /// <returns>模型列表</returns>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        protected override IList<ModelT> Select(IDbConnection dbConnection, IDbTransaction dbTransaction = null, string[] propertyNames = null, CommonUseData comData = null)
        {
            DynamicParameters dataParameter = null;
            var dataPermissionSql = ExecDataPermissionFilter("Select", dbConnection, dbTransaction, ref dataParameter, comData: comData);

            bool sqlEmptyNotFilter;
            var fieldPermissionSql = ExecFieldPermissionFilter("Select", dbConnection, dbTransaction, out sqlEmptyNotFilter, comData: comData);
            if (string.IsNullOrWhiteSpace(fieldPermissionSql) && !sqlEmptyNotFilter)
            {
                return null;
            }

            var sql = SelectSql(propertyNames: propertyNames, dataPermissionSql: dataPermissionSql, fieldPermissionSql: fieldPermissionSql, comData: comData);

            return ExecRecordSqlLog<IList<ModelT>>(sql, () =>
            {
                return dbConnection.Query<ModelT>(sql, dataParameter, dbTransaction).AsList();
            }, comData: comData, "Select");
        }

        /// <summary>
        /// 查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <returns>分页信息</returns>
        protected override PagingInfo<ModelT> SelectPage(int pageIndex, int pageSize, IDbConnection dbConnection, FilterInfo filter = null, IDbTransaction dbTransaction = null, string[] propertyNames = null, CommonUseData comData = null)
        {
            BeforeFilterInfo(filter, comData);
            DynamicParameters dataParameter = null;
            string dataPermissionSql = null;
            return PagingUtil.ExecPage<ModelT>(pageIndex, pageSize, () =>
            {
                dataPermissionSql = ExecDataPermissionFilter("SelectPage", dbConnection, dbTransaction, ref dataParameter, comData: comData);

                DynamicParameters parameters;
                var countSql = CountByFilterSql(filter, dataPermissionSql, out parameters, comData: comData);

                return ExecRecordSqlLog<int>(countSql, () =>
                {
                    return dbConnection.ExecuteScalar<int>(countSql, DapperUtil.MergeParams(dataParameter, parameters), dbTransaction);
                }, comData: comData, "SelectPage");
            }, () =>
            {
                bool sqlEmptyNotFilter;
                var fieldPermissionSql = ExecFieldPermissionFilter("SelectPage", dbConnection, dbTransaction, out sqlEmptyNotFilter, comData: comData);
                if (string.IsNullOrWhiteSpace(fieldPermissionSql) && !sqlEmptyNotFilter)
                {
                    return null;
                }

                DynamicParameters parameters;
                var pageSql = SelectPageSql(pageIndex, pageSize, dataPermissionSql, fieldPermissionSql, out parameters, filter, propertyNames, comData: comData);

                return ExecRecordSqlLog<IList<ModelT>>(pageSql, () =>
                {
                    return dbConnection.Query<ModelT>(pageSql, DapperUtil.MergeParams(dataParameter, parameters), dbTransaction).AsList();
                }, comData: comData, "SelectPage");
            });
        }

        /// <summary>
        /// 根据ID和大于修改时间查询修改信息（多用于乐观锁的判断，以修改时间为判断）
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="comData">通用数据</param>
        /// <returns>只有修改信息的模型</returns>
        protected override ModelT SelectModifyInfoByIdAndGeModifyTime(ModelT model, IDbConnection dbConnection, IDbTransaction dbTransaction = null, CommonUseData comData = null)
        {
            var sql = SelectModifyInfoByIdAndGeModifyTimeSql(model, comData: comData);

            return ExecRecordSqlLog<ModelT>(sql, () =>
            {
                return dbConnection.QueryFirstOrDefault<ModelT>(sql: sql, param: model, transaction: dbTransaction);
            }, comData: comData, "SelectModifyInfoByIdAndGeModifyTime");
        }

        /// <summary>
        /// 根据ID和大于修改时间查询修改信息列表（多用于乐观锁的判断，以修改时间为判断）
        /// </summary>
        /// <param name="models">模型数组</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="comData">通用数据</param>
        /// <returns>只有修改信息的模型列表</returns>
        protected override IList<ModelT> SelectModifyInfosByIdAndGeModifyTime(ModelT[] models, IDbConnection dbConnection, IDbTransaction dbTransaction = null, CommonUseData comData = null)
        {
            DynamicParameters parameters;
            var sql = SelectModifyInfosByIdAndGeModifyTimeSql(models, out parameters, comData: comData);

            return ExecRecordSqlLog<IList<ModelT>>(sql, () =>
            {
                return dbConnection.Query<ModelT>(sql: sql, param: parameters, transaction: dbTransaction).AsList();
            }, comData: comData, "SelectModifyInfosByIdAndGeModifyTime");
        }

        #endregion

        #region 写入方法

        /// <summary>
        /// 插入模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="comData">通用数据</param>
        /// <returns>影响行数</returns>
        protected override int Insert(ModelT model, IDbConnection dbConnection, string[] propertyNames = null, IDbTransaction dbTransaction = null, CommonUseData comData = null)
        {
            var isAuto = PrimaryKeyIncr(model.Id);
            var sql = InsertSql(model, propertyNames, isAuto, comData: comData);
            if (IsSupportIdempotent)
            {
                if (isAuto)
                {
                    throw new NotSupportedException("幂等操作必须由业务生成主键，不能使用数据库自增生成");
                }

                return ExecRecordSqlLog<int>(sql, () =>
                {
                    try
                    {
                        return dbConnection.Execute(sql, model, dbTransaction);
                    }
                    catch (Exception ex)
                    {
                        // 如果主键重复，则忽略异常，实现幂等
                        if (IsExceptionPkRepeat(ex))
                        {
                            Log.InfoAsync($"插入.发生主键重复异常,幂等操作忽略该异常.主键:{model.Id}", ex, this.GetType().Name, comData.GetEventId(), "Insert");
                            return 1;
                        }

                        throw new Exception(ex.Message, ex);
                    }
                }, comData: comData, "Insert");
            }
            else
            {
                if (isAuto)
                {
                    model.Id = ExecRecordSqlLog<IdT>(sql, () =>
                    {
                        return dbConnection.ExecuteScalar<IdT>(sql, model, dbTransaction);
                    }, comData: comData, "Insert");

                    return 1;
                }
                else
                {
                    return ExecRecordSqlLog<int>(sql, () =>
                    {
                        return dbConnection.Execute(sql, model, dbTransaction);
                    }, comData: comData, "Insert");
                }
            }
        }

        /// <summary>
        /// 插入模型列表
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="comData">通用数据</param>
        /// <returns>影响行数</returns>
        protected override int Insert(IList<ModelT> models, IDbConnection dbConnection, string[] propertyNames = null, IDbTransaction dbTransaction = null, CommonUseData comData = null)
        {
            DynamicParameters parameters;
            var sql = InsertSql(models, out parameters, propertyNames, comData: comData);

            return ExecRecordSqlLog<int>(sql, () =>
            {
                if (IsSupportIdempotent)
                {
                    try
                    {
                        return dbConnection.Execute(sql, parameters, dbTransaction);
                    }
                    catch (Exception ex)
                    {
                        // 如果主键重复，则忽略异常，实现幂等
                        if (IsExceptionPkRepeat(ex))
                        {
                            var ids = models.Select(p => p.Id).ToArray();
                            Log.InfoAsync($"批量插入.发生主键重复异常,幂等操作忽略该异常.主键:{ids.ToMergeString(",")}", ex, this.GetType().Name, comData.GetEventId(), "Insert");
                            return models.Count;
                        }

                        throw new Exception(ex.Message, ex);
                    }
                }
                else
                {
                    return dbConnection.Execute(sql, parameters, dbTransaction);
                }
            }, comData: comData, "Insert");
        }

        /// <summary>
        /// 根据ID更新模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <returns>影响行数</returns>
        protected override int UpdateById(ModelT model, IDbConnection dbConnection, IDbTransaction dbTransaction = null, string[] propertyNames = null, CommonUseData comData = null)
        {
            var sql = UpdateByIdSql(model, propertyNames, comData: comData);

            return ExecRecordSqlLog<int>(sql, () =>
            {
                var rowCount = dbConnection.Execute(sql, model, dbTransaction);
                return rowCount == 0 && IsSupportIdempotent ? 1 : rowCount;
            }, comData: comData, "UpdateById");
        }

        /// <summary>
        /// 根据ID删除模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="comData">通用数据</param>
        /// <returns>影响行数</returns>
        protected override int DeleteById(IdT id, IDbConnection dbConnection, IDbTransaction dbTransaction = null, CommonUseData comData = null)
        {
            var sql = DeleteByIdSql(id, comData: comData);

            return ExecRecordSqlLog<int>(sql, () =>
            {
                var rowCount = dbConnection.Execute(sql, new SimpleInfo<IdT>() { Id = id }, dbTransaction);
                return rowCount == 0 && IsSupportIdempotent ? 1 : rowCount;
            }, comData: comData, "DeleteById");
        }

        /// <summary>
        /// 根据ID数组删除模型
        /// </summary>
        /// <param name="ids">ID数组</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="comData">通用数据</param>
        /// <returns>影响行数</returns>
        protected override int DeleteByIds(IdT[] ids, IDbConnection dbConnection, IDbTransaction dbTransaction = null, CommonUseData comData = null)
        {
            DynamicParameters parameters;
            var sql = DeleteByIdsSql(ids, out parameters, comData: comData);

            return ExecRecordSqlLog<int>(sql, () =>
            {
                var rowCount = dbConnection.Execute(sql, parameters, dbTransaction);
                return rowCount == 0 && IsSupportIdempotent ? ids.Length : rowCount;
            }, comData: comData, "DeleteByIds");
        }

        /// <summary>
        /// 删除所有模型
        /// </summary>
        /// <returns>影响行数</returns>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="comData">通用数据</param>
        protected override int Delete(IDbConnection dbConnection, IDbTransaction dbTransaction = null, CommonUseData comData = null)
        {
            var sql = DeleteSql(comData: comData);

            return ExecRecordSqlLog<int>(sql, () =>
            {
                var rowCount = dbConnection.Execute(sql, dbTransaction);
                return rowCount == 0 && IsSupportIdempotent ? 1 : rowCount;
            }, comData: comData, "Delete");
        }

        #endregion

        #region 重写父类的方法

        /// <summary>
        /// 删除从表
        /// </summary>
        /// <param name="table">从表</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="comData">通用数据</param>
        /// <returns>影响行数</returns>
        protected override int DeleteSlaveTable(string table, IDbConnection dbConnection, IDbTransaction dbTransaction = null, CommonUseData comData = null)
        {
            var sql = DeleteByTableSql(table, comData: comData);

            return ExecRecordSqlLog<int>(sql, () =>
            {
                var rowCount = dbConnection.Execute(sql, dbTransaction);
                return rowCount == 0 && IsSupportIdempotent ? 1 : rowCount;
            }, comData: comData, "DeleteSlaveTable");
        }

        /// <summary>
        /// 删除从表
        /// </summary>
        /// <param name="table">从表</param>
        /// <param name="foreignKeyName">外键名称</param>
        /// <param name="foreignKeyValues">外键值集合</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="comData">通用数据</param>
        /// <returns>影响行数</returns>
        protected override int DeleteSlaveTableByForeignKeys(string table, string foreignKeyName, IdT[] foreignKeyValues, IDbConnection dbConnection, IDbTransaction dbTransaction = null, CommonUseData comData = null)
        {
            DynamicParameters parameters;
            var sql = DeleteByTableAndForignKeySql(table, foreignKeyName, foreignKeyValues, out parameters, comData: comData);

            return ExecRecordSqlLog<int>(sql, () =>
            {
                var rowCount = dbConnection.Execute(sql, parameters, dbTransaction);
                return rowCount == 0 && IsSupportIdempotent ? 1 : rowCount;
            }, comData: comData, "DeleteSlaveTableByForeignKeys");
        }

        #endregion

        #region 需要子类重写的方法

        #region 读取方法

        /// <summary>
        /// 根据ID查询模型SQL语句
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="dataPermissionSql">数据权限SQL</param>
        /// <param name="fieldPermissionSql">字段权限SQL</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected abstract string SelectSql(IdT id, string dataPermissionSql, string fieldPermissionSql, string[] propertyNames = null, CommonUseData comData = null);

        /// <summary>
        /// 根据ID集合查询模型列表SQL语句
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="dataPermissionSql">数据权限SQL</param>
        /// <param name="fieldPermissionSql">字段权限SQL</param>
        /// <param name="parameters">参数</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected abstract string SelectSql(IdT[] ids, string dataPermissionSql, string fieldPermissionSql, out DynamicParameters parameters, string[] propertyNames = null, CommonUseData comData = null);

        /// <summary>
        /// 根据ID统计模型数SQL语句
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="dataPermissionSql">数据权限SQL</param>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected abstract string CountSql(IdT id, string dataPermissionSql, CommonUseData comData = null);

        /// <summary>
        /// 统计模型数SQL语句
        /// </summary>
        /// <param name="pfx">前辍</param>
        /// <param name="dataPermissionSql">数据权限SQL</param>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected abstract string CountSql(string pfx = null, string dataPermissionSql = null, CommonUseData comData = null);

        /// <summary>
        /// 根据筛选信息统计模型数SQL语句
        /// </summary>
        /// <param name="filter">筛选信息</param>
        /// <param name="dataPermissionSql">数据权限SQL</param>
        /// <param name="parameters">参数</param>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected abstract string CountByFilterSql(FilterInfo filter, string dataPermissionSql, out DynamicParameters parameters, CommonUseData comData = null);

        /// <summary>
        /// 查询模型列表
        /// </summary>
        /// <param name="pfx">前辍</param>
        /// <param name="appendFieldSqls">追加字段SQL，包含前面的,</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="dataPermissionSql">数据权限SQL</param>
        /// <param name="fieldPermissionSql">字段权限SQL</param>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected abstract string SelectSql(string pfx = null, string appendFieldSqls = null, string[] propertyNames = null, string dataPermissionSql = null, string fieldPermissionSql = null, CommonUseData comData = null);

        /// <summary>
        /// 查询模型列表并分页SQL语句
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="dataPermissionSql">数据权限SQL</param>
        /// <param name="fieldPermissionSql">字段权限SQL</param>
        /// <param name="parameters">参数</param>
        /// <param name="filter">筛选</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected abstract string SelectPageSql(int pageIndex, int pageSize, string dataPermissionSql, string fieldPermissionSql, out DynamicParameters parameters, FilterInfo filter = null, string[] propertyNames = null, CommonUseData comData = null);

        /// <summary>
        /// 根据ID和大于修改时间查询修改信息（多用于乐观锁的判断，以修改时间为判断）
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="comData">通用数据</param>
        /// <returns>只有修改信息的SQL语句</returns>
        protected abstract string SelectModifyInfoByIdAndGeModifyTimeSql(ModelT model, CommonUseData comData = null);

        /// <summary>
        /// 根据ID和大于修改时间查询修改信息列表（多用于乐观锁的判断，以修改时间为判断）
        /// </summary>
        /// <param name="models">模型数组</param>
        /// <param name="parameters">参数</param>
        /// <param name="comData">通用数据</param>
        /// <returns>只有修改信息的SQL语句</returns>
        protected abstract string SelectModifyInfosByIdAndGeModifyTimeSql(ModelT[] models, out DynamicParameters parameters, CommonUseData comData = null);

        #endregion

        #region 写入方法

        /// <summary>
        /// 插入模型SQL语句
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="isGetInsertId">是否获取自增ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected abstract string InsertSql(ModelT model, string[] propertyNames = null, bool isGetInsertId = false, CommonUseData comData = null);

        /// <summary>
        /// 插入模型列表SQL语句
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="parameters">动态参数</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected abstract string InsertSql(IList<ModelT> models, out DynamicParameters parameters, string[] propertyNames = null, CommonUseData comData = null);

        /// <summary>
        /// 根据ID更新模型SQL语句
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected abstract string UpdateByIdSql(ModelT model, string[] propertyNames = null, CommonUseData comData = null);

        /// <summary>
        /// 根据ID删除模型SQL语句
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected abstract string DeleteByIdSql(IdT id, CommonUseData comData = null);

        /// <summary>
        /// 根据ID数组删除模型SQL语句
        /// </summary>
        /// <param name="ids">ID数组</param>
        /// <param name="parameters">参数</param>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected abstract string DeleteByIdsSql(IdT[] ids, out DynamicParameters parameters, CommonUseData comData = null);

        /// <summary>
        /// 删除所有模型SQL语句
        /// </summary>
        /// <returns>SQL语句</returns>
        /// <param name="comData">通用数据</param>
        protected abstract string DeleteSql(CommonUseData comData = null);

        #endregion

        #endregion

        #region 虚方法

        /// <summary>
        /// 根据表名删除所有模型SQL语句
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected virtual string DeleteByTableSql(string table, CommonUseData comData = null) => null;

        /// <summary>
        /// 根据表名、外键字段和外键值删除模型SQL语句
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="foreignKeyName">外键名称</param>
        /// <param name="foreignKeyValues">外键值集合</param>
        /// <param name="parameters">参数</param>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected virtual string DeleteByTableAndForignKeySql(string table, string foreignKeyName, IdT[] foreignKeyValues, out DynamicParameters parameters, CommonUseData comData = null)
        {
            parameters = null;
            return null;
        }

        /// <summary>
        /// 获取查询的排序名称前辍，如果是主表，可以为null或空
        /// </summary>
        /// <param name="filter">筛选</param>
        /// <param name="comData">通用数据</param>
        /// <returns>查询分页的排序名称前辍</returns>
        protected virtual string GetSelectSortNamePfx(FilterInfo filter, CommonUseData comData = null) => filter != null ? GetSelectSortNamePfx(filter.SortName) : null;

        /// <summary>
        /// 获取查询的排序名称前辍，如果是主表，可以为null或空
        /// </summary>
        /// <param name="sortName">排序名称</param>
        /// <returns>查询分页的排序名称前辍</returns>
        protected virtual string GetSelectSortNamePfx(string sortName) => null;

        /// <summary>
        /// 创建数据权限过滤
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="trans">数据库事务</param>
        /// <param name="tbPfx">表前辍</param>
        /// <param name="comData">通用数据</param>
        /// <returns>数据权限过滤</returns>
        protected virtual DataPermissionFilterInfo CreateDataPermissionFilter(string methodName, IDbConnection conn, IDbTransaction trans, string tbPfx = null, CommonUseData comData = null) => new DataPermissionFilterInfo()
        {
            PersistenceClassName = this.GetType().Name,
            Table = Table,
            TablePfx = string.IsNullOrWhiteSpace(tbPfx) ? Table : tbPfx,
            PersistenceMethodName = methodName,
            DbTransaction = trans,
            DbConnection = conn,
            ComData = comData
        };

        /// <summary>
        /// 创建字段权限过滤
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="trans">数据库事务</param>
        /// <param name="tbPfx">表前辍</param>
        /// <param name="comData">通用数据</param>
        /// <returns>字段权限过滤</returns>
        protected virtual FieldPermissionFilterInfo CreateFieldPermissionFilter(string methodName, IDbConnection conn, IDbTransaction trans, string tbPfx = null, CommonUseData comData = null) => new FieldPermissionFilterInfo()
        {
            PersistenceClassName = this.GetType().Name,
            Table = Table,
            TablePfx = string.IsNullOrWhiteSpace(tbPfx) ? Table : tbPfx,
            PersistenceMethodName = methodName,
            DbTransaction = trans,
            DbConnection = conn,
            ComData = comData
        };

        /// <summary>
        /// 执行数据权限过滤
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="trans">数据库事务</param>
        /// <param name="param">参数</param>
        /// <param name="tbPfx">表前辍</param>
        /// <param name="comData">通用数据</param>
        /// <returns>过滤SQL</returns>
        protected virtual string ExecDataPermissionFilter(string methodName, IDbConnection conn, IDbTransaction trans, ref DynamicParameters param, string tbPfx = null, CommonUseData comData = null)
        {
            if (DataPermissionFilter == null)
            {
                return null;
            }
            var perFilter = CreateDataPermissionFilter(methodName, conn, trans, tbPfx, comData);
            DataPermissionFilter.DoFilter(perFilter);
            if (string.IsNullOrWhiteSpace(perFilter.Sql))
            {
                return perFilter.SqlEmptyNotFilter ? null : NoEqualWhereSql();
            }
            else
            {
                if (!perFilter.Params.IsNullOrCount0())
                {
                    if (param == null)
                    {
                        param = new DynamicParameters();
                    }
                    foreach (var p in perFilter.Params)
                    {
                        param.Add(p.Key, p.Value);
                    }
                }
            }

            return perFilter.Sql;
        }

        /// <summary>
        /// 执行字段权限过滤
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="trans">数据库事务</param>
        /// <param name="tbPfx">表前辍</param>
        /// <param name="sqlEmptyNotFilter">SQL为空时则不过滤，默认为是</param>
        /// <param name="comData">通用数据</param>
        /// <returns>过滤SQL</returns>
        protected virtual string ExecFieldPermissionFilter(string methodName, IDbConnection conn, IDbTransaction trans, out bool sqlEmptyNotFilter, string tbPfx = null, CommonUseData comData = null)
        {
            sqlEmptyNotFilter = true;
            if (FieldPermissionFilter == null)
            {
                return null;
            }
            var perFilter = CreateFieldPermissionFilter(methodName, conn, trans, tbPfx, comData);
            FieldPermissionFilter.DoFilter(perFilter);
            sqlEmptyNotFilter = perFilter.SqlEmptyNotFilter;

            return perFilter.Sql;
        }

        /// <summary>
        /// 匹配条件SQL
        /// </summary>
        /// <returns>不匹配条件SQL</returns>
        protected virtual string EqualWhereSql() => " (1=1) ";

        /// <summary>
        /// 不匹配条件SQL
        /// </summary>
        /// <returns>不匹配条件SQL</returns>
        protected virtual string NoEqualWhereSql() => " (1=0) ";

        /// <summary>
        /// 判断异常是否主键重复
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>异常是否主键重复</returns>
        protected virtual bool IsExceptionPkRepeat(Exception ex) => false;

        #endregion
    }
}
