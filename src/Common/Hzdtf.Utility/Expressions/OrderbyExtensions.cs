using Hzdtf.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Hzdtf.Utility.Utils;

namespace System.Linq
{
    /// <summary>
    /// 排序扩展类
    /// @ 黄振东
    /// </summary>
    public static class OrderbyExtensions
    {
        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="query">查询表达式</param>
        /// <param name="fieldMapSortTypes">字段映射排序类型字典</param>
        /// <returns>查询表达式</returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, IDictionary<string, string> fieldMapSortTypes)
        {
            if (fieldMapSortTypes.IsNullOrCount0())
            {
                return query;
            }

            var dic = new Dictionary<string, SortType>(fieldMapSortTypes.Count);
            foreach (var item in fieldMapSortTypes)
            {
                dic.Add(item.Key, GetSortType(item.Value));
            }

            return query.OrderBy(dic);
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="query">查询表达式</param>
        /// <param name="fieldMapSortTypes">字段映射排序类型字典</param>
        /// <returns>查询表达式</returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, IDictionary<string, SortType> fieldMapSortTypes)
        {
            if (fieldMapSortTypes.IsNullOrCount0())
            {
                return query;
            }

            var i = 0;
            foreach (var item in fieldMapSortTypes)
            {
                if (i == 0)
                {
                    query = query.OrderBy(item.Key, item.Value);
                }
                else
                {
                    query = query.ThenOrderBy(item.Key, item.Value);
                }
                i++;
            }

            return query;
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="query">查询表达式</param>
        /// <param name="field">字段</param>
        /// <param name="orderBy">排序类型</param>
        /// <returns>查询表达式</returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string field, string orderBy = "asc")
        {
            return query.OrderBy(field, GetSortType(orderBy));
        }

        /// <summary>
        /// 第二排序
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="query">查询表达式</param>
        /// <param name="field">字段</param>
        /// <param name="orderBy">排序类型</param>
        /// <returns>查询表达式</returns>
        public static IQueryable<T> ThenOrderBy<T>(this IQueryable<T> query, string field, string orderBy = "asc")
        {
            return query.ThenOrderBy(field, GetSortType(orderBy));
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="query">查询表达式</param>
        /// <param name="field">字段</param>
        /// <param name="sortType">排序类型</param>
        /// <returns>查询表达式</returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string field, SortType sortType = SortType.ASC)
        {
            var orderMethod = sortType == SortType.ASC ? "OrderBy" : "OrderByDescending";
            return query.ExecOrderBy(field, orderMethod);
        }

        /// <summary>
        /// 第二排序
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="query">查询表达式</param>
        /// <param name="field">字段</param>
        /// <param name="sortType">排序类型</param>
        /// <returns>查询表达式</returns>
        public static IQueryable<T> ThenOrderBy<T>(this IQueryable<T> query, string field, SortType sortType = SortType.ASC)
        {
            var orderMethod = sortType == SortType.ASC ? "ThenBy" : "ThenByDescending";
            return query.ExecOrderBy(field, orderMethod);
        }

        /// <summary>
        /// 第二排序
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="query">查询表达式</param>
        /// <param name="field">字段</param>
        /// <param name="orderMethod">排序方法</param>
        /// <returns>查询表达式</returns>
        private static IQueryable<T> ExecOrderBy<T>(this IQueryable<T> query, string field, string orderMethod)
        {
            if (string.IsNullOrEmpty(field))
            {
                return query;
            }

            ParameterExpression p = Expression.Parameter(typeof(T));
            Expression key = Expression.Property(p, field);

            var propInfo = GetPropertyInfo(typeof(T), field);
            var expr = GetOrderExpression(typeof(T), propInfo);

            var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == orderMethod && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
            return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, expr });
        }

        /// <summary>
        /// 获取反射
        /// </summary>
        /// <param name="objType">对象类型</param>
        /// <param name="name">名称</param>
        /// <returns>属性信息</returns>
        private static PropertyInfo GetPropertyInfo(Type objType, string name)
        {
            var properties = objType.GetProperties();
            var matchedProperty = properties.FirstOrDefault(p => string.Compare(p.Name, name, true) == 0);
            if (matchedProperty == null)
            {
                throw new ArgumentException($"找不到属性:{name}");
            }

            return matchedProperty;
        }

        /// <summary>
        /// 获取生成表达式
        /// </summary>
        /// <param name="objType">对象类型</param>
        /// <param name="pi">属性信息</param>
        /// <returns>表达式</returns>
        private static LambdaExpression GetOrderExpression(Type objType, PropertyInfo pi)
        {
            var paramExpr = Expression.Parameter(objType);
            var propAccess = Expression.PropertyOrField(paramExpr, pi.Name);
            var expr = Expression.Lambda(propAccess, paramExpr);

            return expr;
        }

        /// <summary>
        /// 获取排序类型
        /// </summary>
        /// <param name="orderBy">排序类型</param>
        /// <returns>排序类型</returns>
        private static SortType GetSortType(string orderBy)
        {
            var sortType = SortType.ASC;
            if (!string.IsNullOrWhiteSpace(orderBy) && orderBy.ToLower().StartsWith("desc"))
            {
                sortType = SortType.DESC;
            }

            return sortType;
        }
    }
}
