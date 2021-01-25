using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.Localization
{
    /// <summary>
    /// 本地化接口
    /// @ 黄振东
    /// </summary>
    public interface ILocalization
    {
        /// <summary>
        /// 根据键获取值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>值</returns>
        string Get(string key, string defaultValue = null);

        /// <summary>
        /// 根据键获取值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="culture">文化</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>值</returns>
        string Get(string key, string culture, string defaultValue = null);

        /// <summary>
        /// 根据键字典获取值字典
        /// </summary>
        /// <param name="keyDefaultValues">键字典，key：键，value：默认值</param>
        /// <param name="culture">文化</param>
        /// <returns>值字典</returns>
        IDictionary<string, string> Get(IDictionary<string, string> keyDefaultValues, string culture = null);
    }
}
