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

            var dicValues = CultureLibrary.Get(key);
            if (dicValues.IsNullOrCount0())
            {
                return defaultValue;
            }
            if (dicValues.ContainsKey(culture))
            {
                return dicValues[culture];
            }

            return defaultValue;
        }

        /// <summary>
        /// 根据键字典获取值字典
        /// </summary>
        /// <param name="keyDefaultValues">键字典，key：键，value：默认值</param>
        /// <param name="culture">文化</param>
        /// <returns>值字典</returns>
        public IDictionary<string, string> Get(IDictionary<string, string> keyDefaultValues, string culture = null)
        {
            if (string.IsNullOrWhiteSpace(culture))
            {
                culture = GetCurrCulture();
                if (string.IsNullOrWhiteSpace(culture))
                {
                    culture = LocalizationUtil.GetCurrentCulture();
                }
            }
            if (string.IsNullOrWhiteSpace(culture))
            {
                return keyDefaultValues;
            }

            var keys = keyDefaultValues.Keys.ToArray();
            var dicValues = CultureLibrary.Get(keys);
            if (dicValues.IsNullOrCount0())
            {
                return keyDefaultValues;
            }

            var result = new Dictionary<string, string>(keyDefaultValues.Count);
            foreach (var kv in keyDefaultValues)
            {
                string value = null;
                if (dicValues.ContainsKey(kv.Key))
                {
                    var val = dicValues[kv.Key];
                    if (val.IsNullOrCount0() || !val.ContainsKey(culture))
                    {
                        value = kv.Value;
                    }
                    else
                    {
                        value = val[culture];
                    }
                }
                else
                {
                    value = kv.Value;
                }

                result.Add(kv.Key, value);
            }

            return result;
        }

        /// <summary>
        /// 获取当前文化
        /// </summary>
        /// <returns>当前文化</returns>
        protected virtual string GetCurrCulture() => null;
    }
}
