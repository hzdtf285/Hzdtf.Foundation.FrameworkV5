using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hzdtf.Utility.Model
{
    /// <summary>
    /// 授权票据接口
    /// @ 黄振东
    /// </summary>
    public interface IAuthToken
    {
        /// <summary>
        /// 获取票据
        /// </summary>
        /// <returns>票据</returns>
        string GetToken();
    }
}
