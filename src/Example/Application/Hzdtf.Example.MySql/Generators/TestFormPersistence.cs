﻿using Hzdtf.Example.Model;
using Hzdtf.MySql;
using Hzdtf.Utility.Attr;
using System;
using Hzdtf.Example.Persistence.Contract;

namespace Hzdtf.Example.MySql
{
    /// <summary>
    /// 测试表单持久化
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class TestFormPersistence : MySqlDapperBase<int, TestFormInfo>, ITestFormPersistence
    {
        /// <summary>
        /// 表名
        /// </summary>
        public override string Table => "test_form";

        /// <summary>
        /// 插入字段名称集合
        /// </summary>
        private readonly static string[] INSERT_FIELD_NAMES = new string[]
        {
            "Id",
            "code",
            "name",
            "apply_no",
            "flow_status",
            "workflow_id",
            "creater_id",
            "creater",
            "modifier_id",
            "modifier",
            "create_time",
            "modify_time",
        };

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        private readonly static string[] UPDATE_FIELD_NAMES = new string[]
        {
            "code",
            "name",
            "apply_no",
            "flow_status",
            "workflow_id",
            "modifier_id",
            "modifier",
            "modify_time",
        };

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        private readonly static string[] FIELD_MAP_PROPS = new string[]
        {
            "Id Id",
            "code Code",
            "name Name",
            "apply_no ApplyNo",
            "flow_status FlowStatus",
            "workflow_id WorkflowId",
            "creater_id CreaterId",
            "creater Creater",
            "modifier_id ModifierId",
            "modifier Modifier",
            "create_time CreateTime",
            "modify_time ModifyTime",
        };

        /// <summary>
        /// 根据字段名获取模型的值
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        protected override object GetValueByFieldName(TestFormInfo model, string field)
        {
            switch (field)
            {
﻿                case "Id":
                    return model.Id;

﻿                case "code":
                    return model.Code;

﻿                case "name":
                    return model.Name;

﻿                case "apply_no":
                    return model.ApplyNo;

﻿                case "flow_status":
                    return model.FlowStatus;

﻿                case "workflow_id":
                    return model.WorkflowId;

﻿                case "creater_id":
                    return model.CreaterId;

﻿                case "creater":
                    return model.Creater;

﻿                case "modifier_id":
                    return model.ModifierId;

﻿                case "modifier":
                    return model.Modifier;

﻿                case "create_time":
                    return model.CreateTime;

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
