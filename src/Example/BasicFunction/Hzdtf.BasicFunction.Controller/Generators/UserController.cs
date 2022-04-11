using Hzdtf.BasicController;
using Hzdtf.BasicFunction.Model;
using Hzdtf.BasicFunction.Service.Contract;
using Hzdtf.Utility.Attr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Hzdtf.BasicFunction.Model.Expand.User;
using System.Collections.Generic;

namespace Hzdtf.BasicFunction.Controller
{
    /// <summary>
    /// 用户控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public partial class UserController : PagingControllerBase<int, UserInfo, IUserService, UserPageInfo, UserFilterInfo>
    {
        /// <summary>
        /// 菜单编码
        /// </summary>
        /// <returns>菜单编码</returns>
        protected override string MenuCode() => "User";
    }
}
