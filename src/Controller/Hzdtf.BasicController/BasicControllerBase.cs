using Hzdtf.Logger.Contract;
using Hzdtf.Service.Contract;
using Hzdtf.Utility;
using Hzdtf.Utility.Localization;
using Hzdtf.Utility.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Hzdtf.BasicController
{
    /// <summary>
    /// 基本控制器基类
    /// </summary>
    /// <typeparam name="IdT">Id类型</typeparam>
    /// <typeparam name="ModelT">模型类型</typeparam>
    /// <typeparam name="ServiceT">服务类型</typeparam>
    public abstract class BasicControllerBase<IdT, ModelT, ServiceT> : ControllerBase
        where ModelT : SimpleInfo<IdT>
        where ServiceT : IService<IdT, ModelT>
    {
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

        /// <summary>
        /// 服务
        /// </summary>
        public ServiceT Service
        {
            get;
            set;
        }

        /// <summary>
        /// 本地化
        /// </summary>
        public ILocalization Localize
        {
            get;
            set;
        }

        /// <summary>
        /// 菜单编码
        /// </summary>
        /// <returns>菜单编码</returns>
        protected virtual string MenuCode() => null;
    }
}
