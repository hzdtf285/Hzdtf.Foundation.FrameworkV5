using Hzdtf.AUC.Contract;
using Hzdtf.AUC.Contract.IdentityAuth;
using Hzdtf.AUC.Contract.User;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.AUC.AspNet
{
    /// <summary>
    /// 身份认证证件单元读取
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    /// <typeparam name="UserT">用户类型</typeparam>
    public class IdentityAuthClaimReader<IdT, UserT> : IIdentityAuthReader<IdT, UserT>, IIdentityAuthContextReader<IdT, UserT>
        where UserT : BasicUserInfo<IdT>
    {
        /// <summary>
        /// Http上下文访问
        /// </summary>
        private readonly IHttpContextAccessor httpContext;

        /// <summary>
        /// 授权用户数据
        /// </summary>
        private readonly IAuthUserData<IdT, UserT> authUserData;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="authUserData">授权用户数据</param>
        public IdentityAuthClaimReader(IAuthUserData<IdT, UserT> authUserData)
        {
            this.authUserData = authUserData;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="httpContext">Http上下文访问</param>
        /// <param name="authUserData">授权用户数据</param>
        public IdentityAuthClaimReader(IHttpContextAccessor httpContext, IAuthUserData<IdT, UserT> authUserData)
        {
            this.httpContext = httpContext;
            this.authUserData = authUserData;
        }

        /// <summary>
        /// 判断是否已授权
        /// </summary>
        /// <returns>返回信息</returns>
        public ReturnInfo<bool> IsAuthed() => IsAuthed(httpContext.HttpContext);

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public ReturnInfo<UserT> Reader() => Reader(httpContext.HttpContext);

        /// <summary>
        /// 判断是否已授权
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>返回信息</returns>
        public ReturnInfo<bool> IsAuthed(HttpContext context)
        {
            return new ReturnInfo<bool>()
            {
                Data = context != null && context.User != null && context.User.Identity != null && context.User.Identity.IsAuthenticated
            };
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>数据</returns>
        public ReturnInfo<UserT> Reader(HttpContext context)
        {
            var returnInfo = new ReturnInfo<UserT>();
            var isAuthReturnInfo = IsAuthed(context);
            if (isAuthReturnInfo.Success() && isAuthReturnInfo.Data)
            {
                var claims = context.User.Claims;
                if (claims == null)
                {
                    return returnInfo;
                }

                returnInfo.Data = IdentityAuthUtil.GetUserDataFromClaims<IdT, UserT>(claims, authUserData);
            }

            return returnInfo;
        }
    }
}
