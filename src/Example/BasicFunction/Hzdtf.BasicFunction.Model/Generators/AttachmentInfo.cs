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
    /// 附件信息
    /// @ 黄振东
    /// </summary>
    public partial class AttachmentInfo : PersonTimeInfo<int>
    {
﻿        /// <summary>
        /// 文件名_名称
        /// </summary>
		public const string FileName_Name = "FileName";

		/// <summary>
        /// 文件名
        /// </summary>
        [JsonProperty("fileName")]
        [MessagePack.Key("fileName")]
        [Required]
        [MaxLength(50)]

        [DisplayName("文件名")]
        [Display(Name = "文件名", Order = 2, AutoGenerateField = true)]
        public string FileName
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 扩展名_名称
        /// </summary>
		public const string ExpandName_Name = "ExpandName";

		/// <summary>
        /// 扩展名
        /// </summary>
        [JsonProperty("expandName")]
        [MessagePack.Key("expandName")]
        [MaxLength(10)]

        [DisplayName("扩展名")]
        [Display(Name = "扩展名", Order = 3, AutoGenerateField = true)]
        public string ExpandName
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 标题_名称
        /// </summary>
		public const string Title_Name = "Title";

		/// <summary>
        /// 标题
        /// </summary>
        [JsonProperty("title")]
        [MessagePack.Key("title")]
        [MaxLength(50)]

        [DisplayName("标题")]
        [Display(Name = "标题", Order = 4, AutoGenerateField = true)]
        public string Title
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 文件地址_名称
        /// </summary>
		public const string FileAddress_Name = "FileAddress";

		/// <summary>
        /// 文件地址
        /// </summary>
        [JsonProperty("fileAddress")]
        [MessagePack.Key("fileAddress")]
        [Required]
        [MaxLength(500)]

        [DisplayName("文件地址")]
        [Display(Name = "文件地址", Order = 5, AutoGenerateField = true)]
        public string FileAddress
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 文件大小（KB）_名称
        /// </summary>
		public const string FileSize_Name = "FileSize";

		/// <summary>
        /// 文件大小（KB）
        /// </summary>
        [JsonProperty("fileSize")]
        [MessagePack.Key("fileSize")]
        [Required]

        [DisplayName("文件大小（KB）")]
        [Display(Name = "文件大小（KB）", Order = 6, AutoGenerateField = true)]
        public float FileSize
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 归属类型_名称
        /// </summary>
		public const string OwnerType_Name = "OwnerType";

		/// <summary>
        /// 归属类型
        /// </summary>
        [JsonProperty("ownerType")]
        [MessagePack.Key("ownerType")]
        [Required]

        [DisplayName("归属类型")]
        [Display(Name = "归属类型", Order = 7, AutoGenerateField = true)]
        public short OwnerType
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 归属ID_名称
        /// </summary>
		public const string OwnerId_Name = "OwnerId";

		/// <summary>
        /// 归属ID
        /// </summary>
        [JsonProperty("ownerId")]
        [MessagePack.Key("ownerId")]

        [DisplayName("归属ID")]
        [Display(Name = "归属ID", Order = 8, AutoGenerateField = true)]
        public int? OwnerId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 备注_名称
        /// </summary>
		public const string Memo_Name = "Memo";

		/// <summary>
        /// 备注
        /// </summary>
        [JsonProperty("memo")]
        [MessagePack.Key("memo")]
        [MaxLength(500)]

        [DisplayName("备注")]
        [Display(Name = "备注", Order = 9, AutoGenerateField = true)]
        public string Memo
        {
            get;
            set;
        }


    }
}
