using Hzdtf.BasicFunction.Model;
using Hzdtf.BasicFunction.Service.Contract;
using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Utils;
using Hzdtf.BasicFunction.Model.Expand.DataDictionaryItem;
using Hzdtf.Utility.Enums;
using Microsoft.AspNetCore.Http;

namespace Hzdtf.BasicFunction.Controller
{
    /// <summary>
    /// 数据字典子项控制器
    /// @ 黄振东
    /// </summary>
    public partial class DataDictionaryItemController
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
        /// 数据字典服务
        /// </summary>
        public IDataDictionaryService DataDictionaryService
        {
            get;
            set;
        }

        /// <summary>
        /// 数据字典子项扩展服务
        /// </summary>
        public IDataDictionaryItemExpandService DataDictionaryItemExpandService
        {
            get;
            set;
        }

        /// <summary>
        /// 查询所有的数据字典列表
        /// </summary>
        /// <returns>数据字典列表</returns>
        [HttpGet("DataDictionarys")]
        [Function(FunCodeDefine.QUERY_CODE)]
        public virtual IList<DataDictionaryInfo> DataDictionarys() => DataDictionaryService.Query(HttpContext.CreateCommonUseData(ComUseDataFactory, menuCode: MenuCode(), functionCode: FunCodeDefine.QUERY_CODE)).Data;

        /// <summary>
        /// 执行分页获取数据
        /// </summary>
        /// <param name="page">页码，从1开始</param>
        /// <param name="rows">每页记录数</param>
        /// <returns>分页返回信息</returns>
        [HttpGet("PageExpandList")]
        [Function(FunCodeDefine.QUERY_CODE)]
        public virtual Page1ReturnInfo<DataDictionaryItemExpandInfo> PageExpandList(int page, int rows)
        {
            var comData = HttpContext.CreateCommonUseData(ComUseDataFactory, menuCode: MenuCode(), functionCode: FunCodeDefine.QUERY_CODE);
            IDictionary<string, string> dicParams = Request.QueryString.Value.ToDictionaryFromUrlParams();
            dicParams.RemoveKey("page");
            dicParams.RemoveKey("rows");

            DataDictionaryItemExpandFilterInfo filter = null;
            if (!dicParams.IsNullOrCount0())
            {
                filter = dicParams.ToObject<DataDictionaryItemExpandFilterInfo, string>();
                if (dicParams.ContainsKey("sidx") && !string.IsNullOrWhiteSpace(dicParams["sidx"]))
                {
                    filter.SortName = dicParams["sidx"];
                }
                if (dicParams.ContainsKey("sord") && !string.IsNullOrWhiteSpace(dicParams["sord"]))
                {
                    if ("desc".Equals(dicParams["sord"]))
                    {
                        filter.Sort = SortType.DESC;
                    }
                }
            }

            return Page1ReturnInfo<DataDictionaryItemExpandInfo>.From(DataDictionaryItemExpandService.QueryPage(page - 1, rows, filter, comData));
        }

        /// <summary>
        /// 填充页面数据，包含当前用户所拥有的权限功能列表
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="comData">通用数据</param>
        protected override void FillPageData(ReturnInfo<PageInfo<int>> returnInfo, CommonUseData comData = null)
        {
            var re = UserService.QueryPageData<PageInfo<int>>(MenuCode(), () =>
            {
                return returnInfo.Data;
            }, comData: comData);
            returnInfo.FromBasic(re);
        }

        /// <summary>
        /// 创建页面数据
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <returns>页面数据</returns>
        protected override PageInfo<int> CreatePageData(CommonUseData comData = null) => new PageInfo<int>();
    }
}
