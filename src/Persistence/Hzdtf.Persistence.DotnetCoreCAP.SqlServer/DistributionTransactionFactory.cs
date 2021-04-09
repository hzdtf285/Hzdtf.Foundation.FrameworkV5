using DotNetCore.CAP;
using Hzdtf.Persistence.Contract.Basic;
using Hzdtf.Utility.Attr;
using System;
using System.Data;

namespace Hzdtf.Persistence.DotnetCoreCAP.SqlServer
{
    /// <summary>
    /// 分布式事务工厂
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class DistributionTransactionFactory : IDistributionDbTransactionFactory
    {
        /// <summary>
        /// CAP发布者
        /// </summary>
        private readonly ICapPublisher publisher;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="publisher">CAP发布者</param>
        public DistributionTransactionFactory(ICapPublisher publisher)
        {
            this.publisher = publisher;
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="transAttr">事务特性</param>
        /// <returns>数据库事务</returns>
        public IDbTransaction Begin(IDbConnection connection, TransactionAttribute transAttr)
        {
            return connection.BeginTransaction(publisher);
        }
    }
}
