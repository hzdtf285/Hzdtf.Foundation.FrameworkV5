using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using Hzdtf.Workflow.Model.Expand.Diversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Workflow.Service.Contract.Engine.Diversion
{
    /// <summary>
    /// 查找处理者用户接口
    /// @ 黄振东
    /// </summary>
    public partial interface IFindHandlerUser
    {
        /// <summary>
        /// 根据ID查找用户信息数组
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        ReturnInfo<FindHandlerUserOutInfo> FindById(int id, int userId, CommonUseData comData = null, string connectionId = null);
    }
}
