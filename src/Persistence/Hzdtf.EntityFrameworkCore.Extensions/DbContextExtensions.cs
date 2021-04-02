using Hzdtf.Utility.Utils;
using System;
using System.Data;
using System.Data.Common;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// 数据库上下文扩展类
    /// @ 黄振东
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// 更新指定的属性实体数组
        /// </summary>
        /// <typeparam name="DbContextT">数据库上下文类型</typeparam>
        /// <param name="context">数据库上下文</param>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entitys">实体数组</param>
        /// <param name="updatePropNames">更新属性名称</param>
        public static void UpdateProps<DbContextT, T>(this DbContextT context, T[] entitys, params string[] updatePropNames) 
            where T : class
            where DbContextT : DbContext
        {
            if (entitys.IsNullOrLength0())
            {
                throw new ArgumentException("实体不能为空");
            }
            if (updatePropNames.IsNullOrLength0())
            {
                throw new ArgumentException("更新属性名称不能为空");
            }

            foreach (var t in entitys)
            {
                context.UpdateProps(t, updatePropNames);
            }
        }

        /// <summary>
        /// 更新指定的属性实体
        /// </summary>
        /// <typeparam name="DbContextT">数据库上下文类型</typeparam>
        /// <param name="context">数据库上下文</param>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="updatePropNames">更新属性名称</param>
        public static void UpdateProps<DbContextT, T>(this DbContextT context, T entity, params string[] updatePropNames)
            where T : class
            where DbContextT : DbContext
        {
            if (entity == null)
            {
                throw new ArgumentException("实体不能为空");
            }
            if (updatePropNames.IsNullOrLength0())
            {
                throw new ArgumentException("更新属性名称不能为空");
            }

            context.Set<T>().Attach(entity);
            foreach (var p in updatePropNames)
            {
                context.Entry<T>(entity).Property(p).IsModified = true;
            }
        }

        /// <summary>
        /// 执行SQL并返回第1行第1列的值
        /// </summary>
        /// <typeparam name="DbContextT">数据库上下文类型</typeparam>
        /// <param name="context">数据库上下文</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="isCloseConn">是否关闭连接</param>
        /// <param name="param">参数</param>
        /// <returns>第1行第1列的值</returns>
        public static object ExecuteScalar<DbContextT>(this DbContextT context, string sql, bool isCloseConn = true, params DbParameter[] param)
            where DbContextT : DbContext
        {
            return context.ExecCommand<DbContextT, object>(sql, cmd =>
            {
                return cmd.ExecuteScalar();
            }, isCloseConn, param);
        }

        /// <summary>
        /// 执行SQL并返回影响行数
        /// </summary>
        /// <typeparam name="DbContextT">数据库上下文类型</typeparam>
        /// <param name="context">数据库上下文</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="isCloseConn">是否关闭连接</param>
        /// <param name="param">参数</param>
        /// <returns>第1行第1列的值</returns>
        public static int ExecuteNonQuery<DbContextT>(this DbContextT context, string sql, bool isCloseConn = true, params DbParameter[] param)
            where DbContextT : DbContext
        {
            return context.ExecCommand<DbContextT, int>(sql, cmd =>
            {
                return cmd.ExecuteNonQuery();
            }, isCloseConn, param);
        }

        /// <summary>
        /// 执行SQL并返回影响行数
        /// </summary>
        /// <typeparam name="DbContextT">数据库上下文类型</typeparam>
        /// <param name="context">数据库上下文</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="callback">回调</param>
        /// <param name="isCloseConn">是否关闭连接</param>
        /// <param name="param">参数</param>
        public static void ExecuteReader<DbContextT>(this DbContextT context, string sql, Action<DbDataReader> callback, bool isCloseConn = true, params DbParameter[] param)
            where DbContextT : DbContext
        {
            context.ExecCommand<DbContextT, object>(sql, cmd =>
            {
                var reader = cmd.ExecuteReader();
                try
                {
                    callback(reader);

                    return null;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    reader.Close();
                }
            }, isCloseConn, param);
        }

        /// <summary>
        /// 执行SQL并返回第1行第1列的值
        /// </summary>
        /// <typeparam name="DbContextT"></typeparam>
        /// <typeparam name="ReturnT">返回类型</typeparam>
        /// <param name="context">数据库上下文</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="func">回调</param>
        /// <param name="isCloseConn">是否关闭连接</param>
        /// <param name="param">参数</param>
        /// <returns>第1行第1列的值</returns>
        private static ReturnT ExecCommand<DbContextT, ReturnT>(this DbContextT context, string sql, Func<DbCommand, ReturnT> func, bool isCloseConn = true, params DbParameter[] param)
            where DbContextT : DbContext
        {
            var conn = context.Database.GetDbConnection();
            DbCommand cmd = null;
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                if (!param.IsNullOrLength0())
                {
                    cmd.Parameters.AddRange(param);
                }

                return func(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (isCloseConn && conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }
    }
}
