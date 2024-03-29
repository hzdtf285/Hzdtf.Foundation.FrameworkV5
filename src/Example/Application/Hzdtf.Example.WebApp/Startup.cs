using Autofac;
using Hzdtf.BasicFunction.Controller.Extensions.RoutePermission;
using Hzdtf.Example.Controller;
using Hzdtf.Example.WebApp.AppStart;
using Hzdtf.Logger.Integration.ENLog;
using Hzdtf.Quartz.Extensions;
using Hzdtf.Quartz.File;
using Hzdtf.Quartz.Persistence.Contract;
using Hzdtf.Utility;
using Hzdtf.Utility.RemoteService.Provider;
using Hzdtf.Utility.RoutePermission;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace Hzdtf.Example.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            App.CurrConfig = configuration;
            App.IsReturnCulture = true;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddControllersAsServices()
                .AddDefaultJsonOptions();
            services.AddControllers()
                .AddControllersAsServices();
            services.AddSession();

            services.AddLogging(builder =>
            {
                builder.AddHzdtf(options =>
                {
                    options.ProtoLog = new IntegrationNLog();
                });
            });

            services.AddIdentityAuth<int>(options =>
            {
                options.LocalAuth.LoginPath = "/login.html";
            });

            services.AddTheReuestOperation();
            services.AddRequestLog();
            services.AddApiExceptionHandle();
            services.AddRoutePermission();

            if (Configuration.GetValue<bool>("Swagger:Enabled"))
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo()
                    {
                        Title = "样例系统接口",
                        Version = "v1"
                    });

                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Hzdtf.Utility.xml"));
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Hzdtf.BasicController.xml"));
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Hzdtf.BasicFunction.Model.xml"));
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Hzdtf.BasicFunction.Controller.xml"));
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Hzdtf.Workflow.Model.xml"));
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Hzdtf.Workflow.Controller.xml"));
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Hzdtf.Example.Controller.xml"));
                });
            }

            services.AddQuartz(() =>
            {
                QuartzStaticConfig.JobHandleExceptionAssembly = "Hzdtf.Example.Service.Impl";
            });

            //services.AddSingleton<IServicesProvider, TestServiceProvider>();
            services.AddUnityServicesBuilderConfigure(options =>
            {
                options.NativeServicesProviderType = typeof(TestServiceProvider);
                options.ServicesProviderType = typeof(TestServiceProvider);
                options.ServicesOptions = new Utility.RemoteService.Options.UnityServicesOptions()
                {
                    GlobalConfiguration = new Utility.RemoteService.Options.GlobalServicesOptions()
                    {
                        Sheme = "http",
                        LoadBalanceMode = Utility.LoadBalance.LoadBalanceMode.ROUND_ROBIN,
                    },
                    Services = new Utility.RemoteService.Options.ServicesOptions[]
                    {
                        new Utility.RemoteService.Options.ServicesOptions()
                        {
                            ServiceName = "service1",
                        }
                    }
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            App.Instance = app.ApplicationServices;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            if (Configuration.GetValue<bool>("Swagger:Enabled"))
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "样例系统 API");
                });
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseQuartz(app.ApplicationServices, lifetime);

            app.UseTheReuestOperation();
            app.UseCulture();
            app.UseRoutePermission<RoutePermissionMiddleware>();
            app.UseRequestLog();
            app.UseIdentityAuth<int>();

            app.UseApiExceptionHandle();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            OtherConfig.Init();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            DependencyInjection.RegisterComponents(builder);
        }
    }
}
