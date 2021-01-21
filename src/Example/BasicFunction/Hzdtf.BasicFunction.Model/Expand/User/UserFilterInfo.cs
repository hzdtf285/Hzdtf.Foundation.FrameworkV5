using Hzdtf.Utility.Model;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Model.Expand.User
{
    /// <summary>
    /// 用户筛选信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class UserFilterInfo : KeywordFilterInfo
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [JsonProperty("roleId")]
        [MessagePack.Key("roleId")]
        public int? RoleId
        {
            get;
            set;
        }

        /// <summary>
        /// 启用
        /// </summary>
        [JsonProperty("enabled")]
        [MessagePack.Key("enabled")]
        public bool? Enabled
        {
            get;
            set;
        }
    }
}
