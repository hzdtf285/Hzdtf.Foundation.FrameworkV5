using Hzdtf.Utility.Model;
using Newtonsoft.Json;
using MessagePack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.Workflow.Model
{
    /// <summary>
    /// 表单信息
    /// @ 黄振东
    /// </summary>
    public partial class FormInfo : PersonTimeInfo<int>
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
        [Required]
        [MaxLength(20)]

        [DisplayName("名称")]
        [Display(Name = "名称", Order = 2, AutoGenerateField = true)]
        public string Name
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 表单URL_名称
        /// </summary>
		public const string FormUrl_Name = "FormUrl";

		/// <summary>
        /// 表单URL
        /// </summary>
        [JsonProperty("formUrl")]
        [MessagePack.Key("formUrl")]
        [Required]
        [MaxLength(200)]

        [DisplayName("表单URL")]
        [Display(Name = "表单URL", Order = 3, AutoGenerateField = true)]
        public string FormUrl
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 表单获取明细URL_名称
        /// </summary>
		public const string FormGetDetailUrl_Name = "FormGetDetailUrl";

		/// <summary>
        /// 表单获取明细URL
        /// </summary>
        [JsonProperty("formGetDetailUrl")]
        [MessagePack.Key("formGetDetailUrl")]
        [Required]
        [MaxLength(200)]

        [DisplayName("表单获取明细URL")]
        [Display(Name = "表单获取明细URL", Order = 4, AutoGenerateField = true)]
        public string FormGetDetailUrl
        {
            get;
            set;
        }


    }
}
