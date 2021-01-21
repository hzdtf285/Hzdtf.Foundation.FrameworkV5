using Hzdtf.Example.Model;
using Hzdtf.Utility.Attr;
using Hzdtf.Workflow.Service.Contract.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Example.Service.Impl.Workflow
{
    /// <summary>
    /// 表单数据读取工厂
    /// 黄振东
    /// </summary>
    [Inject]
    public class FormDataReaderFactory : IFormDataReaderFactory
    {
        /// <summary>
        /// 测试表单服务
        /// </summary>
        public TestFormService TestFormService
        {
            get;
            set;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>产品</returns>
        public IFormDataReader Create(string type)
        {
            switch (type)
            {
                case WorkflowDefine.TEST_FORM:

                    return TestFormService;

                default:

                    return null;
            }
        }
    }
}
