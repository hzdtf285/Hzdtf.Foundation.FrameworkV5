﻿using Hzdtf.BasicFunction.Model;
using Hzdtf.MySql;
using Hzdtf.Utility.Attr;
using System;
using Hzdtf.BasicFunction.Persistence.Contract;

namespace Hzdtf.BasicFunction.MySql
{
    /// <summary>
    /// 附件持久化
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class AttachmentPersistence : MySqlDapperBase<int, AttachmentInfo>, IAttachmentPersistence
    {
        /// <summary>
        /// 表名
        /// </summary>
        public override string Table => "attachment";

        /// <summary>
        /// 插入字段名称集合
        /// </summary>
        private readonly static string[] INSERT_FIELD_NAMES = new string[]
        {
            "id",
            "file_name",
            "expand_name",
            "title",
            "file_address",
            "file_size",
            "owner_type",
            "owner_id",
            "memo",
            "creater_id",
            "creater",
            "create_time",
            "modifier_id",
            "modifier",
            "modify_time",
        };

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        private readonly static string[] UPDATE_FIELD_NAMES = new string[]
        {
            "file_name",
            "expand_name",
            "title",
            "file_address",
            "file_size",
            "owner_type",
            "owner_id",
            "memo",
            "modifier_id",
            "modifier",
            "modify_time",
        };

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        private readonly static string[] FIELD_MAP_PROPS = new string[]
        {
            "id Id",
            "file_name FileName",
            "expand_name ExpandName",
            "title Title",
            "file_address FileAddress",
            "file_size FileSize",
            "owner_type OwnerType",
            "owner_id OwnerId",
            "memo Memo",
            "creater_id CreaterId",
            "creater Creater",
            "create_time CreateTime",
            "modifier_id ModifierId",
            "modifier Modifier",
            "modify_time ModifyTime",
        };

        /// <summary>
        /// 根据字段名获取模型的值
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        protected override object GetValueByFieldName(AttachmentInfo model, string field)
        {
            switch (field)
            {
﻿                case "id":
                    return model.Id;

﻿                case "file_name":
                    return model.FileName;

﻿                case "expand_name":
                    return model.ExpandName;

﻿                case "title":
                    return model.Title;

﻿                case "file_address":
                    return model.FileAddress;

﻿                case "file_size":
                    return model.FileSize;

﻿                case "owner_type":
                    return model.OwnerType;

﻿                case "owner_id":
                    return model.OwnerId;

﻿                case "memo":
                    return model.Memo;

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
