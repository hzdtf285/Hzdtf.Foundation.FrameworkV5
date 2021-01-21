using Hzdtf.Service.Contract;
using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Model.Return;
using Hzdtf.Utility.Model.Page;
using System.Threading.Tasks;

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
        public virtual async Task<object> Page()
        {
            ReturnInfo<PagingInfo<ModelT>> returnInfo = await DoPageAsync();
            return PagingReturnConvert.Convert<ModelT>(returnInfo);
        }

        /// <summary>
        /// 异步去分页
        /// </summary>
        /// <returns>返回信息任务</returns>
        protected virtual async Task<ReturnInfo<PagingInfo<ModelT>>> DoPageAsync()
        {
            int pageIndex, pageSize;
            PageFilterT filter = PagingParseFilter.ToFilterObjectFromHttp<PageFilterT>(Request, out pageIndex, out pageSize);
            AppendFilterParams(filter);

            ReturnInfo<PagingInfo<ModelT>> returnInfo = await QueryPageFromServiceAsync(pageIndex, pageSize, filter);
            AfterPage(returnInfo, pageIndex, pageSize, filter);

            return returnInfo;
        }

        /// <summary>
        /// 异步从服务里查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <returns>返回信息任务</returns>
        protected virtual async Task<ReturnInfo<PagingInfo<ModelT>>> QueryPageFromServiceAsync(int pageIndex, int pageSize, PageFilterT filter) => await Service.QueryPageAsync(pageIndex, pageSize, filter);

        /// <summary>
        /// 分页后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <returns>返回信息</returns>
        protected virtual void AfterPage(ReturnInfo<PagingInfo<ModelT>> returnInfo, int pageIndex, int pageSize, PageFilterT filter) { }

        /// <summary>
        /// 追加筛选参数
        /// </summary>
        /// <param name="pageFilter">分页筛选</param>
        protected virtual void AppendFilterParams(PageFilterT pageFilter) { }
    }
}
