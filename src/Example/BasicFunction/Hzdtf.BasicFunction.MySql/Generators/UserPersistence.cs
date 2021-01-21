﻿using Hzdtf.BasicFunction.Model;
using Hzdtf.MySql;
using Hzdtf.Utility.Attr;
using System;
using Hzdtf.BasicFunction.Persistence.Contract;

namespace Hzdtf.BasicFunction.MySql
{
    /// <summary>
    /// 用户持久化
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class UserPersistence : MySqlDapperBase<int, UserInfo>, IUserPersistence
    {
        /// <summary>
        /// 表名
        /// </summary>
        public override string Table => "user";

        /// <summary>
        /// 插入字段名称集合
        /// </summary>
        private readonly static string[] INSERT_FIELD_NAMES = new string[]
        {
            "id",
            "login_id",
            "password",
            "sex",
            "enabled",
            "system_inlay",
            "system_hide",
            "memo",
            "code",
            "name",
            "creater_id",
            "creater",
            "create_time",
            "modifier_id",
            "modifier",
            "qq",
            "wechat",
            "mail",
            "mobile",
            "modify_time",
        };

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        private readonly static string[] UPDATE_FIELD_NAMES = new string[]
        {
            "login_id",
            "password",
            "sex",
            "enabled",
            "system_inlay",
            "system_hide",
            "memo",
            "code",
            "name",
            "modifier_id",
            "modifier",
            "qq",
            "wechat",
            "mail",
            "mobile",
            "modify_time",
        };

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        private readonly static string[] FIELD_MAP_PROPS = new string[]
        {
            "id Id",
            "login_id LoginId",
            "password Password",
            "sex Sex",
            "enabled Enabled",
            "system_inlay SystemInlay",
            "system_hide SystemHide",
            "memo Memo",
            "code Code",
            "name Name",
            "creater_id CreaterId",
            "creater Creater",
            "create_time CreateTime",
            "modifier_id ModifierId",
            "modifier Modifier",
            "qq QQ",
            "wechat Wechat",
            "mail Mail",
            "mobile Mobile",
            "modify_time ModifyTime",
        };

        /// <summary>
        /// 根据字段名获取模型的值
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        protected override object GetValueByFieldName(UserInfo model, string field)
        {
            switch (field)
            {
﻿                case "id":
                    return model.Id;

﻿                case "login_id":
                    return model.LoginId;

﻿                case "password":
                    return model.Password;

﻿                case "sex":
                    return model.Sex;

﻿                case "enabled":
                    return model.Enabled;

﻿                case "system_inlay":
                    return model.SystemInlay;

﻿                case "system_hide":
                    return model.SystemHide;

﻿                case "memo":
                    return model.Memo;

﻿                case "code":
                    return model.Code;

﻿                case "name":
                    return model.Name;

﻿                case "creater_id":
                    return model.CreaterId;

﻿                case "creater":
                    return model.Creater;

﻿                case "create_time":
                    return model.CreateTime;

﻿                case "modifier_id":
                    return model.ModifierId;

﻿                case "modifier":
                    return model.Modifier;

﻿                case "qq":
                    return model.QQ;

﻿                case "wechat":
                    return model.Wechat;

﻿                case "mail":
                    return model.Mail;

﻿                case "mobile":
                    return model.Mobile;

﻿                case "modify_time":
                    return model.ModifyTime;

                default:
                    return null;
            }
        }

        /// <summary>
        /// 插入字段名集合
        /// </summary>
        /// <returns>插入字段名集合</returns>
        protected override string[] InsertFieldNames() => INSERT_FIELD_NAMES;

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        /// <returns>更新字段名称集合</returns>
        protected override string[] UpdateFieldNames() => UPDATE_FIELD_NAMES;

		/// <summary>
        /// 所有字段映射集合
        /// </summary>
        /// <returns>所有字段映射集合</returns>
        public override string[] AllFieldMapProps() => FIELD_MAP_PROPS;
    }
}
