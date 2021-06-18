using Hzdtf.Persistence.Contract.Basic;
using Hzdtf.Persistence.Contract.Management;
using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Enums;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Page;
using System;
using System.Collections.Generic;
using System.Data;
using Hzdtf.Utility.Utils;
using Hzdtf.Utility.Model.Identitys;
using System.Linq;
using Hzdtf.Utility.Localization;
using Hzdtf.Persistence.Contract.PermissionFilter;
using System.Diagnostics;
using Hzdtf.Logger.Contract;

namespace Hzdtf.Persistence.Contract.Data
{
    /// <summary>
    /// 持久化基类
    /// 如果可实例化的子类包含有TenantId属性（字段），则默认代表中存在租户ID，本类对外提供所有的增/删/改/查操作都自动加上租户ID约束，同时取租户ID时，默认取当前用户的租房ID属性（则执行UserTool.GetCurrUser()方法）
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    /// <typeparam name="ModelT">模型类型</typeparam>
    public abstract partial class PersistenceBase<IdT, ModelT> : PersistenceConnectionBase, IPersistence<IdT, ModelT> 
        where ModelT : SimpleInfo<IdT>
    {
        #region 属性与字段

        /// <summary>
        /// ID
        /// </summary>
        public IIdentity<IdT> Identity
        {
            get;
            set;
        }

        /// <summary>
        /// 本地化
        /// </summary>
        public ILocalization Localize
        {
            get;
            set;
        }

        /// <summary>
        /// 数据权限过滤
        /// </summary>
        public IDataPermissionFilter DataPermissionFilter
        {
            get;
            set;
        }

        /// <summary>
        /// 字段权限过滤
        /// </summary>
        public IFieldPermissionFilter FieldPermissionFilter
        {
            get;
            set;
        }

        /// <summary>
        /// SQL日志等级，默认读取配置文件：Logging:LogLevel:SqlLog
        /// 如果配置文件没有，则默认trace
        /// </summary>
        protected LogLevelEnum SqlLogLevel
        {
            get
            {
                var logLevel = Config["Logging:LogLevel:SqlLog"];
                if (string.IsNullOrWhiteSpace(logLevel))
                {
                    return LogLevelEnum.TRACE;
                }
                else
                {
                    return LogLevelHelper.Parse(logLevel);
                }
            }
        }

        /// <summary>
        /// 是否支持幂等 注意：幂等操作不能使用主键自增，需要由业务自己生成主键ID
        /// </summary>
        protected bool IsSupportIdempotent
        {
            get
            {
                var isSupportIdempotent = Config["ConnectionStrings:IsSupportIdempotent"];
                if (string.IsNullOrWhiteSpace(isSupportIdempotent))
                {
                    return false;
                }

                return Convert.ToBoolean(isSupportIdempotent);
            }
        }

        #endregion

        #region 读取方法

        /// <summary>
        /// 根据ID查询模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型</returns>
        public virtual ModelT Select(IdT id, CommonUseData comData = null, string connectionId = null)
        {
            ModelT result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = Select(id, dbConn, GetDbTransaction(connId, AccessMode.SLAVE), comData: comData);
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据ID查询模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型</returns>
        public virtual ModelT Select(IdT id, string[] propertyNames, CommonUseData comData = null, string connectionId = null)
        {
            ModelT result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = Select(id, dbConn, GetDbTransaction(connId, AccessMode.SLAVE), propertyNames, comData: comData);
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据ID集合查询模型列表
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型列表</returns>
        public virtual IList<ModelT> Select(IdT[] ids, CommonUseData comData = null, string connectionId = null)
        {
            IList<ModelT> result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = Select(ids, dbConn, GetDbTransaction(connId, AccessMode.SLAVE), comData: comData);
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据ID集合查询模型列表
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型列表</returns>
        public virtual IList<ModelT> Select(IdT[] ids, string[] propertyNames, CommonUseData comData = null, string connectionId = null)
        {
            IList<ModelT> result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = Select(ids, dbConn, GetDbTransaction(connId, AccessMode.SLAVE), propertyNames, comData: comData);
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据ID统计模型数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型数</returns>
        public virtual int Count(IdT id, CommonUseData comData = null, string connectionId = null)
        {
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = Count(id, dbConn, GetDbTransaction(connId, AccessMode.SLAVE), comData: comData);
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 统计模型数
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型数</returns>
        public virtual int Count(CommonUseData comData = null, string connectionId = null)
        {
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = Count(dbConn, GetDbTransaction(connId, AccessMode.SLAVE), comData: comData);
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 查询模型列表
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型列表</returns>
        public virtual IList<ModelT> Select(CommonUseData comData = null, string connectionId = null)
        {
            IList<ModelT> result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = Select(dbConn, GetDbTransaction(connId, AccessMode.SLAVE), comData: comData);
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 查询模型列表
        /// </summary>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型列表</returns>
        public virtual IList<ModelT> Select(string[] propertyNames, CommonUseData comData = null, string connectionId = null)
        {
            IList<ModelT> result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = Select(dbConn, GetDbTransaction(connId, AccessMode.SLAVE), propertyNames, comData: comData);
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>分页信息</returns>
        public virtual PagingInfo<ModelT> SelectPage(int pageIndex, int pageSize, FilterInfo filter = null, CommonUseData comData = null, string connectionId = null)
        {
            BeforeFilterInfo(filter, comData);
            PagingInfo<ModelT> result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = SelectPage(pageIndex, pageSize, dbConn, filter, GetDbTransaction(connId, AccessMode.SLAVE), comData: comData);
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="filter">筛选</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>分页信息</returns>
        public virtual PagingInfo<ModelT> SelectPage(int pageIndex, int pageSize, string[] propertyNames, FilterInfo filter = null, CommonUseData comData = null, string connectionId = null)
        {
            BeforeFilterInfo(filter, comData);
            PagingInfo<ModelT> result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = SelectPage(pageIndex, pageSize, dbConn, filter, GetDbTransaction(connId, AccessMode.SLAVE), propertyNames, comData: comData);
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据ID和大于修改时间查询修改信息（多用于乐观锁的判断，以修改时间为判断）
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="mode">访问模式，默认为主库</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>只有修改信息的模型</returns>
        public virtual ModelT SelectModifyInfoByIdAndGeModifyTime(ModelT model, AccessMode mode = AccessMode.MASTER, CommonUseData comData = null, string connectionId = null)
        {
            ModelT result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = SelectModifyInfoByIdAndGeModifyTime(model, dbConn, GetDbTransaction(connId, mode), comData: comData);
            }, mode);

            return result;
        }

        /// <summary>
        /// 根据ID和大于修改时间查询修改信息列表（多用于乐观锁的判断，以修改时间为判断）
        /// </summary>
        /// <param name="models">模型数组</param>
        /// <param name="mode">访问模式，默认为主库</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>只有修改信息的模型列表</returns>
        public virtual IList<ModelT> SelectModifyInfosByIdAndGeModifyTime(ModelT[] models, AccessMode mode = AccessMode.MASTER, CommonUseData comData = null, string connectionId = null)
        {
            IList<ModelT> result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = SelectModifyInfosByIdAndGeModifyTime(models, dbConn, GetDbTransaction(connId, mode), comData: comData);
            }, mode);

            return result;
        }

        #endregion

        #region 写入方法

        /// <summary>
        /// 插入模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        public virtual int Insert(ModelT model, CommonUseData comData = null, string connectionId = null)
        {
            int result = 0;

            IdT tenantId;
            if (!ModelIsSetMerchantId(model) && IsExistsMerchantId(out tenantId, comData))
            {
                SetMerchantId(model, tenantId);
            }

            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = Insert(model, dbConn, GetDbTransaction(connId, AccessMode.MASTER), comData: comData);
            }, AccessMode.MASTER);

            return result;
        }

        /// <summary>
        /// 插入模型列表
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        public virtual int Insert(IList<ModelT> models, CommonUseData comData = null, string connectionId = null)
        {
            int result = 0;

            IdT tenantId = default(IdT);
            foreach (var m in models)
            {
                if (ModelIsSetMerchantId(m))
                {
                    continue;
                }

                if (!Identity.IsEmpty(tenantId) && IsExistsMerchantId(out tenantId, comData))
                {
                    SetMerchantId(m, tenantId);
                }
            }


            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = Insert(models, dbConn, GetDbTransaction(connId, AccessMode.MASTER), comData: comData);
            }, AccessMode.MASTER);

            return result;
        }

        /// <summary>
        /// 根据ID更新模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        public virtual int UpdateById(ModelT model, CommonUseData comData = null, string connectionId = null)
        {
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = UpdateById(model, dbConn, GetDbTransaction(connId, AccessMode.MASTER), comData: comData);
            }, AccessMode.MASTER);

            return result;
        }

        /// <summary>
        /// 根据ID更新模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        public virtual int UpdateById(ModelT model, string[] propertyNames, CommonUseData comData = null, string connectionId = null)
        {
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = UpdateById(model, dbConn, GetDbTransaction(connId, AccessMode.MASTER), propertyNames, comData: comData);
            }, AccessMode.MASTER);

            return result;
        }

        /// <summary>
        /// 根据ID删除模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        [Transaction(ConnectionIdIndex = 2)]
        public virtual int DeleteById(IdT id, CommonUseData comData = null, string connectionId = null)
        {
            int result = 0;
            IDictionary<string, string> slaveTables = SlaveTables();
            if (slaveTables.IsNullOrCount0())
            {
                DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
                {
                    result = DeleteById(id, dbConn, GetDbTransaction(connId, AccessMode.MASTER), comData: comData);
                }, AccessMode.MASTER);

                return result;
            }

            ExecTransaction((cId) =>
            {
                DbConnectionManager.BrainpowerExecute(cId, this, (connId, dbConn) =>
                {
                    result = DeleteById(id, dbConn, GetDbTransaction(connId, AccessMode.MASTER), comData: comData);

                    foreach (KeyValuePair<string, string> slTable in slaveTables)
                    {
                        DeleteSlaveTableByForeignKeys(slTable.Key, slTable.Value, new IdT[] { id }, dbConn, GetDbTransaction(connId, AccessMode.MASTER), comData: comData);
                    }
                }, AccessMode.MASTER);
            }, AccessMode.MASTER, connectionId: connectionId);

            return result;
        }

        /// <summary>
        /// 根据ID数组删除模型
        /// </summary>
        /// <param name="ids">ID数组</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        [Transaction(ConnectionIdIndex = 2)]
        public virtual int DeleteByIds(IdT[] ids, CommonUseData comData = null, string connectionId = null)
        {
            int result = 0;
            IDictionary<string, string> slaveTables = SlaveTables();
            if (slaveTables.IsNullOrCount0())
            {
                DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
                {
                    result = DeleteByIds(ids, dbConn, GetDbTransaction(connId, AccessMode.MASTER), comData: comData);
                }, AccessMode.MASTER);

                return result;
            }

            ExecTransaction((cId) =>
            {
                DbConnectionManager.BrainpowerExecute(cId, this, (connId, dbConn) =>
                {
                    result = DeleteByIds(ids, dbConn, GetDbTransaction(connId, AccessMode.MASTER), comData: comData);

                    foreach (KeyValuePair<string, string> slTable in slaveTables)
                    {
                        DeleteSlaveTableByForeignKeys(slTable.Key, slTable.Value, ids, dbConn, GetDbTransaction(connId, AccessMode.MASTER), comData: comData);
                    }
                }, AccessMode.MASTER);
            }, AccessMode.MASTER, connectionId: connectionId);

            return result;
        }

        /// <summary>
        /// 删除所有模型
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        [Transaction(ConnectionIdIndex = 1)]
        public virtual int Delete(CommonUseData comData = null, string connectionId = null)
        {
            int result = 0;
            IDictionary<string, string> slaveTables = SlaveTables();
            if (slaveTables.IsNullOrCount0())
            {
                DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
                {
                    result = Delete(dbConn, GetDbTransaction(connId, AccessMode.MASTER), comData: comData);
                }, AccessMode.MASTER);

                return result;
            }

            ExecTransaction((cId) =>
            {
                DbConnectionManager.BrainpowerExecute(cId, this, (connId, dbConn) =>
                {
                    result = Delete(dbConn, GetDbTransaction(connId, AccessMode.MASTER), comData: comData);

                    foreach (KeyValuePair<string, string> slTable in slaveTables)
                    {
                        DeleteSlaveTable(slTable.Key, dbConn, GetDbTransaction(connId, AccessMode.MASTER), comData: comData);
                    }
                }, AccessMode.MASTER);
            }, AccessMode.MASTER, connectionId: connectionId);

            return result;
        }

        #endregion

        #region 需要子类重写的方法

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
        protected abstract ModelT Select(IdT id, IDbConnection dbConnection, IDbTransaction dbTransaction = null, string[] propertyNames = null, CommonUseData comData = null);

        /// <summary>
        /// 根据ID集合查询模型列表
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <returns>模型列表</returns>
        protected abstract IList<ModelT> Select(IdT[] ids, IDbConnection dbConnection, IDbTransaction dbTransaction = null, string[] propertyNames = null, CommonUseData comData = null);

        /// <summary>
        /// 根据ID统计模型数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="comData">通用数据</param>
        /// <returns>模型数</returns>
        protected abstract int Count(IdT id, IDbConnection dbConnection, IDbTransaction dbTransaction = null, CommonUseData comData = null);

        /// <summary>
        /// 统计模型数
        /// </summary>
        /// <returns>模型数</returns>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="comData">通用数据</param>
        protected abstract int Count(IDbConnection dbConnection, IDbTransaction dbTransaction = null, CommonUseData comData = null);

        /// <summary>
        /// 查询模型列表
        /// </summary>
        /// <returns>模型列表</returns>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        protected abstract IList<ModelT> Select(IDbConnection dbConnection, IDbTransaction dbTransaction = null, string[] propertyNames = null, CommonUseData comData = null);

        /// <summary>
        /// 查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="filter">筛选</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <returns>分页信息</returns>
        protected abstract PagingInfo<ModelT> SelectPage(int pageIndex, int pageSize, IDbConnection dbConnection, FilterInfo filter = null, IDbTransaction dbTransaction = null, string[] propertyNames = null, CommonUseData comData = null);

        /// <summary>
        /// 根据ID和大于修改时间查询修改信息（多用于乐观锁的判断，以修改时间为判断）
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="comData">通用数据</param>
        /// <returns>只有修改信息的模型</returns>
        protected abstract ModelT SelectModifyInfoByIdAndGeModifyTime(ModelT model, IDbConnection dbConnection, IDbTransaction dbTransaction = null, CommonUseData comData = null);

        /// <summary>
        /// 根据ID和大于修改时间查询修改信息列表（多用于乐观锁的判断，以修改时间为判断）
        /// </summary>
        /// <param name="models">模型数组</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="comData">通用数据</param>
        /// <returns>只有修改信息的模型列表</returns>
        protected abstract IList<ModelT> SelectModifyInfosByIdAndGeModifyTime(ModelT[] models, IDbConnection dbConnection, IDbTransaction dbTransaction = null, CommonUseData comData = null);

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        /// <returns>所有字段映射集合</returns>
        public abstract string[] AllFieldMapProps();

        #endregion

        #region 写入方法

        /// <summary>
        /// 插入模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="comData">通用数据</param>
        /// <returns>影响行数</returns>
        protected abstract int Insert(ModelT model, IDbConnection dbConnection, IDbTransaction dbTransaction = null, CommonUseData comData = null);

        /// <summary>
        /// 插入模型列表
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="comData">通用数据</param>
        /// <returns>影响行数</returns>
        protected abstract int Insert(IList<ModelT> models, IDbConnection dbConnection, IDbTransaction dbTransaction = null, CommonUseData comData = null);

        /// <summary>
        /// 根据ID更新模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="comData">通用数据</param>
        /// <returns>影响行数</returns>
        protected abstract int UpdateById(ModelT model, IDbConnection dbConnection, IDbTransaction dbTransaction = null, string[] propertyNames = null, CommonUseData comData = null);

        /// <summary>
        /// 根据ID删除模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="comData">通用数据</param>
        /// <returns>影响行数</returns>
        protected abstract int DeleteById(IdT id, IDbConnection dbConnection, IDbTransaction dbTransaction = null, CommonUseData comData = null);

        /// <summary>
        /// 根据ID数组删除模型
        /// </summary>
        /// <param name="ids">ID数组</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="comData">通用数据</param>
        /// <returns>影响行数</returns>
        protected abstract int DeleteByIds(IdT[] ids, IDbConnection dbConnection, IDbTransaction dbTransaction = null, CommonUseData comData = null);

        /// <summary>
        /// 删除所有模型
        /// </summary>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="comData">通用数据</param>
        /// <returns>影响行数</returns>
        protected abstract int Delete(IDbConnection dbConnection, IDbTransaction dbTransaction = null, CommonUseData comData = null);

        #endregion

        /// <summary>
        /// 严格判断异常是否主键重复
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>异常是否主键重复</returns>
        public abstract bool StrictnessIsExceptionPkRepeat(Exception ex);

        #endregion

        #region 虚方法

        /// <summary>
        /// 从表集合
        /// Key:表名;Value:外键字段
        /// </summary>
        /// <returns>从表集合</returns>
        protected virtual IDictionary<string, string> SlaveTables() => null;

        /// <summary>
        /// 删除从表
        /// </summary>
        /// <param name="table">从表</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="comData">通用数据</param>
        /// <returns>影响行数</returns>
        protected virtual int DeleteSlaveTable(string table, IDbConnection dbConnection, IDbTransaction dbTransaction = null, CommonUseData comData = null) => 0;

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
        protected virtual int DeleteSlaveTableByForeignKeys(string table, string foreignKeyName, IdT[] foreignKeyValues, IDbConnection dbConnection, IDbTransaction dbTransaction = null, CommonUseData comData = null) => 0;

        /// <summary>
        /// 过滤信息前
        /// </summary>
        /// <param name="filter">过滤</param>
        /// <param name="comData">通用数据</param>
        protected virtual void BeforeFilterInfo(FilterInfo filter, CommonUseData comData = null) { }

        /// <summary>
        /// 主键是否自增
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>主键是否自增</returns>
        protected virtual bool PrimaryKeyIncr(IdT id) => Identity.IsEmpty(id);

        /// <summary>
        /// 是否存在租赁ID
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <returns>查询时是否需要追加租赁ID为过滤条件</returns>
        protected virtual bool IsExistsMerchantId(CommonUseData comData = null)
        {
            IdT currUserMerchantId;
            return IsExistsMerchantId(out currUserMerchantId, comData);
        }

        /// <summary>
        /// 是否存在租赁ID
        /// </summary>
        /// <param name="currUserMerchantId">当前用户租户ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>是否存在租赁ID</returns>
        protected virtual bool IsExistsMerchantId(out IdT currUserMerchantId, CommonUseData comData = null)
        {
            currUserMerchantId = default(IdT);

            if (ModelContainerMerchantId())
            {
                var currUser = UserTool<IdT>.GetCurrUser(comData);
                if (currUser == null)
                {
                    throw new ArgumentNullException("当前用户为空");
                }
                if (Identity.IsEmpty(currUser.TenantId))
                {
                    throw new ArgumentException("当前用户的租户ID为空");
                }
                currUserMerchantId = currUser.TenantId;

                return true;
            }

            return false;
        }

        /// <summary>
        /// 模型是否包含租户ID
        /// </summary>
        /// <returns>模型是否包含租户ID</returns>
        protected virtual bool ModelContainerMerchantId() => false;

        /// <summary>
        /// 模型是否已设置租户ID
        /// </summary>
        /// <param name="model">模型</param>
        /// <returns>模型是否已设置租户ID</returns>
        protected virtual bool ModelIsSetMerchantId(ModelT model) => false;

        /// <summary>
        /// 查询是否追加租户ID，默认为是
        /// </summary>
        /// <returns>查询是否不追加租户ID</returns>
        protected virtual bool SelectIsAppendMerchantId() => true;

        /// <summary>
        /// 设置模型的租赁ID
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="tenantId">租赁ID</param>
        protected virtual void SetMerchantId(ModelT model, IdT tenantId)
        {
            if (model == null)
            {
                return;
            }

            if (model is PersonTimeTenantInfo<ModelT>)
            {
                var temp = model as PersonTimeTenantInfo<IdT>;
                temp.TenantId = tenantId;
            }
            if (model is PersonTimeTenantInfo<IdT>)
            {
                var temp = model as PersonTimeTenantInfo<IdT>;
                temp.TenantId = tenantId;
            }
            else if (model is BasicUserInfo<IdT>)
            {
                var temp = model as BasicUserInfo<IdT>;
                temp.TenantId = tenantId;
            }
        }

        #endregion

        #region 受保护的方法

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="action">动作回调</param>
        /// <param name="accessMode">访问模式</param>
        /// <param name="transAttr">事务特性</param>
        /// <param name="connectionId">连接ID</param>
        protected void ExecTransaction(Action<string> action, AccessMode accessMode = AccessMode.MASTER, TransactionAttribute transAttr = null, string connectionId = null)
        {
            if (string.IsNullOrWhiteSpace(connectionId) || GetDbTransaction(connectionId, accessMode) == null)
            {
                IDbTransaction dbTransaction = null;
                try
                {
                    if (string.IsNullOrWhiteSpace(connectionId))
                    {
                        connectionId = NewConnectionId(accessMode);
                    }
                    if (transAttr == null)
                    {
                        transAttr = new TransactionAttribute()
                        {
                            Level = IsolationLevel.ReadCommitted
                        };
                    }
                    dbTransaction = BeginTransaction(connectionId, transAttr);

                    action(connectionId);

                    dbTransaction.Commit();
                }
                catch (Exception ex)
                {
                    if (dbTransaction != null)
                    {
                        dbTransaction.Rollback();
                    }
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    Release(connectionId);
                }
            }
            else
            {
                action(connectionId);
            }
        }

        /// <summary>
        /// 执行记录SQL日志
        /// </summary>
        /// <typeparam name="ReturnT">返回类型</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="callbackExecDb">回调执行数据库</param>
        /// <param name="comData">通用数据</param>
        /// <param name="tag">标签</param>
        /// <returns>返回值</returns>
        protected ReturnT ExecRecordSqlLog<ReturnT>(string sql, Func<ReturnT> callbackExecDb, CommonUseData comData = null, params string[] tag)
        {
            var level = SqlLogLevel;
            if (level == LogLevelEnum.NONE)
            {
                return callbackExecDb();
            }

            var eventId = comData.GetEventId();
            Exception errEx = null;
            var watch = new Stopwatch();
            watch.Start();
            try
            {
                var result = callbackExecDb();
                watch.Stop();

                return result;
            }
            catch (Exception ex)
            {
                watch.Stop();
                errEx = ex;

                throw new Exception(ex.Message, ex);
            }
            finally
            {
                var msg = sql + $".耗时:{watch.ElapsedMilliseconds}ms.";
                if (errEx == null)
                {
                    switch (level)
                    {
                        case LogLevelEnum.TRACE:
                            Log.TraceAsync(msg, source: this.GetType().Name, eventId: eventId, tags: tag);

                            break;

                        case LogLevelEnum.DEBUG:
                            Log.DebugAsync(msg, source: this.GetType().Name, eventId: eventId, tags: tag);

                            break;

                        case LogLevelEnum.INFO:
                            Log.InfoAsync(msg, source: this.GetType().Name, eventId: eventId, tags: tag);

                            break;

                        case LogLevelEnum.WRAN:
                            Log.WranAsync(msg, source: this.GetType().Name, eventId: eventId, tags: tag);

                            break;

                        case LogLevelEnum.ERROR:
                            Log.ErrorAsync(msg, source: this.GetType().Name, eventId: eventId, tags: tag);

                            break;

                        case LogLevelEnum.FATAL:
                            Log.FatalAsync(msg, source: this.GetType().Name, eventId: eventId, tags: tag);

                            break;
                    }
                }
                else
                {
                    Log.ErrorAsync(msg, ex: errEx, source: this.GetType().Name, eventId: eventId, tags: tag);
                }
            }
        }

        #endregion
    }
}
