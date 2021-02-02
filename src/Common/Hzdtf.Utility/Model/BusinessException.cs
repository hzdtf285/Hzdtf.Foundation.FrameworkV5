using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.Model
{
    /// <summary>
    /// 业务异常
    /// 初步定义：
    /// 100-9999为平台编码
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class BusinessException : Exception
    {
        /// <summary>
        /// 编码
        /// </summary>
        private readonly int code;

        /// <summary>
        /// 编码
        /// </summary>
        [JsonProperty("code")]
        [Key("code")]
        public int Code
        {
            get => code;
        }

        /// <summary>
        /// 消息
        /// </summary>
        private readonly string msg;

        /// <summary>
        /// 消息
        /// </summary>
        [JsonProperty("msg")]
        [Key("msg")]
        public string Msg
        {
            get => msg;
        }

        /// <summary>
        /// 消息
        /// </summary>
        private readonly string desc;

        /// <summary>
        /// 描述
        /// </summary>
        [JsonProperty("desc")]
        [Key("desc")]
        public string Desc
        {
            get => desc;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="msg">消息</param>
        /// <param name="desc">描述</param>
        public BusinessException(int code, string msg = null, string desc = null)
        {
            this.code = code;
            this.msg = msg;
            this.desc = desc;
        }
    }
}
