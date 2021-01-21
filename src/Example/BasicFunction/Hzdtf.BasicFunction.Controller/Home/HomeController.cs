using Hzdtf.BasicFunction.Service.Contract.User;
using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Controller.Home
{
    /// <summary>
    /// 主页控制器
    /// @ 黄振东
    /// </summary>
    [Authorize]
    [Inject]
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        /// <summary>
        /// 用户菜单服务
        /// </summary>
        public IUserMenuService UserMenuService
        {
            get;
            set;
        }

        /// <summary>
        /// 主页
        /// </summary>
        /// <returns>动作结果</returns>
        public ActionResult Index()
        {
            var user = UserTool<int>.GetCurrUser();
            return View(UserMenuService.CanAccessMenus(user.Id));
        }
    }
}
