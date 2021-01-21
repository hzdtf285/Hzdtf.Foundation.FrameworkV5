using Hzdtf.Logger.Contract;
using Hzdtf.Utility;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Service.Impl
{
    /// <summary>
    /// 基本服务基类
    /// @ 黄振东
    /// </summary>
    public abstract class BasicServiceBase
    {
        #region 属性与字段

        /// <summary>
        /// 日志
        /// </summary>
        public ILogable Log
        {
            get;
            set;
        } = LogTool.DefaultLog;

        /// <summary>
        /// 配置
        /// </summary>
        protected IConfiguration Config
        {
            get => App.CurrConfig;
        }

        #endregion
    }
}
