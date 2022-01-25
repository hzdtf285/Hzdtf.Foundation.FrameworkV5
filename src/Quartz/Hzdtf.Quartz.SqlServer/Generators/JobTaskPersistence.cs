﻿using Hzdtf.Quartz.Model;
using Hzdtf.SqlServer;
using Hzdtf.Utility.Attr;
using System;
using Hzdtf.Quartz.Persistence.Contract;

namespace Hzdtf.Quartz.SqlServer
{
    /// <summary>
    /// 作业任务持久化
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class JobTaskPersistence : SqlServerDapperBase<int, JobTaskInfo>, IJobTaskPersistence
    {
        /// <summary>
        /// 表名
        /// </summary>
        public override string Table => "job_task";

        /// <summary>
        /// 插入字段名称集合
        /// </summary>
        private readonly static string[] INSERT_FIELD_NAMES = new string[]
        {
            "create_time",
            "id",
            "job_full_class",
            "job_params_json_string",
            "jt_desc",
            "jt_group",
            "jt_name",
            "modify_time",
            "successed_remove",
            "trigger_cron",
            "trigger_params_json_string",
        };

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        private readonly static string[] UPDATE_FIELD_NAMES = new string[]
        {
            "job_full_class",
            "job_params_json_string",
            "jt_desc",
            "jt_group",
            "jt_name",
            "modify_time",
            "successed_remove",
            "trigger_cron",
            "trigger_params_json_string",
        };

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        private readonly static string[] FIELD_MAP_PROPS = new string[]
        {
            "create_time CreateTime",
            "id Id",
            "job_full_class JobFullClass",
            "job_params_json_string JobParamsJsonString",
            "jt_desc JtDesc",
            "jt_group JtGroup",
            "jt_name JtName",
            "modify_time ModifyTime",
            "successed_remove SuccessedRemove",
            "trigger_cron TriggerCron",
            "trigger_params_json_string TriggerParamsJsonString",
        };

        /// <summary>
        /// 根据字段名获取模型的值
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        protected override object GetValueByFieldName(JobTaskInfo model, string field)
        {
            switch (field)
            {
﻿                case "create_time":
                    return model.CreateTime;

﻿                case "id":
                    return model.Id;

﻿                case "job_full_class":
                    return model.JobFullClass;

﻿                case "job_params_json_string":
                    return model.JobParamsJsonString;

﻿                case "jt_desc":
                    return model.JtDesc;

﻿                case "jt_group":
                    return model.JtGroup;

﻿                case "jt_name":
                    return model.JtName;

﻿                case "modify_time":
                    return model.ModifyTime;

﻿                case "successed_remove":
                    return model.SuccessedRemove;

﻿                case "trigger_cron":
                    return model.TriggerCron;

﻿                case "trigger_params_json_string":
                    return model.TriggerParamsJsonString;

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
