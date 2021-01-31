using Hzdtf.Utility.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.AUC.Contract.IdentityAuth.Token
{
    /// <summary>
    /// 身份令牌授权接口
    /// @ 黄振东
    /// </summary>
    public interface IIdentityTokenAuth
    {
        /// <summary>
        /// 授权并生成令牌
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="password">密码</param>
        /// <param name="otherData">其他数据</param>
        /// <returns>返回信息</returns>
        ReturnInfo<string> AccreditToToken(string user, string password, object otherData = null);
    }
}
