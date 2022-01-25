using Hzdtf.Persistence.Contract.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Quartz.Model;

namespace Hzdtf.Quartz.Persistence.Contract
{
    /// <summary>
    /// 作业任务持久化接口
    /// @ 黄振东
    /// </summary>
    public partial interface IJobTaskPersistence : IPersistence<int, JobTaskInfo>
    {
    }
}
