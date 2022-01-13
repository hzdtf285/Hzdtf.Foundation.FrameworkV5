using Hzdtf.Service.Contract;
using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Model.Return;
using Hzdtf.Utility.Model.Page;
using Microsoft.AspNetCore.Http;

namespace Hzdtf.BasicController
{
    /// <summary>
    /// 分页控制器基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">Id类型</typeparam>
    /// <typeparam name="ModelT">模型类型</typeparam>
    /// <typeparam name="ServiceT">服务类型</typeparam>
    /// <typeparam name="PageInfoT">页面信息类型</typeparam>
    /// <typeparam name="PageFilterT">分页筛选类型</typeparam>
    public abstract class PagingControllerBase<IdT, ModelT, ServiceT, PageInfoT, PageFilterT> : ManageControllerBase<IdT, ModelT, ServiceT, PageInfoT>
        where ModelT : SimpleInfo<IdT>
        where ServiceT : IService<IdT, ModelT>
        where PageInfoT : PageInfo<IdT>
        where PageFilterT : FilterInfo
    {
        /// <summary>
        /// 分页解析筛选
        /// </summary>
        public IPagingParseFilter PagingParseFilter
        {
            get;
            set;
        }

        /// <summary>
        /// 分页返回转换
        /// </summary>
        public IPagingReturnConvert PagingReturnConvert
        {
            get;
            set;
        }

        /// <summary>
        /// 执行分页获取数据
        /// </summary>
        /// <returns>分页返回信息</returns>
        [HttpGet()]
        [Function(FunCodeDefine.QUERY_CODE)]
        public virtual object Page()
        {
            var comData = HttpContext.CreateCommonUseData(ComUseDataFactory, menuCode: MenuCode(), functionCodes: FunCodeDefine.QUERY_CODE);
            ReturnInfo<PagingInfo<ModelT>> returnInfo = DoPage(comData);
            return PagingReturnConvert.Convert<ModelT>(returnInfo);
        }

        /// <summary>
        /// 去分页
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息任务</returns>
        protected virtual ReturnInfo<PagingInfo<ModelT>> DoPage(CommonUseData comData = null)
        {
            int pageIndex, pageSize;
            PageFilterT filter = PagingParseFilter.ToFilterObjectFromHttp<PageFilterT>(Request, out pageIndex, out pageSize);
            AppendFilterParams(filter, comData);

            ReturnInfo<PagingInfo<ModelT>> returnInfo = QueryPageFromService(pageIndex, pageSize, filter, comData);
            AfterPage(returnInfo, pageIndex, pageSize, filter, comData);

            return returnInfo;
        }

        /// <summary>
        /// 从服务里查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息任务</returns>
        protected virtual ReturnInfo<PagingInfo<ModelT>> QueryPageFromService(int pageIndex, int pageSize, PageFilterT filter, CommonUseData comData = null) => Service.QueryPage(pageIndex, pageSize, filter, comData);

        /// <summary>
        /// 分页后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        protected virtual void AfterPage(ReturnInfo<PagingInfo<ModelT>> returnInfo, int pageIndex, int pageSize, PageFilterT filter, CommonUseData comData = null) { }

        /// <summary>
        /// 追加筛选参数
        /// </summary>
        /// <param name="pageFilter">分页筛选</param>
        /// <param name="comData">通用数据</param>
        protected virtual void AppendFilterParams(PageFilterT pageFilter, CommonUseData comData = null) { }
    }
}
