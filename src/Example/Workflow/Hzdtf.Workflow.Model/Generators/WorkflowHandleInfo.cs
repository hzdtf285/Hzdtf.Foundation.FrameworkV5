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
    /// 工作流处理信息
    /// @ 黄振东
    /// </summary>
    public partial class WorkflowHandleInfo : PersonTimeInfo<int>
    {
﻿        /// <summary>
        /// 工作流Id_名称
        /// </summary>
		public const string WorkflowId_Name = "WorkflowId";

		/// <summary>
        /// 工作流Id
        /// </summary>
        [JsonProperty("workflowId")]
        [MessagePack.Key("workflowId")]
        [Required]

        [DisplayName("工作流Id")]
        [Display(Name = "工作流Id", Order = 2, AutoGenerateField = true)]
        public int WorkflowId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 流程关卡ID_名称
        /// </summary>
		public const string FlowCensorshipId_Name = "FlowCensorshipId";

		/// <summary>
        /// 流程关卡ID
        /// </summary>
        [JsonProperty("flowCensorshipId")]
        [MessagePack.Key("flowCensorshipId")]
        [Required]

        [DisplayName("流程关卡ID")]
        [Display(Name = "流程关卡ID", Order = 3, AutoGenerateField = true)]
        public int FlowCensorshipId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 处理人ID_名称
        /// </summary>
		public const string HandlerId_Name = "HandlerId";

		/// <summary>
        /// 处理人ID
        /// </summary>
        [JsonProperty("handlerId")]
        [MessagePack.Key("handlerId")]
        [Required]

        [DisplayName("处理人ID")]
        [Display(Name = "处理人ID", Order = 10, AutoGenerateField = true)]
        public int HandlerId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 处理人_名称
        /// </summary>
		public const string Handler_Name = "Handler";

		/// <summary>
        /// 处理人
        /// </summary>
        [JsonProperty("handler")]
        [MessagePack.Key("handler")]
        [Required]
        [MaxLength(20)]

        [DisplayName("处理人")]
        [Display(Name = "处理人", Order = 11, AutoGenerateField = true)]
        public string Handler
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 处理时间_名称
        /// </summary>
		public const string HandleTime_Name = "HandleTime";

		/// <summary>
        /// 处理时间
        /// </summary>
        [JsonProperty("handleTime")]
        [MessagePack.Key("handleTime")]

        [DisplayName("处理时间")]
        [Display(Name = "处理时间", Order = 12, AutoGenerateField = true)]
        public DateTime? HandleTime
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 处理状态_名称
        /// </summary>
		public const string HandleStatus_Name = "HandleStatus";

		/// <summary>
        /// 处理状态
        /// </summary>
        [JsonProperty("handleStatus")]
        [MessagePack.Key("handleStatus")]
        [Required]

        [DisplayName("处理状态")]
        [Display(Name = "处理状态", Order = 13, AutoGenerateField = true)]
        public HandleStatusEnum HandleStatus
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 是否已读_名称
        /// </summary>
		public const string IsReaded_Name = "IsReaded";

		/// <summary>
        /// 是否已读
        /// </summary>
        [JsonProperty("isReaded")]
        [MessagePack.Key("isReaded")]
        [Required]

        [DisplayName("是否已读")]
        [Display(Name = "是否已读", Order = 14, AutoGenerateField = true)]
        public bool IsReaded
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 意见_名称
        /// </summary>
		public const string Idea_Name = "Idea";

		/// <summary>
        /// 意见
        /// </summary>
        [JsonProperty("idea")]
        [MessagePack.Key("idea")]
        [MaxLength(200)]

        [DisplayName("意见")]
        [Display(Name = "意见", Order = 15, AutoGenerateField = true)]
        public string Idea
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 处理类型_名称
        /// </summary>
		public const string HandleType_Name = "HandleType";

		/// <summary>
        /// 处理类型
        /// </summary>
        [JsonProperty("handleType")]
        [MessagePack.Key("handleType")]
        [Required]

        [DisplayName("处理类型")]
        [Display(Name = "处理类型", Order = 16, AutoGenerateField = true)]
        public HandleTypeEnum HandleType
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 具体关卡ID_名称
        /// </summary>
		public const string ConcreteConcreteId_Name = "ConcreteConcreteId";

		/// <summary>
        /// 具体关卡ID
        /// </summary>
        [JsonProperty("concreteConcreteId")]
        [MessagePack.Key("concreteConcreteId")]
        [Required]

        [DisplayName("具体关卡ID")]
        [Display(Name = "具体关卡ID", Order = 17, AutoGenerateField = true)]
        public int ConcreteConcreteId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 具体关卡_名称
        /// </summary>
		public const string ConcreteConcrete_Name = "ConcreteConcrete";

		/// <summary>
        /// 具体关卡
        /// </summary>
        [JsonProperty("concreteConcrete")]
        [MessagePack.Key("concreteConcrete")]
        [Required]
        [MaxLength(20)]

        [DisplayName("具体关卡")]
        [Display(Name = "具体关卡", Order = 18, AutoGenerateField = true)]
        public string ConcreteConcrete
        {
            get;
            set;
        }
    }
}
