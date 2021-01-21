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
    /// 送件流程路线信息
    /// @ 黄振东
    /// </summary>
    public partial class SendFlowRouteInfo : PersonTimeInfo<int>
    {
﻿        /// <summary>
        /// 流程关卡Id_名称
        /// </summary>
		public const string FlowCensorshipId_Name = "FlowCensorshipId";

		/// <summary>
        /// 流程关卡Id
        /// </summary>
        [JsonProperty("flowCensorshipId")]
        [MessagePack.Key("flowCensorshipId")]
        [Required]

        [DisplayName("流程关卡Id")]
        [Display(Name = "流程关卡Id", Order = 2, AutoGenerateField = true)]
        public int FlowCensorshipId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 流转到流程关卡ID_名称
        /// </summary>
		public const string ToFlowCensorshipId_Name = "ToFlowCensorshipId";

		/// <summary>
        /// 流转到流程关卡ID
        /// </summary>
        [JsonProperty("toFlowCensorshipId")]
        [MessagePack.Key("toFlowCensorshipId")]
        [Required]

        [DisplayName("流转到流程关卡ID")]
        [Display(Name = "流转到流程关卡ID", Order = 3, AutoGenerateField = true)]
        public int ToFlowCensorshipId
        {
            get;
            set;
        }


    }
}
