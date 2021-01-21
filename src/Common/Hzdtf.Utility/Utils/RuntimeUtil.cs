using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace Hzdtf.Utility.Utils
{
    /// <summary>
    /// 运行时辅助类
    /// @ 黄振东
    /// </summary>
    public static class RuntimeUtil
    {
        /// <summary>
        /// 获取项目程序集，排除所有的系统程序集(Microsoft.***、System.***等)、Nuget下载包
        /// </summary>
        /// <returns>程序集列表</returns>
        public static IList<Assembly> GetAllAssemblies()
        {
            var list = new List<Assembly>();
            var deps = DependencyContext.Default;
            var libs = deps.CompileLibraries.Where(lib => !lib.Serviceable && lib.Type != "package");//排除所有的系统程序集、Nuget下载包
            foreach (var lib in libs)
            {
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
                list.Add(assembly);
            }

            return list;
        }

        /// <summary>
        /// 根据程序集名获取程序集
        /// </summary>
        /// <param name="assemblyName">程序集名</param>
        /// <returns>程序集</returns>
        public static Assembly GetAssembly(string assemblyName)
        {
            return GetAllAssemblies().FirstOrDefault(assembly => assembly.FullName.Contains(assemblyName));
        }

        /// <summary>
        /// 获取所有类型列表
        /// </summary>
        /// <returns>类型列表</returns>
        public static IList<Type> GetAllTypes()
        {
            var list = new List<Type>();
            foreach (var assembly in GetAllAssemblies())
            {
                var typeInfos = assembly.DefinedTypes;
                foreach (var typeInfo in typeInfos)
                {
                    list.Add(typeInfo.AsType());
                }
            }

            return list;
        }

        /// <summary>
        /// 根据程序集名获取类型列表
        /// </summary>
        /// <param name="assemblyName">程序集名</param>
        /// <returns>类型列表</returns>
        public static IList<Type> GetTypesByAssembly(string assemblyName)
        {
            var list = new List<Type>();
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(assemblyName));
            var typeInfos = assembly.DefinedTypes;
            foreach (var typeInfo in typeInfos)
            {
                list.Add(typeInfo.AsType());
            }

            return list;
        }

        /// <summary>
        /// 根据类型名和接口类型获取类型
        /// </summary>
        /// <param name="typeName">类型名</param>
        /// <param name="baseInterfaceType">接口类型</param>
        /// <returns>类型</returns>
        public static Type GetImplementType(string typeName, Type baseInterfaceType)
        {
            return GetAllTypes().FirstOrDefault(t =>
            {
                if (t.Name == typeName && t.GetTypeInfo().GetInterfaces().Any(b => b.Name == baseInterfaceType.Name))
                {
                    var typeInfo = t.GetTypeInfo();

                    return typeInfo.IsClass && !typeInfo.IsAbstract && !typeInfo.IsGenericType;
                }

                return false;
            });
        }
    }
}
