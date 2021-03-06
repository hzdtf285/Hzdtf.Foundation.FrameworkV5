﻿using Hzdtf.Persistence.Dapper;
using Hzdtf.Utility.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Hzdtf.SqlServer
{
    /// <summary>
    /// SqlServer Dapper基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    /// <typeparam name="ModelT">模型类型</typeparam>
    public abstract partial class SqlServerDapperBase<IdT, ModelT> : CommonUseSqlDapperBase<IdT, ModelT>
        where ModelT : SimpleInfo<IdT>
    {
        #region 属性与字段

        /// <summary>
        /// 转义符前辍
        /// </summary>
        protected override string PfxEscapeChar { get => "["; }

        /// <summary>
        /// 转义符后辍
        /// </summary>
        protected override string SufxEscapeChar { get => "]"; }


        #endregion

        #region 重写父类的方法

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>数据库连接</returns>
        public override IDbConnection CreateDbConnection(string connectionString) => new SqlConnection(connectionString);

        /// <summary>
        /// 获取部分的分页SQL语句
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns>部分的分页SQL语句</returns>
        protected override string GetPartPageSql(int pageIndex, int pageSize)
        {
            var start = pageIndex > 0 ? pageIndex * pageSize : pageIndex;
            return $"OFFSET {start} rows fetch next {pageSize} rows only";
        }

        /// <summary>
        /// 获取最后插入ID SQL语句
        /// </summary>
        /// <returns>最后插入ID SQL语句</returns>
        protected override string GetLastInsertIdSql() => "SELECT SCOPE_IDENTITY()";

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

        #region 私有方法

        /// <summary>
        /// 判断异常是否主键重复
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>异常是否主键重复</returns>
        protected bool IsCommonExceptionPkRepeat(Exception ex)
        {
            SqlException sqlEx = null;
            if (ex is SqlException)
            {
                sqlEx = ex as SqlException;
            }
            else
            {
                var innerEx = ex.GetLastInnerException();
                if (innerEx is SqlException)
                {
                    sqlEx = innerEx as SqlException;
                }
            }
            if (sqlEx == null)
            {
                return false;
            }

            return sqlEx.Number == 2627;
        }

        #endregion
    }
}
