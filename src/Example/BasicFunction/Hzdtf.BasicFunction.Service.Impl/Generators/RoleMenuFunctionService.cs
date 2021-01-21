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
    /// 角色菜单功能服务
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class RoleMenuFunctionService : ServiceBase<int, RoleMenuFunctionInfo, IRoleMenuFunctionPersistence>, IRoleMenuFunctionService
    {
    }
}
