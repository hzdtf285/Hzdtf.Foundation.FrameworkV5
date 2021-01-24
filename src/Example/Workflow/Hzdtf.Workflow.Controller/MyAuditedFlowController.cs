﻿using Hzdtf.BasicController;
using Hzdtf.BasicFunction.Service.Contract;
using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Page;
using Hzdtf.Utility.Model.Return;
using Hzdtf.Workflow.Model;
using Hzdtf.Workflow.Model.Expand.Filter;
using Hzdtf.Workflow.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Workflow.Controller
{
    /// <summary>
    /// 我的已审核流程控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public partial class MyAuditedFlowController : PagingControllerBase<int, WorkflowInfo, IWorkflowService, DateRangePageInfo, AuditFlowFilterInfo>
    {
        /// <summary>
        /// 用户服务
        /// </summary>
        public IUserService UserService
        {
            get;
            set;
        }

        /// <summary>
        /// 菜单编码
        /// </summary>
        /// <returns>菜单编码</returns>
        protected override string MenuCode() => "MyAuditedFlow";

        /// <summary>
        /// 从服务里查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <returns>返回信息任务</returns>
        protected override Task<ReturnInfo<PagingInfo<WorkflowInfo>>> QueryPageFromServiceAsync(int pageIndex, int pageSize, AuditFlowFilterInfo filter)
        {
            return Task<ReturnInfo<PagingInfo<WorkflowInfo>>>.Run(() => Service.QueryCurrUserAuditedFlowPage(pageIndex, pageSize, filter));
        }

        /// <summary>
        /// 获取流程明细信息
        /// </summary>
        /// <param name="workflowId">工作流ID</param>
        /// <param name="handleId">处理ID</param>
        /// <returns>返回信息</returns>
        [HttpGet("GetFlowDetail/{workflowId}/{handleId}")]
        public virtual ReturnInfo<WorkflowInfo> GetFlowDetail(int workflowId, int handleId) => Service.FindAuditedDetail(workflowId, handleId);

        /// <summary>
        /// 填充页面数据，包含当前用户所拥有的权限功能列表
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        protected override void FillPageData(ReturnInfo<DateRangePageInfo> returnInfo)
        {
            var re = UserService.QueryPageData<DateRangePageInfo>(MenuCode(), () =>
            {
                return returnInfo.Data;
            });
            returnInfo.FromBasic(re);
        }

        /// <summary>
        /// 创建页面数据
        /// </summary>
        /// <returns>页面数据</returns>
        protected override DateRangePageInfo CreatePageData() => new DateRangePageInfo();
    }
}