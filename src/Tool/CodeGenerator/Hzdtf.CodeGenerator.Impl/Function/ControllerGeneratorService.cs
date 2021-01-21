using Hzdtf.CodeGenerator.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Utils;
using Hzdtf.Utility.Attr;

namespace Hzdtf.CodeGenerator.Impl.Function
{
    /// <summary>
    /// 控制器生成服务
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class ControllerGeneratorService : FunctionGeneratorBase
    { 
        /// <summary>
        /// 类模板
        /// </summary>
        private static string classTemplate;

        /// <summary>
        /// 类模板
        /// </summary>
        private static string ClassTemplate
        {
            get
            {
                if (classTemplate == null)
                {
                    classTemplate = $"{AppContext.BaseDirectory}CodeTemplate\\Controller\\classTemplate.txt".ReaderFileContent();
                }

                return classTemplate;
            }
        }

        /// <summary>
        /// 类模板
        /// </summary>
        private static string pageClassTemplate;

        /// <summary>
        /// 分页类模板
        /// </summary>
        private static string PageClassTemplate
        {
            get
            {
                if (pageClassTemplate == null)
                {
                    pageClassTemplate = $"{AppContext.BaseDirectory}CodeTemplate\\Controller\\pageClassTemplate.txt".ReaderFileContent();
                }

                return pageClassTemplate;
            }
        }

        /// <summary>
        /// 生成代码文本集合
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="codeParam">代码参数</param>
        /// <param name="fileNames">文件名集合</param>
        /// <returns>代码文本集合</returns>
        protected override string[] BuilderCodeTexts(TableInfo table, CodeParamInfo codeParam, out string[] fileNames)
        {
            string basicName = table.Name.FristUpper();
            string name = $"{basicName}Controller";
            fileNames = new string[] { $"{name}.cs" };

            var template = table.IsPage ? PageClassTemplate : ClassTemplate;
            string desc = string.IsNullOrWhiteSpace(table.Description) ? basicName : table.Description;
            return new string[] 
            {
                template
                .Replace("|NamespacePfx|", codeParam.NamespacePfx)
                .Replace("|Description|", desc)
                .Replace("|Name|", name)
                .Replace("|Model|", basicName)
                .Replace("|PkType|", codeParam.PkType.ToCodeString())
            };
        }

        /// <summary>
        /// 子文件夹集合
        /// </summary>
        /// <returns>子文件夹集合</returns>
        protected override string[] SubFolders() => new string[] { "Controller" };
    }
}
