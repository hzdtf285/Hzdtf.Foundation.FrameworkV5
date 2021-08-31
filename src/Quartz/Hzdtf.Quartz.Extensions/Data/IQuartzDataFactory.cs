using Hzdtf.Quartz.Extensions.Model;
using Hzdtf.Utility.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Quartz.Extensions.Data
{
    /// <summary>
    /// 时钟数据工厂接口
    /// @ 黄振东
    /// </summary>
    public interface IQuartzDataFactory : IGeneralFactory<IList<JobTaskInfo>>
    {
    }
}
