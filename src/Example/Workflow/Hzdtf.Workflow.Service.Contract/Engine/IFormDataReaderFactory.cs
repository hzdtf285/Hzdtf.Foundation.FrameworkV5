using Hzdtf.Utility.Factory;
using Hzdtf.Utility.Model;
using Hzdtf.Workflow.Model.Expand;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Workflow.Service.Contract.Engine
{
    /// <summary>
    /// 表单数据读取工厂接口
    /// @ 黄振东
    /// </summary>
    public interface IFormDataReaderFactory : ISimpleFactory<string, IFormDataReader>
    {
    }
}
