using Hzdtf.Logger.Contract;
using Hzdtf.Persistence.Contract.Management;
using Hzdtf.Utility;
using Hzdtf.Utility.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace Hzdtf.Persistence.Contract.Basic
{
    /// <summary>
    /// 持久化连接基类
    /// 本类支持自定义连接字符串，取连接字符串顺序为：本类重写>回调获取>读取默认（配置文件）
    /// 如果需要动态获取连接字符串，请设置回调：PersistenceConnectionBase.DynamcGetConnectionString
    /// @ 黄振东
    /// </summary>
    public abstract class PersistenceConnectionBase : IPersistenceConnection
    {
        /// <summary>
        /// 日志
        /// </summary>
        public ILogable Log
        {
            get;
            set;
        } = LogTool.DefaultLog;

        /// <summary>
        /// 配置
        /// </summary>
        protected IConfiguration Config
        {
            get => App.CurrConfig;
        }

        /// <summary>
        /// 默认连接字符串，默认取0主库；1从库
        /// </summary>
        public IDefaultConnectionString DefaultConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// 动态获取连接字符串
        /// </summary>
        public static Func<AccessMode, string> DynamcGetConnectionString;

        /// <summary>
        /// 主持久化连接信息
        /// </summary>
        public PersistenceConectionInfo MasterPersistenceConnection
        {
            get => GetPersistenceConnection(AccessMode.MASTER, () =>
            {
                return GetMasterConnectionString();
            }, 0);
        }

        /// <summary>
        /// 从持久化连接信息
        /// </summary>
        private PersistenceConectionInfo slavePersistenceConnection;

        /// <summary>
        /// 从持久化连接信息
        /// </summary>
        public PersistenceConectionInfo SlavePersistenceConnection
        {
            get => GetPersistenceConnection(AccessMode.SLAVE, ()=>
            {
                return GetSlaveConnectionString();
            }, 1);
        }

        /// <summary>
        /// 获取持久化连接对象
        /// </summary>
        /// <param name="accessMode">访问模式</param>
        /// <param name="getThisConnString">获取本身连接字符串</param>
        /// <param name="defaultConnectionStringIndex">默认连接字符串索引</param>
        /// <returns>持久化连接对象</returns>
        private PersistenceConectionInfo GetPersistenceConnection(AccessMode accessMode, Func<string> getThisConnString, byte defaultConnectionStringIndex)
        {
            var connStr = getThisConnString();
            if (string.IsNullOrWhiteSpace(connStr))
            {
                if (DynamcGetConnectionString != null)
                {
                    connStr = DynamcGetConnectionString(accessMode);
                    if (string.IsNullOrWhiteSpace(connStr))
                    {
                        connStr = DefaultConnectionString.Connections[defaultConnectionStringIndex];
                        if (string.IsNullOrWhiteSpace(connStr) && defaultConnectionStringIndex == 1)
                        {
                            connStr = DefaultConnectionString.Connections[0];
                        }
                    }
                }
                else
                {
                    connStr = DefaultConnectionString.Connections[defaultConnectionStringIndex];
                    if (string.IsNullOrWhiteSpace(connStr) && defaultConnectionStringIndex == 1)
                    {
                        connStr = DefaultConnectionString.Connections[0];
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(connStr))
            {
                throw new ArgumentException($"{this.GetType().Name}.{accessMode}.找不到数据库连接字符串");
            }

            return CreatePersistenceConnection(null, connStr, accessMode);
        }

        /// <summary>
        /// 新建一个连接ID
        /// </summary>
        /// <param name="accessMode">访问模式</param>
        /// <returns>连接ID</returns>
        public string NewConnectionId(AccessMode accessMode = AccessMode.MASTER) => DbConnectionManager.NewConnectionId(this, accessMode);

        /// <summary>
        /// 释放连接ID
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        public void Release(string connectionId) => DbConnectionManager.Release(connectionId);

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="isolation">事务级别</param>
        /// <returns>数据库事务</returns>
        public IDbTransaction BeginTransaction(string connectionId, IsolationLevel isolation = IsolationLevel.ReadCommitted) => DbConnectionManager.BeginTransaction(connectionId, this, isolation);

        /// <summary>
        /// 根据连接ID获取数据库事务
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="accessMode">访问模式</param>
        /// <returns>数据库事务</returns>
        public IDbTransaction GetDbTransaction(string connectionId, AccessMode accessMode = AccessMode.MASTER) => DbConnectionManager.GetDbTransaction(connectionId, this, accessMode);

        /// <summary>
        /// 获取主连接字符串
        /// </summary>
        /// <returns>主连接字符串</returns>
        protected virtual string GetMasterConnectionString() => null;

        /// <summary>
        /// 获取从连接字符串
        /// </summary>
        /// <returns>从连接字符串</returns>
        protected virtual string GetSlaveConnectionString() => null;

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>数据库连接</returns>
        public abstract IDbConnection CreateDbConnection(string connectionString);

        /// <summary>
        /// 创建持久化连接信息
        /// </summary>
        /// <param name="persistenceConection">持久化连接</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="accessMode">访问模式</param>
        /// <param name="setAction">设置动作</param>
        /// <returns>持久化连接信息</returns>
        private PersistenceConectionInfo CreatePersistenceConnection(PersistenceConectionInfo persistenceConection, string connectionString, AccessMode accessMode, Action<PersistenceConectionInfo> setAction = null)
        { 
            if (persistenceConection != null || string.IsNullOrWhiteSpace(connectionString))
            {
                return persistenceConection;
            }

            persistenceConection = new PersistenceConectionInfo()
            {
                ConnectionString = connectionString,
                AccessMode = accessMode
            };

            if (setAction != null)
            {
                setAction(persistenceConection);
            }

            return persistenceConection;
        }
    }
}
