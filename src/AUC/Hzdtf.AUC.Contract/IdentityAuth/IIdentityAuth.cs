using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using System;

namespace Hzdtf.AUC.Contract.IdentityAuth
{
    /// <summary>
    /// 身份授权接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    /// <typeparam name="UserT">用户类型</typeparam>
    public interface IIdentityAuth<IdT, UserT> where UserT : BasicUserInfo<IdT>
    {
        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="password">密码</param>
        /// <param name="otherData">其他数据</param>
        /// <returns>返回信息</returns>
        ReturnInfo<UserT> Accredit(string user, string password, object otherData = null);
    }
}
