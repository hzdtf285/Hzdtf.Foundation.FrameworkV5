using Hzdtf.Utility.Model;
using Newtonsoft.Json;
using MessagePack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.BasicFunction.Model
{
    /// <summary>
    /// 数据字典子项信息
    /// @ 黄振东
    /// </summary>
    public partial class DataDictionaryItemInfo : PersonTimeInfo<int>
    {
﻿        /// <summary>
        /// 编码_名称
        /// </summary>
		public const string Code_Name = "Code";

		/// <summary>
        /// 编码
        /// </summary>
        [JsonProperty("code")]
        [MessagePack.Key("code")]
        [MaxLength(20)]

        [DisplayName("编码")]
        [Display(Name = "编码", Order = 2, AutoGenerateField = true)]
        public string Code
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 数据字典Id_名称
        /// </summary>
		public const string DataDictionaryId_Name = "DataDictionaryId";

		/// <summary>
        /// 数据字典Id
        /// </summary>
        [JsonProperty("dataDictionaryId")]
        [MessagePack.Key("dataDictionaryId")]
        [Required]

        [DisplayName("数据字典Id")]
        [Display(Name = "数据字典Id", Order = 3, AutoGenerateField = true)]
        public int DataDictionaryId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 文本_名称
        /// </summary>
		public const string Text_Name = "Text";

		/// <summary>
        /// 文本
        /// </summary>
        [JsonProperty("text")]
        [MessagePack.Key("text")]
        [Required]
        [MaxLength(20)]

        [DisplayName("文本")]
        [Display(Name = "文本", Order = 10, AutoGenerateField = true)]
        public string Text
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 系统内置_名称
        /// </summary>
		public const string SystemInlay_Name = "SystemInlay";

		/// <summary>
        /// 系统内置
        /// </summary>
        [JsonProperty("systemInlay")]
        [MessagePack.Key("systemInlay")]
        [Required]

        [DisplayName("系统内置")]
        [Display(Name = "系统内置", Order = 11, AutoGenerateField = true)]
        public bool SystemInlay
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 扩展表_名称
        /// </summary>
		public const string ExpandTable_Name = "ExpandTable";

		/// <summary>
        /// 扩展表
        /// </summary>
        [JsonProperty("expandTable")]
        [MessagePack.Key("expandTable")]
        [MaxLength(100)]

        [DisplayName("扩展表")]
        [Display(Name = "扩展表", Order = 12, AutoGenerateField = true)]
        public string ExpandTable
        {
            get;
            set;
        }
    }
}
