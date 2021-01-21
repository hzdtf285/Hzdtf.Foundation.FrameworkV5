using Hzdtf.CodeGenerator.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Utils;
using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Model.Return;
using Hzdtf.CodeGenerator.Contract.Function;

namespace Hzdtf.CodeGenerator.Impl.Function
{
    /// <summary>
    /// 路由权限配置生成服务
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class RoutePermissionConfigGeneratorService : IFunctionGeneratorService
    {
        /// <summary>
        /// 路由权限模板
        /// </summary>
        private static string routePermissionTemplate;

        /// <summary>
        /// 路由权限配置模板
        /// </summary>
        private static string RoutePermissionTemplate
        {
            get
            {
                if (routePermissionTemplate == null)
                {
                    routePermissionTemplate = $"{AppContext.BaseDirectory}CodeTemplate\\RoutePermissionConfig\\routePermissionConfig.txt".ReaderFileContent();
                }

                return routePermissionTemplate;
            }
        }

        /// <summary>
        /// 控制器配置模板
        /// </summary>
        private static string controllerConfigTemplate;

        /// <summary>
        /// 控制器模板
        /// </summary>
        private static string ControllerConfigTemplate
        {
            get
            {
                if (controllerConfigTemplate == null)
                {
                    controllerConfigTemplate = $"{AppContext.BaseDirectory}CodeTemplate\\RoutePermissionConfig\\controllerConfig.txt".ReaderFileContent();
                }

                return controllerConfigTemplate;
            }
        }

        /// <summary>
        /// 动作模板
        /// </summary>
        private static string actionConfigTemplate;

        /// <summary>
        /// 动作配置模板
        /// </summary>
        private static string ActionConfigTemplate
        {
            get
            {
                if (actionConfigTemplate == null)
                {
                    actionConfigTemplate = $"{AppContext.BaseDirectory}CodeTemplate\\RoutePermissionConfig\\actionConfig.txt".ReaderFileContent();
                }

                return actionConfigTemplate;
            }
        }

        /// <summary>
        /// 默认生成动作集合，key:动作名，value:动作编码
        /// </summary>
        private static readonly IDictionary<string, string> DEFAULT_BUILDER_ACTIONS = new Dictionary<string, string>()
        {
            { "Get", "Query" },
            { "Count", "Query" },
            { "Exists", "Query" },
            { "PageData", "Query" },
            { "Post", "Add" },
            { "BatchAdd", "Add" },
            { "Put", "Edit" },
            { "Delete", "Remove" },
            { "BatchRemove", "Remove" },
        };

        /// <summary>
        /// 默认分页生成动作集合，key:动作名，value:动作编码
        /// </summary>
        private static readonly IDictionary<string, string> DEFAULT_PAGE_BUILDER_ACTIONS = new Dictionary<string, string>()
        {
            { "Get", "Query" },
            { "Count", "Query" },
            { "Exists", "Query" },
            { "PageData", "Query" },
            { "Page", "Query" },
            { "Post", "Add" },
            { "BatchAdd", "Add" },
            { "Put", "Edit" },
            { "Delete", "Remove" },
            { "BatchRemove", "Remove" },
        };

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns>返回信息</returns>
        public ReturnInfo<bool> Generator(CodeGeneratorParamInfo param)
        {
            var returnInfo = new ReturnInfo<bool>();
            if (param.Tables.IsNullOrCount0())
            {
                return returnInfo;
            }

            // 生成controller
            var constrollerString = new StringBuilder();
            for (var i = 0; i < param.Tables.Count; i++)
            {
                string basicName = param.Tables[i].Name.FristUpper();
                // 生成action
                var actions = param.Tables[i].IsPage ? DEFAULT_PAGE_BUILDER_ACTIONS : DEFAULT_BUILDER_ACTIONS;
                var actionString = new StringBuilder();
                var j = 0;
                foreach (var ac in actions)
                {
                    actionString.Append(ActionConfigTemplate
                        .Replace("|ActionName|", ac.Key)
                        .Replace("|Code|", ac.Value));
                    if (j < actions.Count - 1)
                    {
                        actionString.Append(",");
                    }
                    else
                    {
                        actionString.AppendLine();
                        actionString.Append("        ");
                    }

                    j++;
                }

                constrollerString.Append(ControllerConfigTemplate
                    .Replace("|ControllerName|", basicName)
                    .Replace("|Code|", basicName)
                    .Replace("|ActionConfig|", actionString.ToString()));
                if (i < param.Tables.Count - 1)
                {
                    constrollerString.Append(",");
                }
                else
                {
                    constrollerString.AppendLine();
                    constrollerString.Append("      ");
                }
            }

            var code = RoutePermissionTemplate.Replace("|ControllerConfig|", constrollerString.ToString());
            string folder = $"{Util.FOLDER_ROOT}\\Config";
            folder.CreateNotExistsDirectory();

            $"{folder}\\routePermissionConfig.json".WriteFileContent(code);

            return returnInfo;
        }
    }
}
