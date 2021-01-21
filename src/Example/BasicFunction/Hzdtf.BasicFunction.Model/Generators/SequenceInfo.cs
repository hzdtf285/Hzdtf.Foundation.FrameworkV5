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
    /// 序列信息
    /// @ 黄振东
    /// </summary>
    public partial class SequenceInfo : PersonTimeInfo<int>
    {
﻿        /// <summary>
        /// 序列类型_名称
        /// </summary>
		public const string SeqType_Name = "SeqType";

		/// <summary>
        /// 序列类型
        /// </summary>
        [JsonProperty("seqType")]
        [MessagePack.Key("seqType")]
        [Required]
        [MaxLength(2)]

        [DisplayName("序列类型")]
        [Display(Name = "序列类型", Order = 2, AutoGenerateField = true)]
        public string SeqType
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 更新日期_名称
        /// </summary>
		public const string UpdateDate_Name = "UpdateDate";

		/// <summary>
        /// 更新日期
        /// </summary>
        [JsonProperty("updateDate")]
        [MessagePack.Key("updateDate")]
        [Required]

        [DisplayName("更新日期")]
        [Display(Name = "更新日期", Order = 3, AutoGenerateField = true)]
        public DateTime UpdateDate
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 增量_名称
        /// </summary>
		public const string Increment_Name = "Increment";

		/// <summary>
        /// 增量
        /// </summary>
        [JsonProperty("increment")]
        [MessagePack.Key("increment")]
        [Required]

        [DisplayName("增量")]
        [Display(Name = "增量", Order = 4, AutoGenerateField = true)]
        public int Increment
        {
            get;
            set;
        }


    }
}
