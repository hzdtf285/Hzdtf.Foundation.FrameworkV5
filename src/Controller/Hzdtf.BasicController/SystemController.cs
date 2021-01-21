using Hzdtf.Persistence.Contract.Management;
using Hzdtf.Utility.Attr;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicController
{
    /// <summary>
    /// 系统控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [ApiController]
    [Route("api/[controller]")]
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
    }
}
