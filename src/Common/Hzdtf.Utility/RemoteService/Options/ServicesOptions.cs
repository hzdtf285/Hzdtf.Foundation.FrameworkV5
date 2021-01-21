﻿using Hzdtf.Utility.LoadBalance;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Utils;
using Hzdtf.Utility.RemoteService.Builder;
using Newtonsoft.Json;
using MessagePack;

namespace Hzdtf.Utility.RemoteService.Options
{
    /// <summary>
    /// 服务基本选项配置
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class ServicesBasicOptions
    {
        /// <summary>
        /// 负载均衡模式
        /// </summary>
        [JsonProperty("loadBalanceMode")]
        [MessagePack.Key("loadBalanceMode")]
        public LoadBalanceMode? LoadBalanceMode
        {
            get;
            set;
        }

        /// <summary>
        /// 方案
        /// </summary>
        [JsonProperty("sheme")]
        [MessagePack.Key("sheme")]
        public string Sheme
        {
            get;
            set;
        }

        /// <summary>
        /// 标签
        /// </summary>
        [JsonProperty("tag")]
        [MessagePack.Key("tag")]
        public string Tag
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 服务选项配置
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class ServicesOptions : ServicesBasicOptions
    {
        /// <summary>
        /// 服务名
        /// </summary>
        [JsonProperty("serviceName")]
        [MessagePack.Key("serviceName")]
        public string ServiceName
        {
            get;
            set;
        }

        /// <summary>
        /// 负载均衡
        /// </summary>
        [JsonIgnore]
        [IgnoreMember]
        public ILoadBalance LoadBalance
        {
            get;
            set;
        }

        /// <summary>
        /// 服务生成器
        /// </summary>
        [JsonIgnore]
        [IgnoreMember]
        public IServicesBuilder ServicesBuilder
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 全局服务配置选项
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class GlobalServicesOptions : ServicesBasicOptions
    {
    }

    /// <summary>
    /// 统一服务选项配置
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class UnityServicesOptions
    {
        /// <summary>
        /// 服务配置数组
        /// </summary>
        [JsonProperty("services")]
        [MessagePack.Key("services")]
        public ServicesOptions[] Services
        {
            get;
            set;
        }

        /// <summary>
        /// 全局配置
        /// </summary>
        [JsonProperty("globalConfiguration")]
        [MessagePack.Key("globalConfiguration")]
        public GlobalServicesOptions GlobalConfiguration
        {
            get;
            set;
        } = new GlobalServicesOptions();

        /// <summary>
        /// 重置，如果Services没有配置而GlobalConfiguration有配置，则用GlobalConfiguration代替。并设置默认值
        /// </summary>
        public void Reset()
        {
            if (Services.IsNullOrLength0())
            {
                return;
            }

            foreach (var ser in Services)
            {
                if (ser.LoadBalanceMode == null && GlobalConfiguration.LoadBalanceMode != null)
                {
                    ser.LoadBalanceMode = GlobalConfiguration.LoadBalanceMode;
                }
                if (ser.Sheme == null && GlobalConfiguration.Sheme != null)
                {
                    ser.Sheme = GlobalConfiguration.Sheme;
                }
                if (ser.Tag == null && GlobalConfiguration.Tag != null)
                {
                    ser.Tag = GlobalConfiguration.Tag;
                }
                
                if (ser.LoadBalanceMode == null)
                {
                    ser.LoadBalanceMode = LoadBalanceMode.RANDOM;
                }
                if (string.IsNullOrWhiteSpace(ser.Sheme))
                {
                    ser.Sheme = Uri.UriSchemeHttp;
                }

                ser.LoadBalance = LoadBalanceSimpleFactory.Create((LoadBalance.LoadBalanceMode)ser.LoadBalanceMode);
                if (ser.LoadBalance == null)
                {
                    throw new KeyNotFoundException($"服务名:{ser.ServiceName},模式:{ser.LoadBalanceMode}.未找到负载均衡");
                }
            }
        }
    }
}
