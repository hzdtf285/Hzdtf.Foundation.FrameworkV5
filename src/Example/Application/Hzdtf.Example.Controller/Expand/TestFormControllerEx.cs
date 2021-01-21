using Hzdtf.BasicFunction.Service.Contract;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hzdtf.Example.Controller
{
    /// <summary>
    /// 测试表单控制器
    /// @ 黄振东
    /// </summary>
    public partial class TestFormController
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
        /// 填充页面数据，包含当前用户所拥有的权限功能列表
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        protected override void FillPageData(ReturnInfo<PageInfo<int>> returnInfo)
        {
            var re = UserService.QueryPageData<PageInfo<int>>(MenuCode(), () =>
            {
                return returnInfo.Data;
            });
            returnInfo.FromBasic(re);
        }

        /// <summary>
        /// 创建页面数据
        /// </summary>
        /// <returns>页面数据</returns>
        protected override PageInfo<int> CreatePageData() => new PageInfo<int>();
    }
}
