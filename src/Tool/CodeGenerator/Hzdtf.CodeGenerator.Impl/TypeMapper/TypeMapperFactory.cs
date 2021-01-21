using Hzdtf.CodeGenerator.Contract.TypeMapper;
using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.CodeGenerator.Impl.TypeMapper
{
    /// <summary>
    /// 类型映射工厂
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class TypeMapperFactory : ISimpleFactory<string, ITypeMapperService>
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>产品</returns>
        public ITypeMapperService Create(string type)
        {
            switch (type)
            {
                case "SqlServer":
                    return new SqlServerMapperService();

                case "MySql":
                    return new MySqlMapperService();

                default:
                    return null;
            }
        }
    }
}
