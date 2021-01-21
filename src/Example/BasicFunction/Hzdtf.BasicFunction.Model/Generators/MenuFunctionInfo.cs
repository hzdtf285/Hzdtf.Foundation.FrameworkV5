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
    /// 菜单功能信息
    /// @ 黄振东
    /// </summary>
    public partial class MenuFunctionInfo : PersonTimeInfo<int>
    {
﻿        /// <summary>
        /// 菜单Id_名称
        /// </summary>
		public const string MenuId_Name = "MenuId";

		/// <summary>
        /// 菜单Id
        /// </summary>
        [JsonProperty("menuId")]
        [MessagePack.Key("menuId")]
        [Required]

        [DisplayName("菜单Id")]
        [Display(Name = "菜单Id", Order = 2, AutoGenerateField = true)]
        public int MenuId
        {
            get;
            set;
        }

﻿        /// <summary>
        /// 功能Id_名称
        /// </summary>
		public const string FunctionId_Name = "FunctionId";

		/// <summary>
        /// 功能Id
        /// </summary>
        [JsonProperty("functionId")]
        [MessagePack.Key("functionId")]
        [Required]

        [DisplayName("功能Id")]
        [Display(Name = "功能Id", Order = 3, AutoGenerateField = true)]
        public int FunctionId
        {
            get;
            set;
        }


    }
}
