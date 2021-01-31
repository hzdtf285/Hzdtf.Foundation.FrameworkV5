using Hzdtf.Utility.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Persistence.Contract.Basic
{
    /// <summary>
    /// 持久化连接工厂接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="PersistencerT">持久化类型</typeparam>
    public interface IPersistenceConnectionFactory<PersistencerT> : IGeneralFactory<PersistencerT>
        where PersistencerT : IPersistenceConnection
    {
    }
}
