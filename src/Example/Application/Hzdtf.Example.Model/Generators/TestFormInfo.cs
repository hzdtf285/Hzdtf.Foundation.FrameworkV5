﻿using Hzdtf.Utility.Model;
using Newtonsoft.Json;
using MessagePack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Hzdtf.Workflow.Model.Expand;

namespace Hzdtf.Example.Model
{
    /// <summary>
    /// 测试表单信息
    /// @ 黄振东
    /// </summary>
    public partial class TestFormInfo : ConcreteFormInfo
    {
        /// <summary>
        /// 编码_名称
        /// </summary>
        public const string Code_Name = "Code";

        /// <summary>
        /// 编码
        /// </summary>
        [JsonProperty("code")]
        [Required]
        [MaxLength(20)]

        [DisplayName("编码")]
        [Display(Name = "编码", Order = 2, AutoGenerateField = true)]
        [MessagePack.Key("code")]
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        /// 名称_名称
        /// </summary>
        public const string Name_Name = "Name";

        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty("name")]
        [Required]
        [MaxLength(20)]

        [DisplayName("名称")]
        [Display(Name = "名称", Order = 11, AutoGenerateField = true)]
        [MessagePack.Key("name")]
        public string Name
        {
            get;
            set;
        }

    }
}
