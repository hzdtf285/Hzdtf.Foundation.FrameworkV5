using System;
using Autofac;
using Hzdtf.Autofac.Extensions;
using Hzdtf.BasicFunction.Service.Impl;
using Hzdtf.BasicFunction.Service.Impl.Expand.Attachment;
using Hzdtf.Logger.Contract;
using Hzdtf.Logger.Integration.ENLog;
using Hzdtf.Utility.ApiPermission;
using Hzdtf.Utility.Config.AssemblyConfig;
using Hzdtf.Utility.Data;
using Hzdtf.Utility.Localization;

namespace Hzdtf.Example.WebApp.AppStart
{
    /// <summary>
    /// 依赖注入
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// 注册组件
        /// </summary>
        /// <param name="builder">内容生成</param>
        public static void RegisterComponents(ContainerBuilder builder)
        {
            var assemblyConfigLocalMember = new AssemblyConfigLocalMember();
            assemblyConfigLocalMember.ProtoAssemblyConfigReader = new AssemblyConfigJson();
            var assemblyConfig = assemblyConfigLocalMember.Reader();

            builder.UnifiedRegisterAssemblysForWeb(new BuilderParam()
            {
                AssemblyServices = assemblyConfig.Services,
                IsLoadAutoMapperConfig = assemblyConfig.IsLoadAutoMapperConfig,
                RegisteringServiceAction = () =>
                {
                    //builder.RegisterType<WorkflowConfigCache>().As<IWorkflowConfigReader>().AsSelf().PropertiesAutowired();
                   // builder.RegisterType<WorkflowInitSequenceService>().As<IWorkflowFormService>().AsSelf().PropertiesAutowired();

                    builder.RegisterType<AutofacInstance>().As<IInstance>().AsSelf().PropertiesAutowired().SingleInstance();
                    builder.RegisterType<IntegrationNLog>().As<ILogable>().AsSelf().PropertiesAutowired().SingleInstance();
                    builder.RegisterType<RoutePermissionCache>().As<IReader<RoutePermissionInfo[]>>().AsSelf().PropertiesAutowired().SingleInstance();
                    builder.RegisterType<CultureLibraryCache>().As<ICultureLibrary>().AsSelf().PropertiesAutowired().SingleInstance();
                }
            });//
            builder.RegisterBuildCallback(container =>
            {
                var attachmentService = container.Resolve<AttachmentService>();
                AttachmentOwnerLocalMember attachmentOwnerLocalMember = container.Resolve<AttachmentOwnerLocalMember>();
                attachmentOwnerLocalMember.ProtoAttachmentOwnerReader = container.Resolve<AttachmentOwnerJson>();

                attachmentService.AttachmentOwnerReader = attachmentOwnerLocalMember;
            });
        }
    }
}