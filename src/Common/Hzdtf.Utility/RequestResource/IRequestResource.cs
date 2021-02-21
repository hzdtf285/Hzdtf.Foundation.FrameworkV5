using Hzdtf.Utility.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.RequestResource
{
    /// <summary>
    /// 请求资源接口
    /// key：请求ID，value：资源键
    /// @ 黄振东
    /// </summary>
    public interface IRequestResource : ISingleTypeCache<string, string>
    {
    }
}
