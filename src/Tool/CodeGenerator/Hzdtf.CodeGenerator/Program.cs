using Autofac;
using Hzdtf.Autofac.Extensions;
using Hzdtf.Utility;
using Hzdtf.Utility.Config.AssemblyConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;

namespace Hzdtf.CodeGenerator
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var assemblyConfigLocalMember = new AssemblyConfigLocalMember();
            assemblyConfigLocalMember.ProtoAssemblyConfigReader = new AssemblyConfigXml();
            var assemblyConfig = assemblyConfigLocalMember.Reader();

            //实例化一个autofac的创建容器
            var builder = new ContainerBuilder();
            builder.UnifiedRegisterAssemblys(new BuilderParam()
            {
                AssemblyServices = assemblyConfig.Services
            });

            App.CurrConfig = ConfigurationFactory.BuilderConfig();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
