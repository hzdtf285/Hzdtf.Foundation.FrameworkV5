﻿using Hzdtf.BasicFunction.Model.Expand.User;
using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Hzdtf.BasicFunction.Model;
using Hzdtf.AUC.Contract.IdentityAuth;
using Hzdtf.AUC.Contract.IdentityAuth.Token;
using Hzdtf.Utility;
using Hzdtf.Utility.Factory;

namespace Hzdtf.BasicFunction.Controller.Home
{
    /// <summary>
    /// 授权控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        /// <summary>
        /// 身份授权
        /// </summary>
        public IIdentityAuth<int, UserInfo> IdentityAuth
        {
            get;
            set;
        }

        /// <summary>
        /// 身份基本授权
        /// </summary>
        public IIdentityAuth<int, BasicUserInfo<int>> IdentityBasicAuth
        {
            get;
            set;
        }

        /// <summary>
        /// 身份退出
        /// </summary>
        public IIdentityExit IdentityExit
        {
            get;
            set;
        }

        /// <summary>
        /// 身份令牌授权
        /// </summary>
        public IIdentityTokenAuth IdentityTokenAuth
        {
            get;
            set;
        }

        /// <summary>
        /// 通用数据工厂
        /// </summary>
        public ISimpleFactory<HttpContext, CommonUseData> ComDataFactory
        {
            get;
            set;
        }

        /// <summary>
        /// 是否禁用登录
        /// </summary>
        protected bool IsDisabledLogin
        {
            get
            {
                var str = App.CurrConfig["IsDisabledLogin"];
                if (string.IsNullOrWhiteSpace(str))
                {
                    return false;
                }

                bool temp;
                if (bool.TryParse(str, out temp))
                {
                    return temp;
                }

                return false;
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginInfo">登录信息</param>
        /// <returns>返回信息</returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public virtual ReturnInfo<LoginReturnInfo> Login(LoginInfo loginInfo)
        {
            if (IdentityAuth == null && IdentityBasicAuth == null)
            {
                var re = new ReturnInfo<LoginReturnInfo>();
                re.SetFailureMsg("不支持登录");

                return re;
            }


            var comData = HttpContext.CreateCommonUseData(ComDataFactory, menuCode: "Authorization", functionCodes: "Login");
            return ExecLogin(loginInfo, (user, pwd, reInfo) =>
            {
                if (IdentityAuth != null)
                {
                    var busRe = IdentityAuth.Accredit(loginInfo.LoginId, loginInfo.Password, comData);
                    reInfo.FromBasic(busRe);
                }
                else
                {
                    var busRe = IdentityBasicAuth.Accredit(loginInfo.LoginId, loginInfo.Password, comData);
                    reInfo.FromBasic(busRe);
                }

                reInfo.Data = reInfo.Data;
            });
        }

        /// <summary>
        /// 登录并返回令牌
        /// </summary>
        /// <param name="loginInfo">登录信息</param>
        /// <returns>返回信息</returns>
        [HttpPost("LoginToToken")]
        [AllowAnonymous]
        public virtual ReturnInfo<LoginReturnInfo> LoginToToken(LoginInfo loginInfo)
        {
            if (IdentityTokenAuth == null)
            {
                var re = new ReturnInfo<LoginReturnInfo>();
                re.SetFailureMsg("不支持令牌登录");

                return re;
            }
            var comData = HttpContext.CreateCommonUseData(ComDataFactory, menuCode: "Authorization", functionCodes: "LoginToToken");
            return ExecLogin(loginInfo, (user, pwd, reInfo) =>
            {
                var busRe = IdentityTokenAuth.AccreditToToken(loginInfo.LoginId, loginInfo.Password, comData);
                reInfo.FromBasic(busRe);
                if (reInfo.Success())
                {
                    reInfo.Data.Token = busRe.Data;
                }
            });
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns>返回信息</returns>
        [HttpDelete("Logout")]
        public virtual ReturnInfo<bool> Logout()
        {
            if (IsDisabledLogin)
            {
                var re = new ReturnInfo<bool>();
                re.SetFailureMsg("登录已禁用");
            }
            if (IdentityExit == null)
            {
                var re = new ReturnInfo<bool>();
                re.SetFailureMsg("不支持登出");

                return re;
            }

            var returnInfo = IdentityExit.Exit();
            if (returnInfo.Success())
            {
                HttpContext.Session.Clear();
            }

            return returnInfo;
        }

        /// <summary>
        /// 获取是否需要验证码
        /// </summary>
        /// <returns>返回信息</returns>
        [HttpGet("GetIsVerificationCode")]
        [AllowAnonymous]
        public ReturnInfo<bool> GetIsVerificationCode()
        {
            ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
            returnInfo.Data = IsNeedIsVerificationCode();

            return returnInfo;
        }

        /// <summary>
        /// 执行登录
        /// </summary>
        /// <param name="loginInfo">登录信息</param>
        /// <param name="callbackLogin">回调登录</param>
        /// <returns>返回信息</returns>
        private ReturnInfo<LoginReturnInfo> ExecLogin(LoginInfo loginInfo, Action<string, string, ReturnInfo<LoginReturnInfo>> callbackLogin)
        {
            ReturnInfo<LoginReturnInfo> returnInfo = new ReturnInfo<LoginReturnInfo>()
            {
                Data = new LoginReturnInfo()
                {
                    ReturnUrl = loginInfo.ReturnUrl
                }
            };
            if (IsDisabledLogin)
            {
                returnInfo.SetFailureMsg("登录已禁用");

                return returnInfo;
            }

            int num = HttpContext.Session.GetInt32("ErrLoginNum").GetValueOrDefault();
            num++;
            //错误登录超过3次则需要验证码
            if (num > 3)
            {
                if (string.IsNullOrWhiteSpace(loginInfo.VerificationCode))
                {
                    returnInfo.Data.IsVerificationCode = true;
                    returnInfo.SetFailureMsg("请输入验证码");

                    return returnInfo;
                }

                if (string.Compare(loginInfo.VerificationCode, HttpContext.Session.GetString("VerificationCode"), true) != 0)
                {
                    returnInfo.Data.IsVerificationCode = true;
                    returnInfo.SetFailureMsg("验证码不对，请输入正确的验证码");

                    return returnInfo;
                }
            }

            callbackLogin(loginInfo.LoginId, loginInfo.Password, returnInfo);
            if (returnInfo.Failure() || returnInfo.Data == null)
            {
                HttpContext.Session.SetInt32("ErrLoginNum", num);
                returnInfo.Data.IsVerificationCode = IsNeedIsVerificationCode();
            }
            else
            {
                HttpContext.Session.Remove("VerificationCode");
                HttpContext.Session.Remove("ErrLoginNum");

                returnInfo.Data.ReturnUrl = loginInfo.ReturnUrl;
            }

            return returnInfo;
        }

        /// <summary>
        /// 判断是否需要验证码
        /// </summary>
        /// <returns>是否需要验证码</returns>
        private bool IsNeedIsVerificationCode() => HttpContext.Session.GetInt32("ErrLoginNum").GetValueOrDefault() > 2;
    }
}
