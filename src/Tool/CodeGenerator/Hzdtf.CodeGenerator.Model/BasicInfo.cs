using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.CodeGenerator.Model
{
    /// <summary>
    /// 基本信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class BasicInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty("name")]
        [MessagePack.Key("name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        [JsonProperty("description")]
        [MessagePack.Key("description")]
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// 是否分页，默认为是
        /// </summary>
        [JsonProperty("isPage")]
        [MessagePack.Key("isPage")]
        public bool IsPage
        {
            get;
            set;
        } = true;
    }
}
