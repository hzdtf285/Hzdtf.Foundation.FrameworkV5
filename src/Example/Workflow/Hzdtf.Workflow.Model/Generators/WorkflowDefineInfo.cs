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
    /// 工作流定义信息
    /// @ 黄振东
    /// </summary>
    public partial class WorkflowDefineInfo : PersonTimeInfo<int>
    {
﻿        /// <summary>
        /// 流程Id_名称
        /// </summary>
		public const string FlowId_Name = "FlowId";

		/// <summary>
        /// 流程Id
        /// </summary>
        [JsonProperty("flowId")]
        [MessagePack.Key("flowId")]
        [Required]

        [DisplayName("流程Id")]
        [Display(Name = "流程Id", Order = 8, AutoGenerateField = true)]
        public int FlowId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 表单Id_名称
        /// </summary>
		public const string FormId_Name = "FormId";

		/// <summary>
        /// 表单Id
        /// </summary>
        [JsonProperty("formId")]
        [MessagePack.Key("formId")]
        [Required]

        [DisplayName("表单Id")]
        [Display(Name = "表单Id", Order = 9, AutoGenerateField = true)]
        public int FormId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 编码_名称
        /// </summary>
		public const string Code_Name = "Code";

		/// <summary>
        /// 编码
        /// </summary>
        [JsonProperty("code")]
        [MessagePack.Key("code")]
        [Required]
        [MaxLength(20)]

        [DisplayName("编码")]
        [Display(Name = "编码", Order = 10, AutoGenerateField = true)]
        public string Code
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
        [Display(Name = "名称", Order = 11, AutoGenerateField = true)]
        public string Name
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 启用_名称
        /// </summary>
		public const string Enabled_Name = "Enabled";

		/// <summary>
        /// 启用
        /// </summary>
        [JsonProperty("enabled")]
        [MessagePack.Key("enabled")]
        [Required]

        [DisplayName("启用")]
        [Display(Name = "启用", Order = 12, AutoGenerateField = true)]
        public bool Enabled
        {
            get;
            set;
        }
    }
}
