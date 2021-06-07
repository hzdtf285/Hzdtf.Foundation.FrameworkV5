using Hzdtf.AUC.AspNet;
using Hzdtf.Utility.AspNet.Extensions;
using Hzdtf.Utility.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.AUC.AspNet.JwtAuthHandler
{
    /// <summary>
    /// Jwt授权票据授权处理基类
    /// @ 黄振东
    /// </summary>
    public abstract class JwtTokenAuthHandlerBase : IAuthenticationHandler, IHttpContextAuthToken
    {
        /// <summary>
        /// 方案
        /// </summary>
        private AuthenticationScheme scheme;

        /// <summary>
        /// 上下文
        /// </summary>
        protected HttpContext context;

        /// <summary>
        /// JWT处理
        /// </summary>
        private readonly static JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();

        /// <summary>
        /// 验证参数
        /// </summary>
        private readonly TokenValidationParameters valiParam;

        /// <summary>
        /// 构造方法
        /// </summary>
        public JwtTokenAuthHandlerBase()
        {
            valiParam = AUCUtility.CreateTokenValiParam();
        }

        /// <summary>
        /// 异步初始化认证
        /// </summary>
        /// <param name="scheme"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            this.scheme = scheme;
            this.context = context;

            return Task.CompletedTask;
        }

        /// <summary>
        /// 异步授权处理
        /// </summary>
        /// <returns>授权结果任务</returns>
        public Task<AuthenticateResult> AuthenticateAsync()
        {
            var token = GetToken();
            if (string.IsNullOrWhiteSpace(token))
            {
                return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
            }
           
            SecurityToken seToken;
            ClaimsPrincipal princ;
            try
            {
                princ = jwtHandler.ValidateToken(token, valiParam, out seToken);
                if (princ == null)
                {
                    return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
                }
            }
            catch (SecurityTokenExpiredException)
            {
                return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
            }
            catch (SecurityTokenInvalidAudienceException)
            {
                return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
            }
            catch (SecurityTokenInvalidSignatureException)
            {
                return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
            }

            var ticket = new AuthenticationTicket(princ, scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

        /// <summary>
        /// 异步未登录
        /// </summary>
        /// <param name="properties">授权属性</param>
        /// <returns>任务</returns>
        public Task ChallengeAsync(AuthenticationProperties properties)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            return Task.CompletedTask;
        }

        /// <summary>
        /// 异步禁止访问
        /// </summary>
        /// <param name="properties">授权属性</param>
        /// <returns>任务</returns>
        public Task ForbidAsync(AuthenticationProperties properties)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;

            return Task.CompletedTask;
        }

        /// <summary>
        /// 获取票据
        /// </summary>
        /// <returns>票据</returns>
        public abstract string GetToken();

        /// <summary>
        /// 设置Http上下文
        /// </summary>
        /// <param name="context">Http上下文</param>
        public void SetHttpContext(HttpContext context)
        {
            this.context = context;
        }
    }
}
