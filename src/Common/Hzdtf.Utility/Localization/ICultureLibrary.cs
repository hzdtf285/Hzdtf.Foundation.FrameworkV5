using Hzdtf.Utility.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.Localization
{
    /// <summary>
    /// 文化库接口
    /// key：键
    /// value： { key：文化名称，value：文化对应的值 }
    /// @ 黄振东
    /// </summary>
    public interface ICultureLibrary : IGetable<string, IDictionary<string, string>>, IReader<IDictionary<string, IDictionary<string, string>>>
    {
        /// <summary>
        /// 根据键数组获取值字典
        /// </summary>
        /// <param name="keys">键数组</param>
        /// <returns>值字典</returns>
        IDictionary<string, IDictionary<string, string>> Get(string[] keys);
    }
}
