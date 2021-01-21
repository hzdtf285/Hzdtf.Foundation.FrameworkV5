using Hzdtf.Example.Model;
using Hzdtf.Service.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Example.Service.Contract
{
    /// <summary>
    /// 测试表单服务接口
    /// @ 黄振东
    /// </summary>
    public partial interface ITestFormService : IService<int, TestFormInfo>
    {
    }
}
