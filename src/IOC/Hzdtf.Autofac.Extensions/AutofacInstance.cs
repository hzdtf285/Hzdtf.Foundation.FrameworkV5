using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Hzdtf.Autofac.Extensions
{
    /// <summary>
    /// Autofac实例
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class AutofacInstance : IInstance
    {
        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="classFullPath">类全路径</param>
        /// <returns>实例</returns>
        public object CreateInstance(string classFullPath)
        {
            return CreateInstanceObj(classFullPath);
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <param name="classFullPath">类全路径</param>
        /// <returns>实例</returns>
        public T CreateInstance<T>(string classFullPath)
            where T : class
        {
            return CreateInstanceObj(classFullPath) as T;
        }

        /// <summary>
        /// 创建实例对象
        /// </summary>
        /// <param name="classFullPath">类全路径</param>
        /// <returns>实例</returns>
        private object CreateInstanceObj(string classFullPath)
        {
            return AutofacTool.Resolve(ReflectExtensions.GetClassType(classFullPath));
        }
    }
}
