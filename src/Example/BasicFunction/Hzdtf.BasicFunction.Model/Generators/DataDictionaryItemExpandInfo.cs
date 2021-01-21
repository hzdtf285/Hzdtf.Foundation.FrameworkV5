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
    /// 数据字典子项扩展信息
    /// @ 黄振东
    /// </summary>
    public partial class DataDictionaryItemExpandInfo : PersonTimeInfo<int>
    {
﻿        /// <summary>
        /// 数据字典子项Id_名称
        /// </summary>
		public const string DataDictionaryItemId_Name = "DataDictionaryItemId";

		/// <summary>
        /// 数据字典子项Id
        /// </summary>
        [JsonProperty("dataDictionaryItemId")]
        [MessagePack.Key("dataDictionaryItemId")]
        [Required]

        [DisplayName("数据字典子项Id")]
        [Display(Name = "数据字典子项Id", Order = 2, AutoGenerateField = true)]
        public int DataDictionaryItemId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 名称_名称
        /// </summary>
		public const string Name_Name = "Name";

		/// <summary>
        /// 名称
        /// </summary>
        [JsonProperty("name")]
        [MessagePack.Key("name")]
        [Required]
        [MaxLength(20)]

        [DisplayName("名称")]
        [Display(Name = "名称", Order = 9, AutoGenerateField = true)]
        public string Name
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
    }
}
