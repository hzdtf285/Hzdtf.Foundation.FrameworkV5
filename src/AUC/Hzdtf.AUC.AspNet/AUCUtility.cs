using Hzdtf.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.AUC.AspNet
{
    /// <summary>
    /// 鉴权中心辅助类
    /// @ 黄振东
    /// </summary>
    public static class AUCUtility
    {
        /// <summary>
        /// 创建票据验证参数
        /// </summary>
        /// <param name="config">配置</param>
        /// <returns>票据验证参数</returns>
        public static TokenValidationParameters CreateTokenValiParam(IdentityAuthOptions config = null)
        {
            if (config == null)
            {
                config = new IdentityAuthOptions();
            }
            return new TokenValidationParameters
            {
                ValidateIssuer = true,//是否验证Issuer
                ValidateAudience = true,//是否验证Audience
                ValidateLifetime = true,//是否验证失效时间
                ClockSkew = TimeSpan.FromSeconds(30),
                ValidateIssuerSigningKey = true,//是否验证SecurityKey
                ValidAudience = config.Config["Jwt:Domain"],//Audience
                ValidIssuer = config.Config["Jwt:Domain"],//Issuer，这两项和前面签发jwt的设置一致
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Config["Jwt:SecurityKey"]))//拿到SecurityKey
            };
        }
    }
}
