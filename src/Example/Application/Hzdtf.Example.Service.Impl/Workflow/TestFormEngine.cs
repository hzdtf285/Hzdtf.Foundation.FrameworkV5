using Hzdtf.Example.Model;
using Hzdtf.Utility.Attr;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Workflow.Service.Contract.Engine.Form;

namespace Hzdtf.Example.Service.Impl.Workflow
{
    /// <summary>
    /// 测试表单引擎
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class TestFormEngine : SimpleFormEngineBase<TestFormInfo>
    {
    }
}
