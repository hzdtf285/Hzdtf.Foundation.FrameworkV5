using Hzdtf.Utility.Factory;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.TheOperation;
using Hzdtf.Utility.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hzdtf.AUC.AspNet
{
    /// <summary>
    /// 通用数据工厂
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    /// <typeparam name="UserT">用户类型</typeparam>
    public class CommonUseDataFactory<IdT, UserT> : ISimpleFactory<HttpContext, CommonUseData>
        where UserT : BasicUserInfo<IdT>
    {
        /// <summary>
        /// 身份读取
        /// </summary>
        private readonly IIdentityAuthContextReader<IdT, UserT> authReader;

        /// <summary>
        /// 本次操作
        /// </summary>
        private readonly ITheOperation theOperation;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="authReader">身份读取</param>
        /// <param name="theOperation">本次操作</param>
        public CommonUseDataFactory(IIdentityAuthContextReader<IdT, UserT> authReader = null, ITheOperation theOperation = null)
        {
            this.authReader = authReader;
            this.theOperation = theOperation;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="context">上下文 </param>
        /// <returns>产品</returns>
        public CommonUseData Create(HttpContext context)
        {
            var result = context.CreateBasicCommonUseData();
            if (context != null && authReader != null)
            {
                var re = authReader.Reader(context);
                if (re.Success())
                {
                    result.CurrUser = re.Data;
                }
            }
            if (string.IsNullOrWhiteSpace(result.EventId))
            {
                if (theOperation != null)
                {
                    result.EventId = theOperation.EventId;
                }
                if (string.IsNullOrWhiteSpace(result.EventId))
                {
                    result.EventId = StringUtil.NewShortGuid();
                }
            }           

            return result;
        }
    }
}
