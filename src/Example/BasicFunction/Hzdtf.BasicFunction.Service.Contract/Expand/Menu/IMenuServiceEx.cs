using Hzdtf.BasicFunction.Model.Expand.Menu;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Service.Contract
{
    /// <summary>
    /// 菜单服务接口
    /// @ 黄振东
    /// </summary>
    public partial interface IMenuService
    {
        /// <summary>
        /// 查询菜单树列表
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        ReturnInfo<IList<MenuTreeInfo>> QueryMenuTrees(CommonUseData comData = null, string connectionId = null);
    }
}
