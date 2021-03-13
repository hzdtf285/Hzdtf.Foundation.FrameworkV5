using Hzdtf.Utility.Enums;
using Hzdtf.Utility.Safety;
using Hzdtf.Utility.TheOperation;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Hzdtf.Utility
{
    /// <summary>
    /// 应用类
    /// @ 黄振东
    /// </summary>
    public static class App
    {
        /// <summary>
        /// 当前配置，通常在程序启动时设置，是一个全局配置
        /// </summary>
        public static IConfiguration CurrConfig
        {
            get;
            set;
        }

        /// <summary>
        /// 最大每页记录数
        /// -1表示不限制，默认为-1
        /// </summary>
        public static int MaxPageSize
        {
            get;
            set;
        } = -1;

        /// <summary>
        /// 同步当前应用程序名称
        /// </summary>
        private static readonly object syncCurrApplicationName = new object();

        /// <summary>
        /// 当前应用程序名称
        /// </summary>
        private static string currApplicationName = null;

        /// <summary>
        /// 当前应用程序名称
        /// </summary>
        public static string CurrApplicationName
        {
            get => currApplicationName;
            set
            {
                lock (syncCurrApplicationName)
                {
                    currApplicationName = value;
                }
            }
        }

        /// <summary>
        /// 同步服务名
        /// </summary>
        private static readonly object syncCurrServiceName = new object();

        /// <summary>
        /// 当前服务名
        /// </summary>
        private static string currServiceName = null;

        /// <summary>
        /// 当前服务名
        /// </summary>
        public static string CurrServiceName
        {
            get => currServiceName;
            set
            {
                lock (syncCurrServiceName)
                {
                    currServiceName = value;
                }
            }
        }

        /// <summary>
        /// 应用服务名
        /// 如果当前服务名不为空，则取当前服务名，否则取当前应用程序名
        /// </summary>
        public static string AppServiceName
        {
            get => string.IsNullOrWhiteSpace(currServiceName) ? currApplicationName : currServiceName;
        }

        /// <summary>
        /// 当前环境类型
        /// </summary>
        public static EnvironmentType CurrEnvironmentType
        {
            get => GetCurrEnvironmentTypeFunc == null ? EnvironmentType.PRODUCTION : GetCurrEnvironmentTypeFunc();
        }

        /// <summary>
        /// 获取当前环境类型回调
        /// </summary>
        public static Func<EnvironmentType> GetCurrEnvironmentTypeFunc;

        /// <summary>
        /// 连接加密
        /// </summary>
        public static bool ConnectionEncryption
        {
            get
            {
                return string.IsNullOrWhiteSpace(CurrConfig["ConnectionStrings:Encrypt"]) ? false : Convert.ToBoolean(CurrConfig["ConnectionStrings:Encrypt"]);
            }
        }

        /// <summary>
        /// 默认连接字符串
        /// </summary>
        public static string DefaultConnectionString { get => GetConnectionString("DefaultConnection"); }

        /// <summary>
        /// 从库连接字符串
        /// </summary>
        public static string SlaveConnectionString { get => GetConnectionString("SlaveConnection"); }

        /// <summary>
        /// 测试默认连接字符串
        /// </summary>
        public static string TestDefaultConnectionString { get => GetConnectionString("TestDefaultConnection"); }

        /// <summary>
        /// 测试从库连接字符串
        /// </summary>
        public static string TestSlaveConnectionString { get => GetConnectionString("TestSlaveConnection"); }

        /// <summary>
        /// 根据名称获取直通连接字符串
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>连接字符串</returns>
        public static string GetConnectionString(string name) => FilterConnectionString(GetDirectConnectionString(name));

        /// <summary>
        /// 默认上传图片的扩展名集合
        /// </summary>
        private static string[] defaultUploadImageExpands;

        /// <summary>
        /// 同步默认上传图片的扩展名集合
        /// </summary>
        private static readonly object syncAllowUploadImageExpands = new object();

        /// <summary>
        /// 默认上传图片的扩展名集合
        /// </summary>
        public static string[] AllowUploadImageExpands
        {
            get
            {
                if (defaultUploadImageExpands == null)
                {
                    string str = CurrConfig["Image:AllowUploadExpands"];
                    if (!string.IsNullOrWhiteSpace(str))
                    {
                        var temp = str.Split(',');
                        lock (syncAllowUploadImageExpands)
                        {
                            defaultUploadImageExpands = temp;
                        }
                    }
                }

                return defaultUploadImageExpands;
            }
        }

        /// <summary>
        /// 过滤连接字符串
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>过滤后的连接字符串</returns>
        public static string FilterConnectionString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                return connectionString;
            }

            return ConnectionEncryption ? DESUtil.Decrypt(connectionString) : connectionString;
        }

        /// <summary>
        /// 根据名称获取直通连接字符串
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>连接字符串</returns>
        public static string GetDirectConnectionString(string name) => CurrConfig.GetConnectionString(name);

        /// <summary>
        /// 默认文化
        /// </summary>
        private static string defaultCulture;

        /// <summary>
        /// 同步默认文化
        /// </summary>
        private readonly static object syncDefaultCulture = new object();

        /// <summary>
        /// 默认文化，默认值取当前线程
        /// </summary>
        public static string DefaultCulture
        {
            get
            {
                if (defaultCulture == null)
                {
                    lock (syncDefaultCulture)
                    {
                        defaultCulture = Thread.CurrentThread.CurrentCulture.Name;
                    }
                }

                return defaultCulture;
            }
            set
            {
                lock (syncDefaultCulture)
                {
                    defaultCulture = value;
                }
            }
        }

        /// <summary>
        /// 返回对象是否返回文化，默认为否，如果要返回，则程序启动时需要设置
        /// </summary>
        public static bool IsReturnCulture { get; set; }

        /// <summary>
        /// 事件ID键
        /// </summary>
        public const string EVENT_ID_KEY = "EventId";

        /// <summary>
        /// 获取token回调
        /// </summary>
        public static Func<string> GetTokenFunc;

        /// <summary>
        /// 获取事件ID回调
        /// </summary>
        public static Func<string> GetEventIdFunc;

        /// <summary>
        /// 本次操作
        /// </summary>
        public static ITheOperation TheOperation;
    }
}
