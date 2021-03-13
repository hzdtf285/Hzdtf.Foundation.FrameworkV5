﻿using Hzdtf.BasicFunction.Model;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Service.Contract
{
    /// <summary>
    /// 数据字典子项服务接口
    /// @ 黄振东
    /// </summary>
    public partial interface IDataDictionaryItemService
    {
        /// <summary>
        /// 根据数据字典ID获取数据字典子项列表
        /// </summary>
        /// <param name="dataDictionaryId">数据字典ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        ReturnInfo<IList<DataDictionaryItemInfo>> QueryByDataDictionaryId(int dataDictionaryId, CommonUseData comData = null, string connectionId = null);

        /// <summary>
        /// 根据数据字典编码获取数据字典子项列表
        /// </summary>
        /// <param name="dataDictionaryCode">数据字典编码</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        ReturnInfo<IList<DataDictionaryItemInfo>> QueryByDataDictionaryCode(string dataDictionaryCode, CommonUseData comData = null, string connectionId = null);
    }
}
