using Hzdtf.BasicFunction.Model;
using Hzdtf.BasicFunction.Persistence.Contract;
using Hzdtf.BasicFunction.Service.Contract;
using Hzdtf.Service.Impl;
using Hzdtf.Utility.Attr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Service.Impl
{
    /// <summary>
    /// 用户菜单功能服务
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class UserMenuFunctionService : ServiceBase<int, UserMenuFunctionInfo, IUserMenuFunctionPersistence>, IUserMenuFunctionService
    {
    }
}
