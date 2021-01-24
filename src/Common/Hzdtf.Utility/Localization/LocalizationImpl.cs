using Hzdtf.Utility.Attr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hzdtf.Utility.Utils;

namespace Hzdtf.Utility.Localization
{
    /// <summary>
    /// 本地化实现
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class LocalizationImpl : ILocalization
    {
        /// <summary>
        /// 文化库
        /// </summary>
        public ICultureLibrary CultureLibrary
        {
            get;
            set;
        }

        /// <summary>
        /// 根据键获取值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>值</returns>
        public string Get(string key, string defaultValue = null)
        {
            var culture = GetCurrCulture();
            if (string.IsNullOrWhiteSpace(culture))
            {
                culture = LocalizationUtil.GetCurrentCulture();
            }
            return Get(key, culture, defaultValue);
        }

        /// <summary>
        /// 根据键获取值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="culture">文化</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>值</returns>
        public string Get(string key, string culture, string defaultValue = null)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("键不能为空");
            }
            if (string.IsNullOrWhiteSpace(culture))
            {
                throw new ArgumentException("文化不能为空");
            }
            if (CultureLibrary == null)
            {
                return defaultValue;
            }

            var dicValues = CultureLibrary.Reader();
            if (dicValues.IsNullOrCount0())
            {
                return defaultValue;
            }
            if (dicValues.ContainsKey(key))
            {
                var keyValues = dicValues[key];
                if (keyValues.IsNullOrCount0())
                {
                    return defaultValue;
                }
                if (keyValues.ContainsKey(culture))
                {
                    return keyValues[culture];
                }
            }

            return defaultValue;
        }

        /// <summary>
        /// 获取当前文化
        /// </summary>
        /// <returns>当前文化</returns>
        protected virtual string GetCurrCulture() => null;
    }
}
