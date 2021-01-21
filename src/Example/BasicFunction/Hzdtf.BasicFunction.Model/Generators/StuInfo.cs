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
    /// 测试表注释信息
    /// @ 黄振东
    /// </summary>
    public partial class StuInfo : PersonTimeInfo<int>
    {
﻿        /// <summary>
        /// 名称_名称
        /// </summary>
		public const string Name_Name = "Name";

		/// <summary>
        /// 名称
        /// </summary>
        [JsonProperty("name")]
        [MessagePack.Key("name")]
        [MaxLength(50)]

        [DisplayName("名称")]
        [Display(Name = "名称", Order = 8, AutoGenerateField = true)]
        public string Name
        {
            get;
            set;
        }
    }
}
