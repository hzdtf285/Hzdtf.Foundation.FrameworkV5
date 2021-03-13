using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Attr.ParamAttr;
using Hzdtf.Utility.Cache;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using Hzdtf.Workflow.Model;
using Hzdtf.Workflow.Service.Contract.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.Workflow.Service.Impl.Engine
{
    /// <summary>
    /// 工作流配置缓存
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class WorkflowConfigCache : SingleTypeLocalMemoryBase<int, WorkflowDefineInfo>, IWorkflowConfigReader
    {
        #region 属性与字段

        /// <summary>
        /// 缓存字典
        /// </summary>
        private readonly static IDictionary<int, WorkflowDefineInfo> dicCache = new Dictionary<int, WorkflowDefineInfo>();

        /// <summary>
        /// 编码映射ID字典
        /// </summary>
        private readonly static IDictionary<string, int> dicCodeMapId = new Dictionary<string, int>();

        /// <summary>
        /// 同步缓存字典
        /// </summary>
        private readonly static object syncDicCache = new object();

        /// <summary>
        /// 同步映射ID字典
        /// </summary>
        private readonly static object syncDicCodeMapId = new object();

        /// <summary>
        /// 原生工作流配置读取
        /// </summary>
        public WorkflowDefineService ProtoWorkflowConfigReader
        {
            get;
            set;
        }

        #endregion

        #region IWorkflowConfigReader 接口

        /// <summary>
        /// 根据工作流定义ID读取工作流定义信息的所有配置
        /// </summary>
        /// <param name="workflowDefineId">工作流定义ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<WorkflowDefineInfo> ReaderAllConfig([DisplayName2("工作流定义ID"), Id] int workflowDefineId, CommonUseData comData = null, string connectionId = null)
        {
            if (dicCache.ContainsKey(workflowDefineId))
            {
                ReturnInfo<WorkflowDefineInfo> returnInfo = new ReturnInfo<WorkflowDefineInfo>();
                returnInfo.Data = dicCache[workflowDefineId];

                return returnInfo;
            }
            else
            {
                ReturnInfo<WorkflowDefineInfo> returnInfo = ProtoWorkflowConfigReader.ReaderAllConfig(workflowDefineId, connectionId : connectionId, comData: comData);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }
                Set(workflowDefineId, returnInfo.Data);

                return returnInfo;
            }
        }

        /// <summary>
        /// 根据工作流编码读取工作流定义信息的所有配置
        /// </summary>
        /// <param name="workflowCode">工作流编码</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<WorkflowDefineInfo> ReaderAllConfig([DisplayName2("工作流编码"), Required] string workflowCode, CommonUseData comData = null, string connectionId = null)
        {
            if (dicCodeMapId.ContainsKey(workflowCode))
            {
                return ReaderAllConfig(dicCodeMapId[workflowCode], comData, connectionId);
            }
            else
            {
                ReturnInfo<WorkflowDefineInfo> returnInfo = ProtoWorkflowConfigReader.ReaderAllConfig(workflowCode, connectionId : connectionId, comData: comData);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }
                Set(returnInfo.Data.Id, returnInfo.Data);

                lock (syncDicCodeMapId)
                {
                    if (dicCodeMapId.ContainsKey(workflowCode))
                    {
                        return returnInfo;
                    }
                    dicCodeMapId.Add(workflowCode, returnInfo.Data.Id);
                }

                return returnInfo;
            }
        }

        #endregion

        #region 重写父类的方法

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <returns>缓存</returns>
        protected override IDictionary<int, WorkflowDefineInfo> GetCache() => dicCache;

        /// <summary>
        /// 获取同步的缓存对象，是为了线程安全
        /// </summary>
        /// <returns>同步的缓存对象</returns>
        protected override object GetSyncCache() => syncDicCache;

        #endregion
    }
}
