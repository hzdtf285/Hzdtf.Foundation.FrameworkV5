﻿using Hzdtf.Workflow.Model;
using Hzdtf.MySql;
using Hzdtf.Utility.Attr;
using System;
using Hzdtf.Workflow.Persistence.Contract;

namespace Hzdtf.Workflow.MySql
{
    /// <summary>
    /// 工作流处理持久化
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class WorkflowHandlePersistence : MySqlDapperBase<int, WorkflowHandleInfo>, IWorkflowHandlePersistence
    {
        /// <summary>
        /// 表名
        /// </summary>
        public override string Table => "workflow_handle";

        /// <summary>
        /// 插入字段名称集合
        /// </summary>
        private readonly static string[] INSERT_FIELD_NAMES = new string[]
        {
            "id",
            "workflow_id",
            "flow_censorship_id",
            "creater_id",
            "creater",
            "create_time",
            "modifier_id",
            "modifier",
            "modify_time",
            "handler_id",
            "handler",
            "handle_time",
            "handle_status",
            "is_readed",
            "idea",
            "handle_type",
            "concrete_concrete_id",
            "concrete_concrete",
        };

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        private readonly static string[] UPDATE_FIELD_NAMES = new string[]
        {
            "workflow_id",
            "flow_censorship_id",
            "modifier_id",
            "modifier",
            "modify_time",
            "handler_id",
            "handler",
            "handle_time",
            "handle_status",
            "is_readed",
            "idea",
            "handle_type",
            "concrete_concrete_id",
            "concrete_concrete",
        };

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        private readonly static string[] FIELD_MAP_PROPS = new string[]
        {
            "id Id",
            "workflow_id WorkflowId",
            "flow_censorship_id FlowCensorshipId",
            "creater_id CreaterId",
            "creater Creater",
            "create_time CreateTime",
            "modifier_id ModifierId",
            "modifier Modifier",
            "modify_time ModifyTime",
            "handler_id HandlerId",
            "handler Handler",
            "handle_time HandleTime",
            "handle_status HandleStatus",
            "is_readed IsReaded",
            "idea Idea",
            "handle_type HandleType",
            "concrete_concrete_id ConcreteConcreteId",
            "concrete_concrete ConcreteConcrete",
        };

        /// <summary>
        /// 根据字段名获取模型的值
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        protected override object GetValueByFieldName(WorkflowHandleInfo model, string field)
        {
            switch (field)
            {
﻿                case "id":
                    return model.Id;

﻿                case "workflow_id":
                    return model.WorkflowId;

﻿                case "flow_censorship_id":
                    return model.FlowCensorshipId;

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

﻿                case "handler_id":
                    return model.HandlerId;

﻿                case "handler":
                    return model.Handler;

﻿                case "handle_time":
                    return model.HandleTime;

﻿                case "handle_status":
                    return model.HandleStatus;

﻿                case "is_readed":
                    return model.IsReaded;

﻿                case "idea":
                    return model.Idea;

﻿                case "handle_type":
                    return model.HandleType;

﻿                case "concrete_concrete_id":
                    return model.ConcreteConcreteId;

﻿                case "concrete_concrete":
                    return model.ConcreteConcrete;

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
