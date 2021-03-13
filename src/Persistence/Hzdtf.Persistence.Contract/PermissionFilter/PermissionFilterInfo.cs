using Hzdtf.Utility.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Persistence.Contract.PermissionFilter
{
    /// <summary>
    /// 权限筛选信息
    /// @ 黄振东
    /// </summary>
    public class PermissionFilterInfo
    {
        /// <summary>
        /// 通用数据
        /// </summary>
        public CommonUseData ComData
        {
            get;
            set;
        }

        /// <summary>
        /// SQL
        /// </summary>
        public string Sql
        {
            get;
            set;
        }

        /// <summary>
        /// SQL为空则不过滤，默认为是
        /// </summary>
        public bool SqlEmptyNotFilter
        {
            get;
            set;
        } = true;

        /// <summary>
        /// 表前辍
        /// </summary>
        public string TablePfx
        {
            get;
            set;
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string Table
        {
            get;
            set;
        }

        /// <summary>
        /// 持久化类名
        /// </summary>
        public string PersistenceClassName
        {
            get;
            set;
        }

        /// <summary>
        /// 持久化方法名
        /// </summary>
        public string PersistenceMethodName
        {
            get;
            set;
        }

        /// <summary>
        /// 数据库连接
        /// </summary>
        public IDbConnection DbConnection
        {
            get;
            set;
        }

        /// <summary>
        /// 数据库事务
        /// </summary>
        public IDbTransaction DbTransaction
        {
            get;
            set;
        }
    }
}
