using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Page;
using System;
using System.Collections.Generic;
using Dapper;
using System.Data;
using Hzdtf.Utility.Utils;
using Hzdtf.Persistence.Contract.Data;
using Hzdtf.Persistence.Contract.PermissionFilter;

namespace Hzdtf.Persistence.Dapper
{
    /// <summary>
    /// Dapper持久化基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    /// <typeparam name="ModelT">模型类型</typeparam>
    public abstract partial class DapperPersistenceBase<IdT, ModelT> : PersistenceBase<IdT, ModelT> where ModelT : SimpleInfo<IdT>
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
        /// <returns>模型</returns>
        protected override ModelT Select(IdT id, IDbConnection dbConnection, IDbTransaction dbTransaction = null, string[] propertyNames = null)
        {
            DynamicParameters dataParameter = null;
            var dataPermissionSql = ExecDataPermissionFilter("Select", dbConnection, dbTransaction, ref dataParameter);

            bool sqlEmptyNotFilter;
            var fieldPermissionSql = ExecFieldPermissionFilter("Select", dbConnection, dbTransaction, out sqlEmptyNotFilter);
            if (string.IsNullOrWhiteSpace(fieldPermissionSql) && !sqlEmptyNotFilter)
            {
                return null;
            }

            var sql = SelectSql(id, dataPermissionSql, fieldPermissionSql, propertyNames);
            if (dataParameter == null)
            {
                dataParameter = new DynamicParameters();
            }
            dataParameter.Add("@Id", id);

            return ExecRecordSqlLog<ModelT>(sql, () =>
            {
                return dbConnection.QueryFirstOrDefault<ModelT>(sql, dataParameter, dbTransaction);
            }, "Select");
        }

        /// <summary>
        /// 根据ID集合查询模型
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <returns>模型列表</returns>
        protected override IList<ModelT> Select(IdT[] ids, IDbConnection dbConnection, IDbTransaction dbTransaction = null, string[] propertyNames = null)
        {
            DynamicParameters dataParameter = null;
            var dataPermissionSql = ExecDataPermissionFilter("Select", dbConnection, dbTransaction, ref dataParameter);

            bool sqlEmptyNotFilter;
            var fieldPermissionSql = ExecFieldPermissionFilter("Select", dbConnection, dbTransaction, out sqlEmptyNotFilter);
            if (string.IsNullOrWhiteSpace(fieldPermissionSql) && !sqlEmptyNotFilter)
            {
                return null;
            }

            DynamicParameters parameters;
            var sql = SelectSql(ids, dataPermissionSql, fieldPermissionSql, out parameters, propertyNames);

            return ExecRecordSqlLog<IList<ModelT>>(sql, () =>
            {
                return dbConnection.Query<ModelT>(sql, DapperUtil.MergeParams(dataParameter, parameters), dbTransaction).AsList();
            }, "Select");
        }

        /// <summary>
        /// 根据ID统计模型数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <returns>模型数</returns>
        protected override int Count(IdT id, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DynamicParameters dataParameter = null;
            var dataPermissionSql = ExecDataPermissionFilter("Count", dbConnection, dbTransaction, ref dataParameter);
            var sql = CountSql(id, dataPermissionSql);

            if (dataParameter == null)
            {
                dataParameter = new DynamicParameters();
            }
            dataParameter.Add("@Id", id);

            return ExecRecordSqlLog<int>(sql, () =>
            {
                return dbConnection.ExecuteScalar<int>(sql, dataParameter, dbTransaction);
            }, "Count");
        }

        /// <summary>
        /// 统计模型数
        /// </summary>
        /// <returns>模型数</returns>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        protected override int Count(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DynamicParameters dataParameter = null;
            var dataPermissionSql = ExecDataPermissionFilter("Count", dbConnection, dbTransaction, ref dataParameter);
            var sql = CountSql(dataPermissionSql: dataPermissionSql);

            return ExecRecordSqlLog<int>(sql, () =>
            {
                return dbConnection.ExecuteScalar<int>(sql, dataParameter, dbTransaction);
            }, "Count");
        }

        /// <summary>
        /// 查询模型列表
        /// </summary>
        /// <returns>模型列表</returns>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="propertyNames">属性名称集合</param>
        protected override IList<ModelT> Select(IDbConnection dbConnection, IDbTransaction dbTransaction = null, string[] propertyNames = null)
        {
            DynamicParameters dataParameter = null;
            var dataPermissionSql = ExecDataPermissionFilter("Select", dbConnection, dbTransaction, ref dataParameter);

            bool sqlEmptyNotFilter;
            var fieldPermissionSql = ExecFieldPermissionFilter("Select", dbConnection, dbTransaction, out sqlEmptyNotFilter);
            if (string.IsNullOrWhiteSpace(fieldPermissionSql) && !sqlEmptyNotFilter)
            {
                return null;
            }

            var sql = SelectSql(propertyNames: propertyNames, dataPermissionSql: dataPermissionSql, fieldPermissionSql: fieldPermissionSql);

            return ExecRecordSqlLog<IList<ModelT>>(sql, () =>
            {
                return dbConnection.Query<ModelT>(sql, dataParameter, dbTransaction).AsList();
            }, "Select");
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
        /// <returns>分页信息</returns>
        protected override PagingInfo<ModelT> SelectPage(int pageIndex, int pageSize, IDbConnection dbConnection, FilterInfo filter = null, IDbTransaction dbTransaction = null, string[] propertyNames = null)
        {
            BeforeFilterInfo(filter);
            DynamicParameters dataParameter = null;
            string dataPermissionSql = null;
            return PagingUtil.ExecPage<ModelT>(pageIndex, pageSize, () =>
            {
                dataPermissionSql = ExecDataPermissionFilter("SelectPage", dbConnection, dbTransaction, ref dataParameter);

                DynamicParameters parameters;
                var countSql = CountByFilterSql(filter, dataPermissionSql, out parameters);

                return ExecRecordSqlLog<int>(countSql, () =>
                {
                    return dbConnection.ExecuteScalar<int>(countSql, DapperUtil.MergeParams(dataParameter, parameters), dbTransaction);
                }, "SelectPage");
            }, () =>
            {
                bool sqlEmptyNotFilter;
                var fieldPermissionSql = ExecFieldPermissionFilter("SelectPage", dbConnection, dbTransaction, out sqlEmptyNotFilter);
                if (string.IsNullOrWhiteSpace(fieldPermissionSql) && !sqlEmptyNotFilter)
                {
                    return null;
                }

                DynamicParameters parameters;
                var pageSql = SelectPageSql(pageIndex, pageSize, dataPermissionSql, fieldPermissionSql, out parameters, filter, propertyNames);

                return ExecRecordSqlLog<IList<ModelT>>(pageSql, () =>
                {
                    return dbConnection.Query<ModelT>(pageSql, DapperUtil.MergeParams(dataParameter, parameters), dbTransaction).AsList();
                }, "SelectPage");
            });
        }

        /// <summary>
        /// 根据ID和大于修改时间查询修改信息（多用于乐观锁的判断，以修改时间为判断）
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <returns>只有修改信息的模型</returns>
        protected override ModelT SelectModifyInfoByIdAndGeModifyTime(ModelT model, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            var sql = SelectModifyInfoByIdAndGeModifyTimeSql(model);

            return ExecRecordSqlLog<ModelT>(sql, () =>
            {
                return dbConnection.QueryFirstOrDefault<ModelT>(sql: sql, param: model, transaction: dbTransaction);
            }, "SelectModifyInfoByIdAndGeModifyTime");
        }

        /// <summary>
        /// 根据ID和大于修改时间查询修改信息列表（多用于乐观锁的判断，以修改时间为判断）
        /// </summary>
        /// <param name="models">模型数组</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <returns>只有修改信息的模型列表</returns>
        protected override IList<ModelT> SelectModifyInfosByIdAndGeModifyTime(ModelT[] models, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DynamicParameters parameters;
            var sql = SelectModifyInfosByIdAndGeModifyTimeSql(models, out parameters);

            return ExecRecordSqlLog<IList<ModelT>>(sql, () =>
            {
                return dbConnection.Query<ModelT>(sql: sql, param: parameters, transaction: dbTransaction).AsList();
            }, "SelectModifyInfosByIdAndGeModifyTime");
        }

        #endregion

        #region 写入方法

        /// <summary>
        /// 插入模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <returns>影响行数</returns>
        protected override int Insert(ModelT model, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            var isAuto = PrimaryKeyIncr(model.Id);
            var sql = InsertSql(model, isAuto);
            if (isAuto)
            {
                model.Id = ExecRecordSqlLog<IdT>(sql, () =>
                {
                    return dbConnection.ExecuteScalar<IdT>(sql, model, dbTransaction);
                }, "Insert");

                return 1;
            }
            else
            {
                return ExecRecordSqlLog<int>(sql, () =>
                {
                    return dbConnection.Execute(sql, model, dbTransaction);
                }, "Insert");
            }
        }

        /// <summary>
        /// 插入模型列表
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <returns>影响行数</returns>
        protected override int Insert(IList<ModelT> models, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DynamicParameters parameters;
            var sql = InsertSql(models, out parameters);

            return ExecRecordSqlLog<int>(sql, () =>
            {
                return dbConnection.Execute(sql, parameters, dbTransaction);
            }, "Insert");
        }

        /// <summary>
        /// 根据ID更新模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <returns>影响行数</returns>
        protected override int UpdateById(ModelT model, IDbConnection dbConnection, IDbTransaction dbTransaction = null, string[] propertyNames = null)
        {
            var sql = UpdateByIdSql(model, propertyNames);

            return ExecRecordSqlLog<int>(sql, () =>
            {
                return dbConnection.Execute(sql, model, dbTransaction);
            }, "UpdateById");
        }

        /// <summary>
        /// 根据ID删除模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <returns>影响行数</returns>
        protected override int DeleteById(IdT id, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            var sql = DeleteByIdSql(id);

            return ExecRecordSqlLog<int>(sql, () =>
            {
                return dbConnection.Execute(sql, new SimpleInfo<IdT>() { Id = id }, dbTransaction);
            }, "DeleteById");
        }

        /// <summary>
        /// 根据ID数组删除模型
        /// </summary>
        /// <param name="ids">ID数组</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <returns>影响行数</returns>
        protected override int DeleteByIds(IdT[] ids, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DynamicParameters parameters;
            var sql = DeleteByIdsSql(ids, out parameters);

            return ExecRecordSqlLog<int>(sql, () =>
            {
                return dbConnection.Execute(sql, parameters, dbTransaction);
            }, "DeleteByIds");
        }

        /// <summary>
        /// 删除所有模型
        /// </summary>
        /// <returns>影响行数</returns>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        protected override int Delete(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            var sql = DeleteSql();

            return ExecRecordSqlLog<int>(sql, () =>
            {
                return dbConnection.Execute(sql, dbTransaction);
            }, "Delete");
        }

        #endregion

        #region 重写父类的方法

        /// <summary>
        /// 删除从表
        /// </summary>
        /// <param name="table">从表</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <returns>影响行数</returns>
        protected override int DeleteSlaveTable(string table, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            var sql = DeleteByTableSql(table);

            return ExecRecordSqlLog<int>(sql, () =>
            {
                return dbConnection.Execute(sql, dbTransaction);
            }, "DeleteSlaveTable");
        }

        /// <summary>
        /// 删除从表
        /// </summary>
        /// <param name="table">从表</param>
        /// <param name="foreignKeyName">外键名称</param>
        /// <param name="foreignKeyValues">外键值集合</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <returns>影响行数</returns>
        protected override int DeleteSlaveTableByForeignKeys(string table, string foreignKeyName, IdT[] foreignKeyValues, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DynamicParameters parameters;
            var sql = DeleteByTableAndForignKeySql(table, foreignKeyName, foreignKeyValues, out parameters);

            return ExecRecordSqlLog<int>(sql, () =>
            {
                return dbConnection.Execute(sql, parameters, dbTransaction);
            }, "DeleteSlaveTableByForeignKeys");
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
        /// <returns>SQL语句</returns>
        protected abstract string SelectSql(IdT id, string dataPermissionSql, string fieldPermissionSql, string[] propertyNames = null);

        /// <summary>
        /// 根据ID集合查询模型列表SQL语句
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="dataPermissionSql">数据权限SQL</param>
        /// <param name="fieldPermissionSql">字段权限SQL</param>
        /// <param name="parameters">参数</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <returns>SQL语句</returns>
        protected abstract string SelectSql(IdT[] ids, string dataPermissionSql, string fieldPermissionSql, out DynamicParameters parameters, string[] propertyNames = null);

        /// <summary>
        /// 根据ID统计模型数SQL语句
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="dataPermissionSql">数据权限SQL</param>
        /// <returns>SQL语句</returns>
        protected abstract string CountSql(IdT id, string dataPermissionSql);

        /// <summary>
        /// 统计模型数SQL语句
        /// </summary>
        /// <param name="pfx">前辍</param>
        /// <param name="dataPermissionSql">数据权限SQL</param>
        /// <returns>SQL语句</returns>
        protected abstract string CountSql(string pfx = null, string dataPermissionSql = null);

        /// <summary>
        /// 根据筛选信息统计模型数SQL语句
        /// </summary>
        /// <param name="filter">筛选信息</param>
        /// <param name="dataPermissionSql">数据权限SQL</param>
        /// <param name="parameters">参数</param>
        /// <returns>SQL语句</returns>
        protected abstract string CountByFilterSql(FilterInfo filter, string dataPermissionSql, out DynamicParameters parameters);

        /// <summary>
        /// 查询模型列表
        /// </summary>
        /// <param name="pfx">前辍</param>
        /// <param name="appendFieldSqls">追加字段SQL，包含前面的,</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="dataPermissionSql">数据权限SQL</param>
        /// <param name="fieldPermissionSql">字段权限SQL</param>
        /// <returns>SQL语句</returns>
        protected abstract string SelectSql(string pfx = null, string appendFieldSqls = null, string[] propertyNames = null, string dataPermissionSql = null, string fieldPermissionSql = null);

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
        /// <returns>SQL语句</returns>
        protected abstract string SelectPageSql(int pageIndex, int pageSize, string dataPermissionSql, string fieldPermissionSql, out DynamicParameters parameters, FilterInfo filter = null, string[] propertyNames = null);

        /// <summary>
        /// 根据ID和大于修改时间查询修改信息（多用于乐观锁的判断，以修改时间为判断）
        /// </summary>
        /// <param name="model">模型</param>
        /// <returns>只有修改信息的模型</returns>
        protected abstract string SelectModifyInfoByIdAndGeModifyTimeSql(ModelT model);

        /// <summary>
        /// 根据ID和大于修改时间查询修改信息列表（多用于乐观锁的判断，以修改时间为判断）
        /// </summary>
        /// <param name="models">模型数组</param>
        /// <param name="parameters">参数</param>
        /// <returns>只有修改信息的模型列表</returns>
        protected abstract string SelectModifyInfosByIdAndGeModifyTimeSql(ModelT[] models, out DynamicParameters parameters);

        #endregion

        #region 写入方法

        /// <summary>
        /// 插入模型SQL语句
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="isGetInsertId">是否获取自增ID</param>
        /// <returns>SQL语句</returns>
        protected abstract string InsertSql(ModelT model, bool isGetInsertId = false);

        /// <summary>
        /// 插入模型列表SQL语句
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="parameters">动态参数</param>
        /// <returns>SQL语句</returns>
        protected abstract string InsertSql(IList<ModelT> models, out DynamicParameters parameters);

        /// <summary>
        /// 根据ID更新模型SQL语句
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <returns>SQL语句</returns>
        protected abstract string UpdateByIdSql(ModelT model, string[] propertyNames = null);

        /// <summary>
        /// 根据ID删除模型SQL语句
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>SQL语句</returns>
        protected abstract string DeleteByIdSql(IdT id);

        /// <summary>
        /// 根据ID数组删除模型SQL语句
        /// </summary>
        /// <param name="ids">ID数组</param>
        /// <param name="parameters">参数</param>
        /// <returns>SQL语句</returns>
        protected abstract string DeleteByIdsSql(IdT[] ids, out DynamicParameters parameters);

        /// <summary>
        /// 删除所有模型SQL语句
        /// </summary>
        /// <returns>SQL语句</returns>
        protected abstract string DeleteSql();

        #endregion

        #endregion

        #region 虚方法

        /// <summary>
        /// 根据表名删除所有模型SQL语句
        /// </summary>
        /// <param name="table">表名</param>
        /// <returns>SQL语句</returns>
        protected virtual string DeleteByTableSql(string table) => null;

        /// <summary>
        /// 根据表名、外键字段和外键值删除模型SQL语句
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="foreignKeyName">外键名称</param>
        /// <param name="foreignKeyValues">外键值集合</param>
        /// <param name="parameters">参数</param>
        /// <returns>SQL语句</returns>
        protected virtual string DeleteByTableAndForignKeySql(string table, string foreignKeyName, IdT[] foreignKeyValues, out DynamicParameters parameters)
        {
            parameters = null;
            return null;
        }

        /// <summary>
        /// 获取查询的排序名称前辍，如果是主表，可以为null或空
        /// </summary>
        /// <param name="filter">筛选</param>
        /// <returns>查询分页的排序名称前辍</returns>
        protected virtual string GetSelectSortNamePfx(FilterInfo filter) => filter != null ? GetSelectSortNamePfx(filter.SortName) : null;

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
        /// <returns>数据权限过滤</returns>
        protected virtual DataPermissionFilterInfo CreateDataPermissionFilter(string methodName, IDbConnection conn, IDbTransaction trans, string tbPfx = null) => new DataPermissionFilterInfo()
        {
            PersistenceClassName = this.GetType().Name,
            Table = Table,
            TablePfx = string.IsNullOrWhiteSpace(tbPfx) ? Table : tbPfx,
            PersistenceMethodName = methodName,
            DbTransaction = trans,
            DbConnection = conn
        };

        /// <summary>
        /// 创建字段权限过滤
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="trans">数据库事务</param>
        /// <param name="tbPfx">表前辍</param>
        /// <returns>字段权限过滤</returns>
        protected virtual FieldPermissionFilterInfo CreateFieldPermissionFilter(string methodName, IDbConnection conn, IDbTransaction trans, string tbPfx = null) => new FieldPermissionFilterInfo()
        {
            PersistenceClassName = this.GetType().Name,
            Table = Table,
            TablePfx = string.IsNullOrWhiteSpace(tbPfx) ? Table : tbPfx,
            PersistenceMethodName = methodName,
            DbTransaction = trans,
            DbConnection = conn
        };

        /// <summary>
        /// 执行数据权限过滤
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="trans">数据库事务</param>
        /// <param name="param">参数</param>
        /// <param name="tbPfx">表前辍</param>
        /// <returns>过滤SQL</returns>
        protected virtual string ExecDataPermissionFilter(string methodName, IDbConnection conn, IDbTransaction trans, ref DynamicParameters param, string tbPfx = null)
        {
            if (DataPermissionFilter == null)
            {
                return null;
            }
            var perFilter = CreateDataPermissionFilter(methodName, conn, trans, tbPfx);
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
        /// <returns>过滤SQL</returns>
        protected virtual string ExecFieldPermissionFilter(string methodName, IDbConnection conn, IDbTransaction trans, out bool sqlEmptyNotFilter, string tbPfx = null)
        {
            sqlEmptyNotFilter = true;
            if (FieldPermissionFilter == null)
            {
                return null;
            }
            var perFilter = CreateFieldPermissionFilter(methodName, conn, trans, tbPfx);
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

        #endregion
    }
}
