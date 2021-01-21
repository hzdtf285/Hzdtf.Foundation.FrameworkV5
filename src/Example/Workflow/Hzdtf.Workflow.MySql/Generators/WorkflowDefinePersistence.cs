﻿using Hzdtf.Workflow.Model;
using Hzdtf.MySql;
using Hzdtf.Utility.Attr;
using System;
using Hzdtf.Workflow.Persistence.Contract;

namespace Hzdtf.Workflow.MySql
{
    /// <summary>
    /// 工作流定义持久化
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class WorkflowDefinePersistence : MySqlDapperBase<int, WorkflowDefineInfo>, IWorkflowDefinePersistence
    {
        /// <summary>
        /// 表名
        /// </summary>
        public override string Table => "workflow_define";

        /// <summary>
        /// 插入字段名称集合
        /// </summary>
        private readonly static string[] INSERT_FIELD_NAMES = new string[]
        {
            "id",
            "creater_id",
            "creater",
            "create_time",
            "modifier_id",
            "modifier",
            "modify_time",
            "flow_id",
            "form_id",
            "code",
            "name",
            "enabled",
        };

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        private readonly static string[] UPDATE_FIELD_NAMES = new string[]
        {
            "modifier_id",
            "modifier",
            "modify_time",
            "flow_id",
            "form_id",
            "code",
            "name",
            "enabled",
        };

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        private readonly static string[] FIELD_MAP_PROPS = new string[]
        {
            "id Id",
            "creater_id CreaterId",
            "creater Creater",
            "create_time CreateTime",
            "modifier_id ModifierId",
            "modifier Modifier",
            "modify_time ModifyTime",
            "flow_id FlowId",
            "form_id FormId",
            "code Code",
            "name Name",
            "enabled Enabled",
        };

        /// <summary>
        /// 根据字段名获取模型的值
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        protected override object GetValueByFieldName(WorkflowDefineInfo model, string field)
        {
            switch (field)
            {
﻿                case "id":
                    return model.Id;

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

﻿                case "flow_id":
                    return model.FlowId;

﻿                case "form_id":
                    return model.FormId;

﻿                case "code":
                    return model.Code;

﻿                case "name":
                    return model.Name;

﻿                case "enabled":
                    return model.Enabled;

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
