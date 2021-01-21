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
    /// 角色菜单功能信息
    /// @ 黄振东
    /// </summary>
    public partial class RoleMenuFunctionInfo : PersonTimeInfo<int>
    {
﻿        /// <summary>
        /// 角色Id_名称
        /// </summary>
		public const string RoleId_Name = "RoleId";

		/// <summary>
        /// 角色Id
        /// </summary>
        [JsonProperty("roleId")]
        [MessagePack.Key("roleId")]
        [Required]

        [DisplayName("角色Id")]
        [Display(Name = "角色Id", Order = 2, AutoGenerateField = true)]
        public int RoleId
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
