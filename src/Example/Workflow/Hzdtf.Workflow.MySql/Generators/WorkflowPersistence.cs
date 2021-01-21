﻿using Hzdtf.Workflow.Model;
using Hzdtf.MySql;
using Hzdtf.Utility.Attr;
using System;
using Hzdtf.Workflow.Persistence.Contract;

namespace Hzdtf.Workflow.MySql
{
    /// <summary>
    /// 工作流持久化
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class WorkflowPersistence : MySqlDapperBase<int, WorkflowInfo>, IWorkflowPersistence
    {
        /// <summary>
        /// 表名
        /// </summary>
        public override string Table => "workflow";

        /// <summary>
        /// 插入字段名称集合
        /// </summary>
        private readonly static string[] INSERT_FIELD_NAMES = new string[]
        {
            "id",
            "workflow_define_id",
            "creater_id",
            "creater",
            "create_time",
            "modifier_id",
            "modifier",
            "modify_time",
            "title",
            "apply_no",
            "flow_status",
            "curr_concrete_censorship_ids",
            "curr_concrete_censorships",
            "curr_handler_ids",
            "curr_handlers",
            "curr_flow_censorship_ids",
        };

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        private readonly static string[] UPDATE_FIELD_NAMES = new string[]
        {
            "workflow_define_id",
            "modifier_id",
            "modifier",
            "modify_time",
            "title",
            "apply_no",
            "flow_status",
            "curr_concrete_censorship_ids",
            "curr_concrete_censorships",
            "curr_handler_ids",
            "curr_handlers",
            "curr_flow_censorship_ids",
        };

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        private readonly static string[] FIELD_MAP_PROPS = new string[]
        {
            "id Id",
            "workflow_define_id WorkflowDefineId",
            "creater_id CreaterId",
            "creater Creater",
            "create_time CreateTime",
            "modifier_id ModifierId",
            "modifier Modifier",
            "modify_time ModifyTime",
            "title Title",
            "apply_no ApplyNo",
            "flow_status FlowStatus",
            "curr_concrete_censorship_ids CurrConcreteCensorshipIds",
            "curr_concrete_censorships CurrConcreteCensorships",
            "curr_handler_ids CurrHandlerIds",
            "curr_handlers CurrHandlers",
            "curr_flow_censorship_ids CurrFlowCensorshipIds",
        };

        /// <summary>
        /// 根据字段名获取模型的值
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        protected override object GetValueByFieldName(WorkflowInfo model, string field)
        {
            switch (field)
            {
﻿                case "id":
                    return model.Id;

﻿                case "workflow_define_id":
                    return model.WorkflowDefineId;

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

﻿                case "title":
                    return model.Title;

﻿                case "apply_no":
                    return model.ApplyNo;

﻿                case "flow_status":
                    return model.FlowStatus;

﻿                case "curr_concrete_censorship_ids":
                    return model.CurrConcreteCensorshipIds;

﻿                case "curr_concrete_censorships":
                    return model.CurrConcreteCensorships;

﻿                case "curr_handler_ids":
                    return model.CurrHandlerIds;

﻿                case "curr_handlers":
                    return model.CurrHandlers;

﻿                case "curr_flow_censorship_ids":
                    return model.CurrFlowCensorshipIds;

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
