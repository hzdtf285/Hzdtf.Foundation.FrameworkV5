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
    /// 用户角色信息
    /// @ 黄振东
    /// </summary>
    public partial class UserRoleInfo : PersonTimeInfo<int>
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
        [Display(Name = "用户Id", Order = 3, AutoGenerateField = true)]
        public int UserId
        {
            get;
            set;
        }


    }
}
