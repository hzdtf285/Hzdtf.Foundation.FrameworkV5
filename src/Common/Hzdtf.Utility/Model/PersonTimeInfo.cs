using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.Utility.Model
{
    /// <summary>
    /// 带有人时间信息
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    [MessagePackObject]
    public class PersonTimeInfo<IdT> : TimeInfo<IdT>
    {
        /// <summary>
        /// 创建人ID_名称
        /// </summary>
        public const string CreaterId_Name = "CreaterId";

        /// <summary>
        /// 创建人ID
        /// </summary>
        [JsonProperty("createrId")]
        [Display(AutoGenerateField = false)]
        [MessagePack.Key("createrId")]
        public IdT CreaterId
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人_名称
        /// </summary>
        public const string Creater_Name = "Creater";

        /// <summary>
        /// 创建人
        /// </summary>
        [JsonProperty("creater")]
        [Display(AutoGenerateField = false)]
        [MessagePack.Key("creater")]
        public string Creater
        {
            get;
            set;
        }

        /// <summary>
        /// 修改人ID_名称
        /// </summary>
        public const string ModifierId_Name = "ModifierId";

        /// <summary>
        /// 修改人ID
        /// </summary>
        [JsonProperty("modifierId")]
        [Display(AutoGenerateField = false)]
        [MessagePack.Key("modifierId")]
        public IdT ModifierId
        {
            get;
            set;
        }

        /// <summary>
        /// 修改人_名称
        /// </summary>
        public const string Modifier_Name = "Modifier";

        /// <summary>
        /// 修改人
        /// </summary>
        [JsonProperty("modifier")]
        [Display(AutoGenerateField = false)]
        [MessagePack.Key("modifier")]
        public string Modifier
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 带有人时间信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class PersonTimeInfo : PersonTimeInfo<int>
    {
    }

    /// <summary>
    /// 带有人时间租户信息
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    public class PersonTimeTenantInfo<IdT> : PersonTimeInfo<IdT>
    {
        /// <summary>
        /// 租户ID_名称
        /// </summary>
        public const string TenantId_Name = "TenantId";

        /// <summary>
        /// 租户ID
        /// </summary>
        [JsonProperty("tenantId")]
        [Display(Name = "租户ID", Order = 10, AutoGenerateField = false)]
        [MessagePack.Key("tenantId")]
        public IdT TenantId
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 带人和时间信息扩展类
    /// @ 黄振东
    /// </summary>
    public static class SimpleInfoExtensions
    {
        /// <summary>
        /// 设置创建信息
        /// </summary>
        /// <typeparam name="IdT">ID类型</typeparam>
        /// <param name="model">模型</param>
        /// <param name="currUser">当前用户</param>
        public static void SetCreateInfo<IdT>(this PersonTimeInfo<IdT> model, BasicUserInfo<IdT> currUser = null)
        {
            var user = UserTool<IdT>.GetCurrUser(currUser);
            if (user == null)
            {
                return;
            }

            model.CreaterId = model.ModifierId = user.Id;
            model.Creater = model.Modifier = user.Name;
            model.CreateTime = model.ModifyTime = DateTime.Now;
        }

        /// <summary>
        /// 设置修改信息
        /// </summary>
        /// <typeparam name="IdT">ID类型</typeparam>
        /// <param name="model">模型</param>
        /// <param name="currUser">当前用户</param>
        public static void SetModifyInfo<IdT>(this PersonTimeInfo<IdT> model, BasicUserInfo<IdT> currUser = null)
        {
            var user = UserTool<IdT>.GetCurrUser(currUser);
            if (user == null)
            {
                return;
            }

            model.ModifierId = user.Id;
            model.Modifier = user.Name;
            model.ModifyTime = DateTime.Now;
        }
    }
}
