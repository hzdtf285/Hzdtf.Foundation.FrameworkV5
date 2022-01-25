using Hzdtf.Utility.Utils;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Quartz.Model
{
    /// <summary>
    /// 作业异常信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class JobExceptionInfo
    {
        /// <summary>
        /// 事务ID
        /// </summary>
        [Key("transId")]
        [JsonProperty("transId")]
        public long TransId
        {
            get;
            set;
        }

        /// <summary>
        /// 作业组
        /// </summary>
        [Key("jobGroup")]
        [JsonProperty("jobGroup")]
        public string JobGroup
        {
            get;
            set;
        }

        /// <summary>
        /// 作业名称
        /// </summary>
        [Key("jobName")]
        [JsonProperty("jobName")]
        public string JobName
        {
            get;
            set;
        }

        /// <summary>
        /// 服务名
        /// </summary>
        [Key("serviceName")]
        [JsonProperty("serviceName")]
        public string ServiceName
        {
            get;
            set;
        }

        /// <summary>
        /// 异常消息
        /// </summary>
        [Key("exMsg")]
        [JsonProperty("exMsg")]
        public string ExMsg
        {
            get;
            set;
        }

        /// <summary>
        /// 异常堆栈
        /// </summary>
        [Key("exStackTrace")]
        [JsonProperty("exStackTrace")]
        public string ExStackTrace
        {
            get;
            set;
        }

        /// <summary>
        /// 作业全路径
        /// </summary>
        [Key("jobFullClass")]
        [JsonProperty("jobFullClass")]
        public string JobFullClass
        {
            get;
            set;
        }

        /// <summary>
        /// 服务IP
        /// </summary>
        [Key("serverIp")]
        [JsonProperty("serverIp")]
        public string ServerIp
        {
            get;
            set;
        } = NetworkUtil.LocalIP;

        /// <summary>
        /// 时间
        /// </summary>
        [Key("time")]
        [JsonProperty("time")]
        public DateTime Time
        {
            get;
            set;
        } = DateTime.Now;

        /// <summary>
        /// 异常
        /// </summary>
        [JsonIgnore]
        [IgnoreMember]
        public Exception Ex
        {
            get;
            set;
        }
    }
}
