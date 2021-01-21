using Hzdtf.BasicFunction.Model;
using Hzdtf.Service.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Service.Contract
{
    /// <summary>
    /// 序列服务接口
    /// @ 黄振东
    /// </summary>
    public partial interface ISequenceService : IService<int, SequenceInfo>
    {
    }
}
