﻿using Hzdtf.Persistence.Dapper;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Hzdtf.MySql
{
    /// <summary>
    /// MySql Dapper基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    /// <typeparam name="ModelT">模型类型</typeparam>
    public abstract partial class MySqlDapperBase<IdT, ModelT> : CommonUseSqlDapperBase<IdT, ModelT>
        where ModelT : SimpleInfo<IdT>
    {
        #region 属性与字段

        /// <summary>
        /// 转义符前辍
        /// </summary>
        protected override string PfxEscapeChar { get => "`"; }

        /// <summary>
        /// 转义符后辍
        /// </summary>
        protected override string SufxEscapeChar { get => "`"; }

        #endregion

        #region 重写父类的方法

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>数据库连接</returns>
        public override IDbConnection CreateDbConnection(string connectionString) => new MySqlConnection(connectionString);

        /// <summary>
        /// 获取部分的分页SQL语句
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns>部分的分页SQL语句</returns>
        protected override string GetPartPageSql(int pageIndex, int pageSize)
        {
            int[] page = PagingUtil.PageStartEnd(pageIndex, pageSize);
            return $"LIMIT {page[0]},{pageSize}";
        }

        /// <summary>
        /// 获取最后插入ID SQL语句
        /// </summary>
        /// <returns>最后插入ID SQL语句</returns>
        protected override string GetLastInsertIdSql() => "SELECT LAST_INSERT_ID()";

        /// <summary>
        /// 匹配条件SQL
        /// </summary>
        /// <returns>不匹配条件SQL</returns>
        protected override string EqualWhereSql() => " (true) ";

        /// <summary>
        /// 不匹配条件SQL
        /// </summary>
        /// <returns>不匹配条件SQL</returns>
        protected override string NoEqualWhereSql() => " (false) ";

        /// <summary>
        /// 判断异常是否主键重复
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>异常是否主键重复</returns>
        protected override bool IsExceptionPkRepeat(Exception ex)
        {
            return IsCommonExceptionPkRepeat(ex);
        }

        /// <summary>
        /// 严格判断异常是否主键重复
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>异常是否主键重复</returns>
        public override bool StrictnessIsExceptionPkRepeat(Exception ex)
        {
            return IsCommonExceptionPkRepeat(ex);
        }

        #endregion

        #region 受保护方法

        /// <summary>
        /// 判断异常是否主键重复
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>异常是否主键重复</returns>
        protected virtual bool IsCommonExceptionPkRepeat(Exception ex)
        {
            MySqlException sqlEx = null;
            if (ex is MySqlException)
            {
                sqlEx = ex as MySqlException;
            }
            else
            {
                var innerEx = ex.GetLastInnerException();
                if (innerEx is MySqlException)
                {
                    sqlEx = innerEx as MySqlException;
                }
            }
            if (sqlEx == null)
            {
                return false;
            }

            var result = sqlEx.Number == 1062;
            if (result)
            {
                return OtherIsPkRepeat(sqlEx);
            }

            return result;
        }

        /// <summary>
        /// 其他判断主键重复，此方法目的是为了异常可能包含其他非主键重复的
        /// 如果子类没重写，默认为是
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>其他判断主键重复</returns>
        protected virtual bool OtherIsPkRepeat(MySqlException ex) => true;

        #endregion
    }
}
