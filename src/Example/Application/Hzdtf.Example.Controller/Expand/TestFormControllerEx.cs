using Hzdtf.BasicFunction.Service.Contract;
using Hzdtf.Quartz.Extensions.Scheduler;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
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

        [AllowAnonymous]
        [HttpGet("test")]
        public async Task Test()
        {
            await Scheduler.RescheduleJobTaskAsync("0/10 * * * * ?", "作业1", "分组1");
            //Scheduler.StopJobTaskAsync("作业1", "分组1").Wait();
        }

        [AllowAnonymous]
        [HttpGet("pause")]
        public void pause()
        {
            Scheduler.PauseJobTaskAsync("作业1").Wait();
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
        [HttpGet("re")]
        public void re()
        {
            Scheduler.RescheduleJobTaskAsync(new Quartz.Model.JobTaskInfo()
            {
                Id = 3,
                JtName = "作业3",
                JtGroup = "分组3",
                TriggerCron = "0/10 * * * * ?",
                JobFullClass = "Hzdtf.Example.Service.Impl,Hzdtf.Example.Service.Impl.Quartz.JobService3",
                JobParams = new Dictionary<string, string>()
                {
                    { "j1", "j11" },
                    { "j2", "j12" },
                },
                TriggerParams = new Dictionary<string, string>()
                {
                    { "t1", "t11" },
                    { "t2", "t12" },
                },
                SuccessedRemove= true,
            }).Wait();
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
