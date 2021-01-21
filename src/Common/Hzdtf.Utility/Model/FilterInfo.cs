using Hzdtf.Utility.Enums;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Model
{
    /// <summary>
    /// 筛选信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class FilterInfo
    {
        /// <summary>
        /// 开始创建时间
        /// </summary>
        [JsonProperty("startCreateTime")]
        [MessagePack.Key("startCreateTime")]
        public DateTime? StartCreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 结束创建时间
        /// </summary>
        [JsonProperty("endCreateTime")]
        [MessagePack.Key("endCreateTime")]
        public DateTime? EndCreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 排序
        /// </summary>
        [JsonProperty("sort")]
        [MessagePack.Key("sort")]
        public SortType Sort
        {
            get;
            set;
        }

        /// <summary>
        /// 排序名称
        /// </summary>
        [JsonProperty("sortName")]
        [MessagePack.Key("sortName")]
        public string SortName
        {
            get;
            set;
        }
    }
}
