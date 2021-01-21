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
    /// 用户菜单功能信息
    /// @ 黄振东
    /// </summary>
    public partial class UserMenuFunctionInfo : PersonTimeInfo<int>
    {
﻿        /// <summary>
        /// 用户Id_名称
        /// </summary>
		public const string UserId_Name = "UserId";

		/// <summary>
        /// 用户Id
        /// </summary>
        [JsonProperty("userId")]
        [MessagePack.Key("userId")]
        [Required]

        [DisplayName("用户Id")]
        [Display(Name = "用户Id", Order = 2, AutoGenerateField = true)]
        public int UserId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 菜单功能Id_名称
        /// </summary>
		public const string MenuFunctionId_Name = "MenuFunctionId";

		/// <summary>
        /// 菜单功能Id
        /// </summary>
        [JsonProperty("menuFunctionId")]
        [MessagePack.Key("menuFunctionId")]
        [Required]

        [DisplayName("菜单功能Id")]
        [Display(Name = "菜单功能Id", Order = 3, AutoGenerateField = true)]
        public int MenuFunctionId
        {
            get;
            set;
        }


    }
}
