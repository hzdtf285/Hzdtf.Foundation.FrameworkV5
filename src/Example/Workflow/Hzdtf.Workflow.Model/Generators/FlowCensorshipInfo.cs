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
    /// 流程关卡信息
    /// @ 黄振东
    /// </summary>
    public partial class FlowCensorshipInfo : PersonTimeInfo<int>
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
        [Display(Name = "流程Id", Order = 2, AutoGenerateField = true)]
        public int FlowId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 归属关卡类型_名称
        /// </summary>
		public const string OwnerCensorshipType_Name = "OwnerCensorshipType";

		/// <summary>
        /// 归属关卡类型
        /// </summary>
        [JsonProperty("ownerCensorshipType")]
        [MessagePack.Key("ownerCensorshipType")]
        [Required]

        [DisplayName("归属关卡类型")]
        [Display(Name = "归属关卡类型", Order = 9, AutoGenerateField = true)]
        public CensorshipTypeEnum OwnerCensorshipType
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 归属关卡ID_名称
        /// </summary>
		public const string OwnerCensorshipId_Name = "OwnerCensorshipId";

		/// <summary>
        /// 归属关卡ID
        /// </summary>
        [JsonProperty("ownerCensorshipId")]
        [MessagePack.Key("ownerCensorshipId")]
        [Required]

        [DisplayName("归属关卡ID")]
        [Display(Name = "归属关卡ID", Order = 10, AutoGenerateField = true)]
        public int OwnerCensorshipId
        {
            get;
            set;
        }
    }
}
