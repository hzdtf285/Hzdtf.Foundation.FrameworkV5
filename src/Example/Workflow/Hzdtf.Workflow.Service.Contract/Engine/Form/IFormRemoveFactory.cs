using Hzdtf.Utility.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Workflow.Service.Contract.Engine.Form
{
    /// <summary>
    /// 表单移除工厂接口
    /// @ 黄振东
    /// </summary>
    public interface IFormRemoveFactory : ISimpleFactory<string, IFormRemove>
    {
    }
}
