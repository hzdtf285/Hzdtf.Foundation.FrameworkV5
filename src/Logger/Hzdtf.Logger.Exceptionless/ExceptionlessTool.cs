using Exceptionless;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Hzdtf.Utility;
using Hzdtf.Logger.Contract;
using Exceptionless.Logging;

namespace Hzdtf.Logger.Exceptionless
{
    /// <summary>
    /// Exceptionless分布式日志工具
    /// @ 黄振东
    /// </summary>
    public static class ExceptionlessTool
    {
        /// <summary>
        /// 同步API键
        /// </summary>
        private readonly static object syncApiKey = new object();

        /// <summary>
        /// API键
        /// </summary>
        private static string apiKey;

        /// <summary>
        /// API键，默认取配置文件Exceptionless:ApiKey
        /// </summary>
        public static string ApiKey
        {
            get
            {
                if (apiKey == null)
                {
                    lock (syncApiKey)
                    {
                        apiKey = App.CurrConfig["Exceptionless:ApiKey"];
                    }
                }

                return apiKey;
            }
        }

        /// <summary>
        /// 同步服务URL地址
        /// </summary>
        private readonly static object syncServerUrl = new object();

        /// <summary>
        /// 服务URL地址
        /// </summary>
        private static string serverUrl;

        /// <summary>
        /// 服务URL地址，默认取配置文件Exceptionless:ServerUrl
        /// </summary>
        public static string ServerUrl
        {
            get
            {
                if (serverUrl == null)
                {
                    lock (syncServerUrl)
                    {
                        serverUrl = App.CurrConfig["Exceptionless:ServerUrl"];
                    }
                }

                return serverUrl;
            }
        }

        /// <summary>
        /// 获取记录级别
        /// </summary>
        /// <returns>记录级别</returns>
        public static string RecordLevel
        {
            get
            {
                if (string.IsNullOrWhiteSpace(App.CurrConfig["Logging:LogLevel:Default"]))
                {
                    if (string.IsNullOrWhiteSpace(App.CurrConfig["HzdtfLog:LogLevel:Default"]))
                    {
                        return null;
                    }
                    else
                    {
                        return App.CurrConfig["HzdtfLog:LogLevel:Default"];
                    }
                }
                else
                {
                    return App.CurrConfig["Logging:LogLevel:Default"];
                }
            }
        }

        /// <summary>
        /// 是否执行默认初始化
        /// </summary>
        private static bool isExecDefaultInit;

        /// <summary>
        /// 同步是否执行默认初始化
        /// </summary>
        private static readonly object syncIsExecDefaultInit = new object();

        /// <summary>
        /// 静态构造方法
        /// </summary>
        static ExceptionlessTool()
        {
            SetIsFilterUnrelatedLog();
        }

        /// <summary>
        /// 设置初始化
        /// </summary>
        /// <param name="apiKey">API键</param>
        /// <param name="serverUrl">服务URL地址</param>
        /// <param name="isFilterUnrelatedLog">是否过滤掉无关日志</param>
        /// <param name="minLogLevel">最小日志等级,如果为null，则从配置里取</param>
        public static void SetInit(string apiKey = null, string serverUrl = null, bool isFilterUnrelatedLog = true, LogLevel minLogLevel = null)
        {
            lock (syncApiKey)
            {
                ExceptionlessTool.apiKey = apiKey;
            }
            lock (syncApiKey)
            {
                ExceptionlessTool.serverUrl = serverUrl;
            }

            lock (syncIsExecDefaultInit)
            {
                isExecDefaultInit = false;
            }
            DefaultInit(minLogLevel);

            if (isFilterUnrelatedLog)
            {
                SetIsFilterUnrelatedLog(isFilterUnrelatedLog);
            }
        }

        /// <summary>
        /// 默认初始化
        /// </summary>
        /// <param name="minLogLevel">最小日志等级,如果为null，则从配置里取</param>
        public static void DefaultInit(LogLevel minLogLevel = null)
        {
            if (isExecDefaultInit)
            {
                return;
            }

            ExceptionlessClient.Default.Configuration.ApiKey = ApiKey;
            ExceptionlessClient.Default.Configuration.ServerUrl = ServerUrl;

            if (minLogLevel == null)
            {
                var logLevel = LogLevelHelper.Parse(RecordLevel);
                switch (logLevel)
                {
                    case LogLevelEnum.TRACE:
                        minLogLevel = LogLevel.Trace;

                        break;

                    case LogLevelEnum.DEBUG:
                        minLogLevel = LogLevel.Debug;

                        break;

                    case LogLevelEnum.INFO:
                        minLogLevel = LogLevel.Info;

                        break;

                    case LogLevelEnum.WRAN:
                        minLogLevel = LogLevel.Warn;

                        break;

                    case LogLevelEnum.ERROR:
                        minLogLevel = LogLevel.Error;

                        break;

                    case LogLevelEnum.FATAL:
                        minLogLevel = LogLevel.Fatal;

                        break;

                    default:
                        minLogLevel = LogLevel.Off;

                        break;
                }
            }

            ExceptionlessClient.Default.Configuration.SetDefaultMinLogLevel(minLogLevel);

            lock (syncIsExecDefaultInit)
            {
                isExecDefaultInit = true;
            }
        }

        /// <summary>
        /// 设置是否过滤掉无关日志
        /// </summary>
        /// <param name="isFilterUnrelatedLog">是否过滤掉无关日志</param>
        public static void SetIsFilterUnrelatedLog(bool isFilterUnrelatedLog = true)
        {
            if (isFilterUnrelatedLog)
            {
                ExceptionlessClient.Default.SubmittingEvent += FilterUnrelatedSubmittingEvent;
            }
            else
            {
                ExceptionlessClient.Default.SubmittingEvent -= FilterUnrelatedSubmittingEvent;
            }
        }

        /// <summary>
        /// 过滤无关提交事件
        /// </summary>
        /// <param name="sender">引发对象</param>
        /// <param name="e">事件提交参数</param>
        private static void FilterUnrelatedSubmittingEvent(object sender, EventSubmittingEventArgs e)
        {
            // 仅处理未被处理过的异常
            if (e.IsUnhandledError)
            {
                // 忽略404事件
                if (e.Event.IsNotFound())
                {
                    e.Cancel = true;
                    return;
                }

                // 获取error对象
                var error = e.Event.GetError();
                if (error == null)
                {
                    return;
                }

                // 忽略 401 或 `HttpRequestValidationException`异常
                if (error.Code == "401" || error.Type == "System.Web.HttpRequestValidationException")
                {
                    e.Cancel = true;
                    return;
                }

                // 忽略不是指定命名空间代码抛出的异常
                var handledNamespaces = new List<string> { "Exceptionless" };
                if (!error.StackTrace.Select(s => s.DeclaringNamespace).Distinct().Any(ns => handledNamespaces.Any(ns.Contains)))
                {
                    e.Cancel = true;
                    return;
                }

                e.Event.MarkAsCritical();
            }
        }
    }
}
