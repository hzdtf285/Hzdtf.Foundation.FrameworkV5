using Hzdtf.Persistence.Contract.Management;
using Hzdtf.Utility.Attr;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Hzdtf.Utility.Model.Return;
using Microsoft.AspNetCore.Authorization;

namespace Hzdtf.BasicController
{
    /// <summary>
    /// 系统控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SystemController : ControllerBase
    {
        /// <summary>
        /// 获取当前数据库连接数
        /// </summary>
        /// <returns>当前数据库连接数</returns>
        [HttpGet("CurrConnCount")]
        public int CurrConnCount() => DbConnectionManager.CurrDbConnCount;

        /// <summary>
        /// 获取当前数据库事务数
        /// </summary>
        /// <returns>当前数据库事务数</returns>
        [HttpGet("CurrTransactionCount")]
        public int CurrTransactionCount() => DbConnectionManager.CurrDbTransactionCount;

        /// <summary>
        /// 获取当前语言文化
        /// </summary>
        /// <returns>返回信息</returns>
        [HttpGet("CurrCulture")]
        [AllowAnonymous]
        public ReturnInfo<string> CurrCulture()
        {
            var re = new ReturnInfo<string>();
            re.Data = HttpContext.GetCurrentCulture();

            return re;
        }

        /// <summary>
        /// 设置当前语言文化
        /// </summary>
        /// <param name="culture">文化</param>
        /// <returns>返回信息</returns>
        [HttpPatch("SetCurrCulture/{culture}")]
        [AllowAnonymous]
        public BasicReturnInfo SetCurrCulture(string culture)
        {
            HttpContext.SetCurrentCulture(culture);

            return new BasicReturnInfo();
        }
    }
}
