using Hzdtf.Utility.Data;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.AUC.AspNet
{
    /// <summary>
    /// 身份认证上下文读取接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    /// <typeparam name="UserT">用户类型</typeparam>
    public interface IIdentityAuthContextReader<IdT, UserT>
        where UserT : BasicUserInfo<IdT>
    {
        /// <summary>
        /// 判断是否已授权
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> IsAuthed(HttpContext context);

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>数据</returns>
        ReturnInfo<UserT> Reader(HttpContext context);
    }
}
