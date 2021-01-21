using Hzdtf.Example.Model;
using Hzdtf.Example.Persistence.Contract;
using Hzdtf.Example.Service.Contract;
using Hzdtf.Service.Impl;
using Hzdtf.Utility.Attr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Example.Service.Impl
{
    /// <summary>
    /// 测试表单服务
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class TestFormService : ServiceBase<int, TestFormInfo, ITestFormPersistence>, ITestFormService
    {
    }
}
