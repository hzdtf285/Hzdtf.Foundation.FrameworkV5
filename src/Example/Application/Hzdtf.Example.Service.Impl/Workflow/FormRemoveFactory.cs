using Hzdtf.Example.Model;
using Hzdtf.Utility.Attr;
using Hzdtf.Workflow.Service.Contract.Engine.Form;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Example.Service.Impl.Workflow
{
    /// <summary>
    /// 表单移除工厂
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class FormRemoveFactory : IFormRemoveFactory
    {
        /// <summary>
        /// 测试表单移除
        /// </summary>
        public TestFormRemove TestFormRemove
        {
            get;
            set;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>产品</returns>
        public IFormRemove Create(string type)
        {
            switch (type)
            {
                case WorkflowDefine.TEST_FORM:

                    return TestFormRemove;

                default:

                    return null;
            }
        }
    }
}
