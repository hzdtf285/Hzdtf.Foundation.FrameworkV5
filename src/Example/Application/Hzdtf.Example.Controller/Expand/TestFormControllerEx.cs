using Grpc.Net.Client;
using GrpcService2;
using Hzdtf.BasicFunction.Service.Contract;
using Hzdtf.Quartz.Extensions.Scheduler;
using Hzdtf.Quartz.Persistence.Contract;
using Hzdtf.Utility;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using Hzdtf.Utility.RemoteService.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hzdtf.Example.Controller
{
    /// <summary>
    /// 测试表单控制器
    /// @ 黄振东
    /// </summary>
    public partial class TestFormController
    {
        /// <summary>
        /// 用户服务
        /// </summary>
        public IUserService UserService
        {
            get;
            set;
        }

        public ISchedulerWrap Scheduler
        {
            get;
            set;
        }

        public IJobTaskPersistence TaskPersistence
        {
            get;
            set;
        }

        [AllowAnonymous]
        [HttpGet("test")]
        public void Test()
        {
            GRpcChannelUtil.GetGRpcClientFormStrategy<Greeter.GreeterClient>("service1", channel =>
            {
                return new Greeter.GreeterClient(channel);
            }, (client, header) =>
            {
                var msg = client.SayHello(new HelloRequest()
                {
                    Name = "李"
                });
            });
        }

        [AllowAnonymous]
        [HttpGet("pause")]
        public void pause()
        {
            var s = App.GetServiceFromInstance<INativeServicesProvider>();
            
            //Scheduler.PauseJobTaskAsync("作业1").Wait();
        }

        [AllowAnonymous]
        [HttpGet("resume")]
        public void resume()
        {
            Scheduler.ResumeJobTaskAsync("作业1").Wait();
        }


        [AllowAnonymous]
        [HttpGet("remove")]
        public void remove()
        {
            Scheduler.CompletelyRemoveJobTaskAsync("作业3", "分组3").Wait();
        }

        [AllowAnonymous]
        [HttpGet("removeall")]
        public void removeall()
        {
            Scheduler.StopAsync().Wait();
        }

        [AllowAnonymous]
        [HttpGet("re/{num}")]
        public void re(int num)
        {
            var conn = TaskPersistence.NewConnectionId();
            for (var i = 1 + 17897; i <= num; i++)
            {
                var model = new Quartz.Model.JobTaskInfo()
                {
                    Id = i,
                    JtName = "作业" + i,
                    JtGroup = "分组" + i,
                    TriggerCron = "0/30 * * * * ?",
                    JobFullClass = "Hzdtf.Example.Service.Impl,Hzdtf.Example.Service.Impl.Quartz.JobService1",
                    JobParams = new Dictionary<string, string>()
                    {
                        { "j1", "j11" },
                        { "j2", "j12" },
                    },
                        TriggerParams = new Dictionary<string, string>()
                    {
                        { "t1", "t11" },
                        { "t2", "t12" },
                    }
                };

                TaskPersistence.Insert(model, connectionId: conn);
                continue;

                Scheduler.RescheduleJobTaskAsync(model).Wait();
            }
        }

        /// <summary>
        /// 填充页面数据，包含当前用户所拥有的权限功能列表
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="comData">通用数据</param>
        protected override void FillPageData(ReturnInfo<PageInfo<int>> returnInfo, CommonUseData comData = null)
        {
            var re = UserService.QueryPageData<PageInfo<int>>(MenuCode(), () =>
            {
                return returnInfo.Data;
            }, comData: comData);
            returnInfo.FromBasic(re);
        }

        /// <summary>
        /// 创建页面数据
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <returns>页面数据</returns>
        protected override PageInfo<int> CreatePageData(CommonUseData comData = null) => new PageInfo<int>();
    }
}
