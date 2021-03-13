using Dapper;
using Hzdtf.Utility.Enums;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Hzdtf.Persistence.Dapper
{
    /// <summary>
    /// 通用Sql Dapper基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    /// <typeparam name="ModelT">模型类型</typeparam>
    public abstract partial class CommonUseSqlDapperBase<IdT, ModelT> : DapperPersistenceBase<IdT, ModelT> 
        where ModelT : SimpleInfo<IdT>
    {
        #region 属性与字段

        /// <summary>
        /// 转义符前辍
        /// </summary>
        protected abstract string PfxEscapeChar { get; }

        /// <summary>
        /// 转义符后辍
        /// </summary>
        protected abstract string SufxEscapeChar { get; }

        /// <summary>
        /// 带ID等于参数的条件SQL
        /// </summary>
        protected string WHERE_ID_EQUAL_PARAM_SQL
        {
            get => $"WHERE {PfxEscapeChar}Id{SufxEscapeChar}=@Id";
        }

        #endregion

        #region 重写父类的方法

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
        protected override string SelectSql(IdT id, string dataPermissionSql, string fieldPermissionSql, string[] propertyNames = null, CommonUseData comData = null)
        {
            if (!string.IsNullOrWhiteSpace(dataPermissionSql))
            {
                dataPermissionSql = $" AND ({dataPermissionSql})";
            }

            string basicSelectSql = null;
            if (!string.IsNullOrWhiteSpace(fieldPermissionSql) && propertyNames.IsNullOrLength0())
            {
                basicSelectSql = $"SELECT {fieldPermissionSql} FROM {PfxEscapeChar}{Table}{SufxEscapeChar} {Table}";
            }
            else
            {
                basicSelectSql = BasicSelectSql(propertyNames: propertyNames, comData: comData);
            }

            return $"{basicSelectSql} {WHERE_ID_EQUAL_PARAM_SQL} {dataPermissionSql} {GetTenantIdFilterSql(isBeforeAppAnd: true, comData: comData)}";
        }

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
        protected override string SelectSql(IdT[] ids, string dataPermissionSql, string fieldPermissionSql, out DynamicParameters parameters, string[] propertyNames = null, CommonUseData comData = null)
        {
            if (!string.IsNullOrWhiteSpace(dataPermissionSql))
            {
                dataPermissionSql = $" AND ({dataPermissionSql})";
            }

            string basicSelectSql = null;
            if (!string.IsNullOrWhiteSpace(fieldPermissionSql) && propertyNames.IsNullOrLength0())
            {
                basicSelectSql = $"SELECT {fieldPermissionSql} FROM {PfxEscapeChar}{Table}{SufxEscapeChar} {Table}";
            }
            else
            {
                basicSelectSql = BasicSelectSql(propertyNames: propertyNames, comData: comData);
            }

            return $"{basicSelectSql} WHERE {GetWhereIdsSql(ids, out parameters, comData: comData)} {dataPermissionSql} {GetTenantIdFilterSql(isBeforeAppAnd: true, comData: comData)}";
        }

        /// <summary>
        /// 根据ID统计模型数SQL语句
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="dataPermissionSql">数据权限SQL</param>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected override string CountSql(IdT id, string dataPermissionSql, CommonUseData comData = null)
        {
            if (!string.IsNullOrWhiteSpace(dataPermissionSql))
            {
                dataPermissionSql = $" AND ({dataPermissionSql})";
            }

            return $"{BasicCountSql(comData: comData)} {WHERE_ID_EQUAL_PARAM_SQL} {dataPermissionSql} {GetTenantIdFilterSql(isBeforeAppAnd: true, comData: comData)}";
        }

        /// <summary>
        /// 统计模型数SQL语句
        /// </summary>
        /// <param name="pfx">前辍</param>
        /// <param name="dataPermissionSql">数据权限SQL</param>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected override string CountSql(string pfx = null, string dataPermissionSql = null, CommonUseData comData = null)
        {
            var whereSql = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(dataPermissionSql))
            {
                whereSql.AppendFormat(" WHERE ({0})", dataPermissionSql);
            }

            string tbAlias = string.IsNullOrWhiteSpace(pfx) ? null : pfx.Replace(".", null);
            bool isAppWhere = false, isAppAnd = true;
            if (whereSql.Length == 0)
            {
                isAppWhere = true;
                isAppAnd = false;
            }
            whereSql.Append(GetTenantIdFilterSql(isBeforeAppWhere: isAppWhere, isBeforeAppAnd: isAppAnd, pfx: tbAlias, comData: comData));

            return $"{BasicCountSql(pfx, comData: comData)} {whereSql.ToString()} {GetTenantIdFilterSql(isBeforeAppWhere: true, pfx: tbAlias, comData: comData)}";
        }

        /// <summary>
        /// 基本统计模型数SQL语句
        /// </summary>
        /// <param name="pfx">前辍</param>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected string BasicCountSql(string pfx = null, CommonUseData comData = null)
        {
            string tbAlias = string.IsNullOrWhiteSpace(pfx) ? null : pfx.Replace(".", null);

            return $"SELECT COUNT(*) FROM {PfxEscapeChar}{Table}{SufxEscapeChar} {tbAlias}";
        }

        /// <summary>
        /// 查询模型列表SQL语句
        /// </summary>
        /// <param name="pfx">前辍</param>
        /// <param name="appendFieldSqls">追加字段SQL，包含前面的,</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="dataPermissionSql">数据权限SQL</param>
        /// <param name="fieldPermissionSql">字段权限SQL</param>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected override string SelectSql(string pfx = null, string appendFieldSqls = null, string[] propertyNames = null, string dataPermissionSql = null, string fieldPermissionSql = null, CommonUseData comData = null)
        {
            string tbAlias = null;
            if (string.IsNullOrWhiteSpace(pfx))
            {
                pfx = $"{PfxEscapeChar}{Table}{SufxEscapeChar}.";
            }
            else
            {
                tbAlias = pfx.Replace(".", null);
            }

            var whereSql = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(dataPermissionSql))
            {
                whereSql.AppendFormat(" WHERE ({0})", dataPermissionSql);
            }
            bool isAppWhere = false, isAppAnd = true;
            if (whereSql.Length == 0)
            {
                isAppWhere = true;
                isAppAnd = false;
            }
            whereSql.Append(GetTenantIdFilterSql(isBeforeAppWhere: isAppWhere, isBeforeAppAnd: isAppAnd, pfx: tbAlias, comData: comData));

            string basicSelectSql = null;
            if (!string.IsNullOrWhiteSpace(fieldPermissionSql) && propertyNames.IsNullOrLength0())
            {
                basicSelectSql = $"SELECT {fieldPermissionSql} FROM {PfxEscapeChar}{Table}{SufxEscapeChar} {Table}";
            }
            else
            {
                basicSelectSql = BasicSelectSql(pfx, appendFieldSqls, propertyNames, comData: comData);
            }

            return $"{basicSelectSql} {whereSql.ToString()}";
        }

        /// <summary>
        /// 基本查询SQL
        /// </summary>
        /// <param name="pfx">前辍</param>
        /// <param name="appendFieldSqls">追加字段SQL，包含前面的,</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected string BasicSelectSql(string pfx = null, string appendFieldSqls = null, string[] propertyNames = null, CommonUseData comData = null)
        {
            string tbAlias = null;
            if (string.IsNullOrWhiteSpace(pfx))
            {
                pfx = $"{PfxEscapeChar}{Table}{SufxEscapeChar}.";
            }
            else
            {
                tbAlias = pfx.Replace(".", null);
            }

            return $"SELECT {JoinSelectPropMapFields(propertyNames, pfx: pfx)}{appendFieldSqls} FROM {PfxEscapeChar}{Table}{SufxEscapeChar} {tbAlias}";
        }

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
        protected override string SelectPageSql(int pageIndex, int pageSize, string dataPermissionSql, string fieldPermissionSql, out DynamicParameters parameters, FilterInfo filter = null, string[] propertyNames = null, CommonUseData comData = null)
        {
            StringBuilder whereSql = MergeWhereSql(filter, out parameters, comData: comData);
            if (!string.IsNullOrWhiteSpace(dataPermissionSql))
            {
                if (whereSql.Length == 0)
                {
                    whereSql.AppendFormat(" WHERE ({0})", dataPermissionSql);
                }
                else
                {
                    whereSql.AppendFormat(" AND ({0})", dataPermissionSql);
                }
            }
            string sortSql = GetSelectPageSortSql(filter, GetSelectSortNamePfx(filter, comData: comData), comData);
            if (string.IsNullOrWhiteSpace(sortSql))
            {
                sortSql = DefaultPageSortSql();
            }

            string basicSelectSql = null;
            if (!string.IsNullOrWhiteSpace(fieldPermissionSql) && propertyNames.IsNullOrLength0())
            {
                basicSelectSql = $"SELECT {fieldPermissionSql} FROM {PfxEscapeChar}{Table}{SufxEscapeChar} {Table}";
            }
            else
            {
                basicSelectSql = BasicSelectSql(appendFieldSqls: AppendSelectPageFieldsSql(comData: comData), propertyNames: propertyNames, comData: comData);
            }

            return $"{basicSelectSql} " +
                $"{GetSelectPageJoinSql(parameters, filter, comData)} {whereSql.ToString()}  {GetTenantIdFilterSql(isBeforeAppAnd: true, comData: comData)} {sortSql} {GetPartPageSql(pageIndex, pageSize)}";
        }

        /// <summary>
        /// 组合条件SQL
        /// </summary>
        /// <param name="filter">筛选</param>
        /// <param name="parameters">参数</param>
        /// <param name="comData">通用数据</param>
        /// <returns>条件SQL</returns>
        protected virtual StringBuilder MergeWhereSql(FilterInfo filter, out DynamicParameters parameters, CommonUseData comData = null)
        {
            StringBuilder whereSql = CreateWhereSql();
            parameters = new DynamicParameters();
            if (filter == null)
            {
                return whereSql;
            }

            AppendCreateTimeSql(whereSql, filter, parameters, comData: comData);
            AppendKeywordSql(whereSql, filter as KeywordFilterInfo, comData: comData);
            AppendSelectPageWhereSql(whereSql, parameters, filter, comData: comData);

            return whereSql;
        }

        /// <summary>
        /// 追加创建时间SQL
        /// </summary>
        /// <param name="whereSql">条件SQL</param>
        /// <param name="filter">筛选</param>
        /// <param name="parameters">参数</param>
        /// <param name="comData">通用数据</param>
        protected virtual void AppendCreateTimeSql(StringBuilder whereSql, FilterInfo filter, DynamicParameters parameters, CommonUseData comData = null)
        {
            if (filter == null)
            {
                return;
            }

            string createTimeField = GetFieldByProp("CreateTime");
            if (filter.StartCreateTime != null)
            {
                parameters.Add("@StartCreateTime", filter.StartCreateTime);

                whereSql.AppendFormat(" AND {2}{0}{3}.{1}>=@StartCreateTime", Table, createTimeField, PfxEscapeChar, SufxEscapeChar);
            }
            if (filter.EndCreateTime != null)
            {
                parameters.Add("@EndCreateTime", filter.EndCreateTime.ToLessThanDate());

                whereSql.AppendFormat(" AND {2}{0}{3}.{1}<@EndCreateTime", Table, createTimeField, PfxEscapeChar, SufxEscapeChar);
            }
        }

        /// <summary>
        /// 追加按关键字查询的SQL
        /// </summary>
        /// <param name="whereSql">条件SQL</param>
        /// <param name="keywordFilter">关键字筛选</param>
        /// <param name="comData">通用数据</param>
        protected virtual void AppendKeywordSql(StringBuilder whereSql, KeywordFilterInfo keywordFilter, CommonUseData comData = null)
        {
            if (keywordFilter == null || string.IsNullOrWhiteSpace(keywordFilter.Keyword))
            {
                return;
            }

            string[] keywordFields = GetPageKeywordFields();
            if (!keywordFields.IsNullOrLength0())
            {
                whereSql.Append(" AND (");
                foreach (var f in keywordFields)
                {
                    string pfx = f.Contains(".") ? null : Table + ".";
                    whereSql.AppendFormat("{0}{1} LIKE '%{2}%' OR ", pfx, f, keywordFilter.Keyword.FillSqlValue());
                }
                whereSql.Remove(whereSql.Length - 4, 4);
                whereSql.Append(")");
            }
        }

        /// <summary>
        /// 获取分页按关键字查询的字段集合
        /// </summary>
        /// <returns>分页按关键字查询的字段集合</returns>
        protected virtual string[] GetPageKeywordFields() => null;

        /// <summary>
        /// 根据筛选信息统计模型数SQL语句
        /// </summary>
        /// <param name="filter">筛选信息</param>
        /// <param name="dataPermissionSql">数据权限SQL</param>
        /// <param name="parameters">参数</param>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected override string CountByFilterSql(FilterInfo filter, string dataPermissionSql, out DynamicParameters parameters, CommonUseData comData = null)
        {
            StringBuilder whereSql = MergeWhereSql(filter, out parameters, comData: comData);
            if (!string.IsNullOrWhiteSpace(dataPermissionSql))
            {
                if (whereSql.Length == 0)
                {
                    whereSql.AppendFormat(" WHERE ({0})", dataPermissionSql);
                }
                else
                {
                    whereSql.AppendFormat(" AND ({0})", dataPermissionSql);
                }
            }

            return $"{BasicCountSql(comData: comData)} {GetSelectPageJoinSql(parameters, filter, comData: comData)} {whereSql.ToString()} {GetTenantIdFilterSql(isBeforeAppAnd: true, comData: comData)}";
        }

        /// <summary>
        /// 获取租户ID筛选SQL
        /// </summary>
        /// <param name="isBeforeAppWhere">是否前面追加WHERE</param>
        /// <param name="isBeforeAppAnd">是否前面追加AND</param>
        /// <param name="pfx">前辍</param>
        /// <param name="comData">通用数据</param>
        /// <returns>租户ID筛选SQL</returns>
        protected virtual string GetTenantIdFilterSql(bool isBeforeAppWhere = false, bool isBeforeAppAnd = false, string pfx = null, CommonUseData comData = null)
        {
            IdT tenantId;
            if (IsExistsTenantId(out tenantId))
            {
                var tenantIdField = GetFieldByProp("TenantId");
                pfx = string.IsNullOrWhiteSpace(pfx) ? null : pfx + ".";
                var sql = $"({pfx}{PfxEscapeChar}{tenantIdField}{SufxEscapeChar}={Identity.GetValueSql(tenantId)})";
                if (isBeforeAppWhere)
                {
                    return $" WHERE {sql}";
                }
                else if (isBeforeAppAnd)
                {
                    return $" AND {sql}";
                }

                return sql;
            }

            return null;
        }

        /// <summary>
        /// 默认分页排序SQL，默认是按修改时间降序、创建时间降序、ID升序。如果要改变，请在子类重写
        /// </summary>
        /// <param name="pfx">前辍</param>
        /// <returns>默认排序SQL</returns>
        public virtual string DefaultPageSortSql(string pfx = null)
        {
            if (string.IsNullOrWhiteSpace(pfx))
            {
                pfx = Table;
            }
            else
            {
                pfx = pfx.Replace(".", null);
            }

            return $" ORDER BY {pfx}.{PfxEscapeChar}{GetFieldByProp("ModifyTime")}{SufxEscapeChar} DESC, {pfx}.{PfxEscapeChar}{GetFieldByProp("CreateTime")}{SufxEscapeChar} DESC, {pfx}.{PfxEscapeChar}{GetFieldByProp("Id")}{SufxEscapeChar}";
        }

        /// <summary>
        /// 根据ID和大于修改时间查询修改信息（多用于乐观锁的判断，以修改时间为判断）
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="comData">通用数据</param>
        /// <returns>只有修改信息的SQL语句</returns>
        protected override string SelectModifyInfoByIdAndGeModifyTimeSql(ModelT model, CommonUseData comData = null)
        {
            var modifyTimeField = $"{PfxEscapeChar}{ GetFieldByProp("ModifyTime") }{SufxEscapeChar}";
            var idField = $"{PfxEscapeChar}{ GetFieldByProp("Id")}{SufxEscapeChar}";
            return $"SELECT {idField} Id,{PfxEscapeChar}{GetFieldByProp("ModifierId")}{SufxEscapeChar} ModifierId,{PfxEscapeChar}{GetFieldByProp("Modifier")}{SufxEscapeChar} Modifier,{modifyTimeField} ModifyTime"
                + $" FROM {PfxEscapeChar}{Table}{SufxEscapeChar} WHERE {idField}=@Id AND {modifyTimeField}>@ModifyTime {GetTenantIdFilterSql(isBeforeAppAnd: true, comData: comData)}";
        }

        /// <summary>
        /// 根据ID和大于修改时间查询修改信息列表（多用于乐观锁的判断，以修改时间为判断）
        /// </summary>
        /// <param name="models">模型数组</param>
        /// <param name="parameters">参数</param>
        /// <param name="comData">通用数据</param>
        /// <returns>只有修改信息的SQL语句</returns>
        protected override string SelectModifyInfosByIdAndGeModifyTimeSql(ModelT[] models, out DynamicParameters parameters, CommonUseData comData = null)
        {
            parameters = new DynamicParameters();
            var modifyTimeField = $"{PfxEscapeChar}{ GetFieldByProp("ModifyTime") }{SufxEscapeChar}";
            var idField = $"{PfxEscapeChar}{ GetFieldByProp("Id")}{SufxEscapeChar}";

            var whereSql = new StringBuilder(" WHERE (");
            for (var i = 0; i < models.Length; i++)
            {
                var person = models[i] as PersonTimeInfo<IdT>;

                var paraIdName = $"@Id{i}";
                var paraModifyTimeName = $"@ModifyTime{i}";
                parameters.Add(paraIdName, person.Id);
                parameters.Add(paraModifyTimeName, person.ModifyTime);

                whereSql.AppendFormat(" ({0}={1} AND {2}>{3}) OR", idField, paraIdName, modifyTimeField, paraModifyTimeName);
            }
            whereSql.Remove(whereSql.Length - 3, 3);
            whereSql.Append(")");

            return $"SELECT {idField} Id,{PfxEscapeChar}{GetFieldByProp("ModifierId")}{SufxEscapeChar} ModifierId,{PfxEscapeChar}{GetFieldByProp("Modifier")}{SufxEscapeChar} Modifier,{modifyTimeField} ModifyTime"
                + $" FROM {PfxEscapeChar}{Table}{SufxEscapeChar} {whereSql.ToString()} {GetTenantIdFilterSql(isBeforeAppAnd: true, comData: comData)}";
        }

        #endregion

        #region 写入方法

        /// <summary>
        /// 插入模型SQL语句
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="isGetAutoId">是否获取自增ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected override string InsertSql(ModelT model, bool isGetAutoId = false, CommonUseData comData = null)
        {
            string[] partSql = CombineInsertSqlByFieldNames(WrapInsertFieldNames(model.Id));
            string sql = $"INSERT INTO {PfxEscapeChar}{Table}{SufxEscapeChar}({partSql[0]}) VALUES({partSql[1]})";

            return isGetAutoId ? $"{sql};{GetLastInsertIdSql()}" : sql;
        }

        /// <summary>
        /// 插入模型列表SQL语句
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="para">参数集合</param>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected override string InsertSql(IList<ModelT> models, out DynamicParameters para, CommonUseData comData = null)
        {
            string[] partSql = CombineBatchInsertSqlByFieldNames(WrapInsertFieldNames(models[0].Id), models, out para);
            return $"INSERT INTO {PfxEscapeChar}{Table}{SufxEscapeChar}({partSql[0]}) VALUES{partSql[1]}";
        }

        /// <summary>
        /// 根据ID更新模型SQL语句
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected override string UpdateByIdSql(ModelT model, string[] propertyNames = null, CommonUseData comData = null) => $"UPDATE {PfxEscapeChar}{Table}{SufxEscapeChar} SET {GetUpdateFieldsSql(propertyNames)} {WHERE_ID_EQUAL_PARAM_SQL} {GetTenantIdFilterSql(isBeforeAppAnd: true, comData: comData)}";

        /// <summary>
        /// 根据ID删除模型SQL语句
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected override string DeleteByIdSql(IdT id, CommonUseData comData = null) => $"{BasicDeleteSql(comData: comData)} {WHERE_ID_EQUAL_PARAM_SQL} {GetTenantIdFilterSql(isBeforeAppAnd: true, comData: comData)}";

        /// <summary>
        /// 根据ID数组删除模型SQL语句
        /// </summary>
        /// <param name="ids">ID数组</param>
        /// <param name="parameters">参数集合</param>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected override string DeleteByIdsSql(IdT[] ids, out DynamicParameters parameters, CommonUseData comData = null) => $"{BasicDeleteSql(comData)} WHERE {GetWhereIdsSql(ids, out parameters, comData: comData)} {GetTenantIdFilterSql(isBeforeAppAnd: true, comData: comData)}";

        /// <summary>
        /// 删除所有模型SQL语句
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected override string DeleteSql(CommonUseData comData = null) => $"DELETE FROM {PfxEscapeChar}{Table}{SufxEscapeChar} {GetTenantIdFilterSql(isBeforeAppWhere: true, comData: comData)}";

        /// <summary>
        /// 基本删除所有模型SQL语句
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected string BasicDeleteSql(CommonUseData comData = null) => $"DELETE FROM {PfxEscapeChar}{Table}{SufxEscapeChar}";

        /// <summary>
        /// 模型是否包含租户ID
        /// </summary>
        /// <returns>模型是否包含租户ID</returns>
        protected override bool ModelContainerTenantId() => !string.IsNullOrWhiteSpace(GetFieldByProp("TenantId"));

        #endregion

        /// <summary>
        /// 根据表名删除所有模型SQL语句
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected override string DeleteByTableSql(string table, CommonUseData comData = null) => $"DELETE FROM {PfxEscapeChar}{table}{SufxEscapeChar} WHERE {EqualWhereSql()} {GetTenantIdFilterSql(isBeforeAppAnd: true, comData: comData)}";

        /// <summary>
        /// 基本根据表名删除所有模型SQL语句
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected string BasicDeleteByTableSql(string table, CommonUseData comData = null) => $"DELETE FROM {PfxEscapeChar}{table}{SufxEscapeChar}";

        /// <summary>
        /// 根据表名、外键字段和外键值删除模型SQL语句
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="foreignKeyName">外键名称</param>
        /// <param name="foreignKeyValues">外键值集合</param>
        /// <param name="parameters">参数</param>
        /// <param name="comData">通用数据</param>
        /// <returns>SQL语句</returns>
        protected override string DeleteByTableAndForignKeySql(string table, string foreignKeyName, IdT[] foreignKeyValues, out DynamicParameters parameters, CommonUseData comData = null)
        {
            parameters = new DynamicParameters();
            StringBuilder whereSql = new StringBuilder();
            for (var i = 0; i < foreignKeyValues.Length; i++)
            {
                string p = $"@{foreignKeyName}{i}";
                whereSql.AppendFormat("{0},", p);
                parameters.Add(p, foreignKeyValues[i]);
            }
            whereSql.Remove(whereSql.Length - 1, 1);

            return $"{BasicDeleteByTableSql(table, comData: comData)} WHERE {PfxEscapeChar}{foreignKeyName}{SufxEscapeChar} IN({whereSql.ToString()}) {GetTenantIdFilterSql(isBeforeAppAnd: true, comData: comData)}";
        }

        #endregion

        #region 需要子类重写的方法

        /// <summary>
        /// 插入字段名集合
        /// </summary>
        /// <returns>插入字段名集合</returns>
        protected abstract string[] InsertFieldNames();

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        /// <returns>更新字段名称集合</returns>
        protected abstract string[] UpdateFieldNames();

        /// <summary>
        /// 根据字段名获取模型的值
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        protected abstract object GetValueByFieldName(ModelT model, string field);

        /// <summary>
        /// 获取部分的分页SQL语句
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns>部分的分页SQL语句</returns>
        protected abstract string GetPartPageSql(int pageIndex, int pageSize);

        /// <summary>
        /// 获取最后插入ID SQL语句
        /// </summary>
        /// <returns>最后插入ID SQL语句</returns>
        protected abstract string GetLastInsertIdSql();

        #endregion

        #region 受保护的方法

        /// <summary>
        /// 根据ID数组获取ID条件SQL语句，不包含where
        /// </summary>
        /// <param name="ids">ID数组</param>
        /// <param name="parameters">参数</param>
        /// <param name="prefixTable">表前辍</param>
        /// <param name="idField">ID字段</param>
        /// <param name="comData">通用数据</param>
        /// <returns>ID条件SQL语句</returns>
        protected string GetWhereIdsSql(IdT[] ids, out DynamicParameters parameters, string prefixTable = null, string idField = "Id", CommonUseData comData = null) => GetWhereTypesSql<IdT>(ids, out parameters, idField, prefixTable, comData);

        /// <summary>
        /// 根据值数组获取条件SQL语句，不包含where
        /// </summary>
        /// <param name="values">值数组</param>
        /// <param name="parameters">参数</param>
        /// <param name="field">字段</param>
        /// <param name="prefixTable">表前辍</param>
        /// <param name="comData">通用数据</param>
        /// <returns>ID条件SQL语句</returns>
        protected string GetWhereTypesSql<T>(T[] values, out DynamicParameters parameters, string field, string prefixTable = null, CommonUseData comData = null)
        {
            parameters = new DynamicParameters(values.Length);
            StringBuilder whereSql = new StringBuilder($"{prefixTable}{PfxEscapeChar}{field}{SufxEscapeChar} IN(");
            for (int i = 0; i < values.Length; i++)
            {
                string paraName = $"@{field}{i}";
                whereSql.AppendFormat("{0},", paraName);
                parameters.Add(paraName, values[i]);
            }
            whereSql.Remove(whereSql.Length - 1, 1);
            whereSql.Append(")");

            return whereSql.ToString();
        }

        /// <summary>
        /// 根据字段名获取属性名
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="fieldMapProps">字段映射属性集合</param>
        /// <returns>属性名</returns>
        protected string GetPropByField(string field, string[] fieldMapProps = null)
        {
            if (fieldMapProps == null)
            {
                fieldMapProps = AllFieldMapProps();
            }
            foreach (string fp in fieldMapProps)
            {
                string[] temp = fp.Split(' ');
                if (field.Equals(temp[0]))
                {
                    return temp[1];
                }
            }

            return null;
        }

        /// <summary>
        /// 根据属性名获取字段名
        /// </summary>
        /// <param name="prop">属性</param>
        /// <param name="fieldMapProps">字段映射属性集合</param>
        /// <returns>属性名</returns>
        protected string GetFieldByProp(string prop, string[] fieldMapProps = null)
        {
            if (fieldMapProps == null)
            {
                fieldMapProps = AllFieldMapProps();
            }
            foreach (string fp in fieldMapProps)
            {
                string[] temp = fp.Split(' ');
                if (prop.Equals(temp[1]))
                {
                    return temp[0];
                }
            }

            return null;
        }

        /// <summary>
        /// 获取排序SQL语句
        /// </summary>
        /// <param name="sort">排序</param>
        /// <param name="prop">排序的属性名</param>
        /// <param name="pfx">前辍</param>
        /// <returns>排序SQL语句</returns>
        protected string GetSortSql(SortType sort, string prop, string pfx = null)
        {
            if (string.IsNullOrWhiteSpace(prop))
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(pfx))
            {
                pfx = Table + ".";
            }

            string field = GetFieldByProp(prop);
            if (string.IsNullOrWhiteSpace(field))
            {
                field = prop;
            }
            StringBuilder sql = new StringBuilder($"ORDER BY {pfx}{field}");
            if (sort == SortType.ASC)
            {
                sql.Append(" ASC");
            }
            else
            {
                sql.Append(" DESC");
            }

            return sql.ToString();
        }

        /// <summary>
        /// 根据字段名获取排序SQL语句
        /// </summary>
        /// <param name="sort">排序</param>
        /// <param name="field">排序的字段名</param>
        /// <param name="pfx">前辍</param>
        /// <returns>排序SQL语句</returns>
        protected string GetSortSqlByField(SortType sort, string field, string pfx = null)
        {
            if (string.IsNullOrWhiteSpace(field))
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(pfx))
            {
                pfx = Table + ".";
            }

            StringBuilder sql = new StringBuilder($"ORDER BY {pfx}{field}");
            if (sort == SortType.ASC)
            {
                sql.Append(" ASC");
            }
            else
            {
                sql.Append(" DESC");
            }

            return sql.ToString();
        }

        /// <summary>
        /// 连接查询的属性映射字段集合
        /// 带有,号
        /// </summary>
        /// <param name="props">属性名集合（如果为null则取全部）</param>
        /// <param name="pfx">前辍</param>
        /// <returns>连接后的查询的属性映射字段集合</returns>
        protected string JoinSelectPropMapFields(string[] props = null, string pfx = null)
        {
            StringBuilder result = new StringBuilder();
            if (props == null)
            {
                string[] strs = AllFieldMapProps();
                foreach (string s in strs)
                {
                    result.AppendFormat("{0}{1},", pfx, s);
                }
            }
            else
            {
                foreach (string p in props)
                {
                    result.AppendFormat("{0}{1} {2},", pfx, GetFieldByProp(p), p);
                }
            }
            if (result.Length > 0)
            {
                result.Remove(result.Length - 1, 1);
            }

            return result.ToString();
        }

        /// <summary>
        /// 获取修改信息SQL
        /// 前面带有,前辍
        /// </summary>
        /// <param name="model">模型</param>
        /// <returns>修改信息SQL</returns>
        protected string GetModifyInfoSql(ModelT model)
        {
            if (model is PersonTimeInfo<IdT>)
            {
                string[] modifyProps = new string[] { "ModifierId", "Modifier", "ModifyTime" };
                StringBuilder sql = new StringBuilder();
                foreach (var p in modifyProps)
                {
                    string pName = $"@{p}";
                    sql.AppendFormat(",{2}{0}{3}={1}", GetFieldByProp(p), pName, PfxEscapeChar, SufxEscapeChar);
                }

                return sql.ToString();
            }

            return null;
        }

        #endregion

        #region 虚方法

        /// <summary>
        /// 获取查询分页排序的SQL
        /// </summary>
        /// <param name="filter">筛选信息</param>
        /// <param name="pfx">前辍</param>
        /// <param name="comData">通用数据</param>
        /// <returns>分页排序的SQL</returns>
        protected virtual string GetSelectPageSortSql(FilterInfo filter, string pfx = null, CommonUseData comData = null)
        {
            if (filter == null || string.IsNullOrWhiteSpace(filter.SortName))
            {
                return null;
            }

            return GetSortSql(filter.Sort, ConvertSortName(filter.SortName), pfx);
        }

        /// <summary>
        /// 追加查询分页字段SQL
        /// </summary>
        /// <param name="comData">通用数据</param>
        protected virtual string AppendSelectPageFieldsSql(CommonUseData comData = null) => null;

        /// <summary>
        /// 追加查询分页条件SQL
        /// </summary>
        /// <param name="whereSql">where语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="filter">筛选</param>
        /// <param name="comData">通用数据</param>
        protected virtual void AppendSelectPageWhereSql(StringBuilder whereSql, DynamicParameters parameters, FilterInfo filter = null, CommonUseData comData = null) { }

        /// <summary>
        /// 获取查询分页连接SQL
        /// </summary>
        /// <param name="parameters">参数</param>
        /// <param name="filter">筛选</param>
        /// <param name="comData">通用数据</param>
        /// <returns>连接SQL语句</returns>
        protected virtual string GetSelectPageJoinSql(DynamicParameters parameters, FilterInfo filter = null, CommonUseData comData = null) => null;

        /// <summary>
        /// 转换排序名称
        /// </summary>
        /// <param name="sortName">排名名称</param>
        /// <returns>排序名称</returns>
        protected virtual string ConvertSortName(string sortName) => sortName.FristUpper();

        /// <summary>
        /// 包装插入字段名集合
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>插入字段名集合</returns>
        protected virtual string[] WrapInsertFieldNames(IdT id)
        {
            var fields = InsertFieldNames();
            if (PrimaryKeyIncr(id))
            {
                var idField = GetFieldByProp("Id", AllFieldMapProps());
                return fields.Remove(idField);
            }

            return fields;
        }

        /// <summary>
        /// 创建where语句
        /// </summary>
        /// <returns>where语句</returns>
        protected virtual StringBuilder CreateWhereSql() => new StringBuilder($" WHERE {EqualWhereSql()}");

        #endregion

        #region 私有方法

        /// <summary>
        /// 根据字段名称集合组合插入SQL语句
        /// </summary>
        /// <param name="fieldNames">字段名称集合</param>
        /// <returns>插入SQL语句</returns>
        private string[] CombineInsertSqlByFieldNames(string[] fieldNames)
        {
            StringBuilder fieldBuilder = new StringBuilder();
            StringBuilder valueBuilder = new StringBuilder();
            string[] fieldMapProps = AllFieldMapProps();
            foreach (string field in fieldNames)
            {
                fieldBuilder.AppendFormat("{1}{0}{2},", field, PfxEscapeChar, SufxEscapeChar);
                valueBuilder.AppendFormat("@{0},", GetPropByField(field));
            }

            fieldBuilder.Remove(fieldBuilder.Length - 1, 1);
            valueBuilder.Remove(valueBuilder.Length - 1, 1);

            return new string[] { fieldBuilder.ToString(), valueBuilder.ToString() };
        }

        /// <summary>
        /// 根据字段名称集合组合批量插入SQL语句
        /// </summary>
        /// <param name="fieldNames">字段名称集合</param>
        /// <param name="models">模型列表</param>
        /// <param name="para">参数</param>
        /// <returns>插入SQL语句</returns>
        private string[] CombineBatchInsertSqlByFieldNames(string[] fieldNames, IList<ModelT> models, out DynamicParameters para)
        {
            para = new DynamicParameters();
            StringBuilder fieldBuilder = new StringBuilder();
            StringBuilder[] valueBuilder = new StringBuilder[models.Count];
            for (int i = 0; i < valueBuilder.Length; i++)
            {
                valueBuilder[i] = new StringBuilder();
            }

            string[] fieldMapProps = AllFieldMapProps();
            foreach (string field in fieldNames)
            {
                fieldBuilder.AppendFormat("{1}{0}{2},", field, PfxEscapeChar, SufxEscapeChar);

                for (int i = 0; i < valueBuilder.Length; i++)
                {
                    string paraName = $"@{GetPropByField(field)}{i}";
                    para.Add(paraName, GetValueByFieldName(models[i], field));
                    valueBuilder[i].AppendFormat("{0},", paraName);
                }
            }
            fieldBuilder.Remove(fieldBuilder.Length - 1, 1);
            StringBuilder valResultSql = new StringBuilder();
            for (int i = 0; i < valueBuilder.Length; i++)
            {
                valueBuilder[i].Remove(valueBuilder[i].Length - 1, 1);
                valResultSql.AppendFormat("({0}),", valueBuilder[i].ToString());
            }
            valResultSql.Remove(valResultSql.Length - 1, 1);

            return new string[] { fieldBuilder.ToString(), valResultSql.ToString() };
        }

        /// <summary>
        /// 根据字段名称集合组合更新SQL语句
        /// </summary>
        /// <param name="fieldNames">字段名称集合</param>
        /// <returns>更新SQL语句</returns>
        private string CompareUpdateSqlByFieldNames(string[] fieldNames)
        {
            StringBuilder fieldValueBuilder = new StringBuilder();
            string[] fieldMapProps = AllFieldMapProps();
            foreach (string field in fieldNames)
            {
                fieldValueBuilder.AppendFormat("{2}{0}{3}=@{1},", field, GetPropByField(field), PfxEscapeChar, SufxEscapeChar);
            }
            fieldValueBuilder.Remove(fieldValueBuilder.Length - 1, 1);

            return fieldValueBuilder.ToString();
        }

        /// <summary>
        /// 获取更新字段SQL
        /// 如果传入的属性名称为null则获取子类的字段
        /// </summary>
        /// <param name="propertyNames">属性名称</param>
        /// <returns>更新字段SQL</returns>
        private string GetUpdateFieldsSql(string[] propertyNames = null)
        {
            string[] fields = null;
            if (propertyNames == null)
            {
                fields = UpdateFieldNames();
            }
            else
            {
                fields = new string[propertyNames.Length];
                for (var i = 0; i < propertyNames.Length; i++)
                {
                    fields[i] = GetFieldByProp(propertyNames[i]);
                }
            }

            return CompareUpdateSqlByFieldNames(fields);
        }

        #endregion
    }
}