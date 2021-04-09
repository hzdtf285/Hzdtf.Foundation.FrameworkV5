using Hzdtf.Utility.Attr;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Persistence.Contract.Basic
{
    /// <summary>
    /// 数据库事务工厂
    /// @ 黄振东
    /// </summary>
    public interface IDbTransactionFactory
    {
        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="transAttr">事务特性</param>
        /// <returns>数据库事务</returns>
        IDbTransaction Begin(IDbConnection connection, TransactionAttribute transAttr);
    }

    /// <summary>
    /// 本地数据库事务工厂
    /// @ 黄振东
    /// </summary>
    public interface ILocalDbTransactionFactory : IDbTransactionFactory
    {
    }

    /// <summary>
    /// 默认本地数据库事务工厂
    /// @ 黄振东
    /// </summary>
    public class DefaultLocalDbTransactionFactory : ILocalDbTransactionFactory
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="transAttr">事务特性</param>
        /// <returns>数据库事务</returns>
        public IDbTransaction Begin(IDbConnection connection, TransactionAttribute transAttr)
        {
            return connection.BeginTransaction(transAttr.Level);
        }
    }

    /// <summary>
    /// 分布式数据库事务工厂
    /// @ 黄振东
    /// </summary>
    public interface IDistributionDbTransactionFactory : IDbTransactionFactory
    {
    }
}
