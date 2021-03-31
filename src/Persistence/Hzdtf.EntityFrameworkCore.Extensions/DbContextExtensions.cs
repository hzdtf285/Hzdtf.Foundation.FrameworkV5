using Hzdtf.Utility.Utils;
using System;

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
    }
}
