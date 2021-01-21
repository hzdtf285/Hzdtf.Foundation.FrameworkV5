using Hzdtf.CodeGenerator.Contract;
using Hzdtf.CodeGenerator.Contract.Function;
using Hzdtf.CodeGenerator.Impl.Function;
using Hzdtf.CodeGenerator.Model;
using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Utils;

namespace Hzdtf.CodeGenerator.Impl
{
    /// <summary>
    /// 代码生成服务
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class CodeGeneratorService : ICodeGeneratorService
    {
        /// <summary>
        /// 属性生成服务
        /// </summary>
        public ModelGeneratorService ModelGeneratorService
        {
            get;
            set;
        }

        /// <summary>
        /// 持久生成服务
        /// </summary>
        public PersistenceGeneratorService PersistenceGeneratorService
        {
            get;
            set;
        }

        /// <summary>
        /// 服务生成服务
        /// </summary>
        public ServiceGeneratorService ServiceGeneratorService
        {
            get;
            set;
        }

        /// <summary>
        /// 控制器生成服务
        /// </summary>
        public ControllerGeneratorService ControllerGeneratorService
        {
            get;
            set;
        }

        /// <summary>
        /// 路由权限配置生成服务
        /// </summary>
        public RoutePermissionConfigGeneratorService PermissionConfigGeneratorService
        {
            get;
            set;
        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<bool> Generator(CodeGeneratorParamInfo param)
        {
            Util.FOLDER_ROOT.DeleteDirectory();

            ReturnInfo<bool> returnInfo = null;
            IList<IFunctionGeneratorService> services = GetFunctionServices(param.FunctionTypes);

            foreach (IFunctionGeneratorService s in services)
            {
                returnInfo = s.Generator(param);
            }

            return returnInfo;
        }

        /// <summary>
        /// 获取功能生成服务列表
        /// </summary>
        /// <param name="functionTypes">功能类型集合</param>
        /// <returns>功能生成服务列表</returns>
        private IList<IFunctionGeneratorService> GetFunctionServices(FunctionType[] functionTypes)
        {
            IList<IFunctionGeneratorService> services = new List<IFunctionGeneratorService>();
            foreach (FunctionType f in functionTypes)
            {
                switch (f)
                {
                    case FunctionType.MODEL:
                        services.Add(ModelGeneratorService);

                        break;

                    case FunctionType.PERSISTENCE:
                        services.Add(PersistenceGeneratorService);

                        break;

                    case FunctionType.SERVICE:
                        services.Add(ServiceGeneratorService);

                        break;

                    case FunctionType.CONTROLLER:
                        services.Add(ControllerGeneratorService);

                        break;

                    case FunctionType.ROUTE_PERMISSION_CONFIG:
                        services.Add(PermissionConfigGeneratorService);

                        break;
                }
            }

            return services;
        }
    }
}
