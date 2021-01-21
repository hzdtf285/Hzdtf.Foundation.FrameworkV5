using Hzdtf.Utility.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hzdtf.Utility.Utils;
using Hzdtf.Utility.Enums;

namespace System
{
    /// <summary>
    /// 对象有序连接扩展类
    /// @ 黄振东
    /// </summary>
    public static class ObjectOrderbyConnExtension
    {
        /// <summary>
        /// 转换为有序排列的字符串
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="options">有序选项</param>
        /// <returns>排列后的字符串</returns>
        public static string ToOrderbyString<T>(this T obj, OrderbyOptions options = null)
        {
            if (obj == null)
            {
                return null;
            }

            var props = typeof(T).GetProperties();
            if (props.IsNullOrLength0())
            {
                return null;
            }
            if (options == null)
            {
                options = new OrderbyOptions();
            }

            var length = options.AppendPropValues.IsNullOrCount0() ? props.Length : props.Length + options.AppendPropValues.Count;
            var names = new List<string>(length);
            var values = new List<string>(length);
            foreach (var p in props)
            {
                if (p.CanRead)
                {
                    // 忽略需要忽略的属性
                    if (options.IgnorePropertyNames != null && options.IgnorePropertyNames.Contains(p.Name))
                    {
                        continue;
                    }

                    string value = null;
                    var objValue = p.GetValue(obj);
                    // 如果有自定义转换
                    if (options.ConvertToValue != null)
                    {
                        var kv = options.ConvertToValue(p.Name, objValue);
                        // 已完成自定义转换
                        if (kv != null && kv.Value)
                        {
                            value = kv.Key;
                        }
                        else
                        {
                            if (objValue != null)
                            {
                                value = objValue.ToString();
                            }
                        }
                    }
                    else
                    {
                        if (objValue != null)
                        {
                            value = objValue.ToString();
                        }
                    }
                    
                    if (options.IgnoreNullOrEmpty && string.IsNullOrEmpty(value))
                    {
                        continue;
                    }

                    var name = options.IsHump ? p.Name.FristLower() : p.Name;
                    names.Add(name);
                    values.Add(value);
                }
            }

            return ToOrderbyString(names, values, options);
        }

        /// <summary>
        /// 转换为有序排列的字符串
        /// </summary>
        /// <typeparam name="KeyT">键类型</typeparam>
        /// <typeparam name="ValueT">值类型</typeparam>
        /// <param name="dic">字典</param>
        /// <param name="options">有序选项</param>
        /// <returns>排列后的字符串</returns>
        public static string ToOrderbyString<KeyT, ValueT>(this IDictionary<KeyT, ValueT> dic, OrderbyOptions options = null)
        {
            if (dic.IsNullOrCount0())
            {
                return null;
            }

            if (options == null)
            {
                options = new OrderbyOptions();
            }

            var length = options.AppendPropValues.IsNullOrCount0() ? dic.Count : dic.Count + options.AppendPropValues.Count;
            var names = new List<string>(length);
            var values = new List<string>(length);
            foreach (var item in dic)
            {
                // 忽略需要忽略的属性
                var key = item.Key.ToString();
                if (options.IgnorePropertyNames != null && options.IgnorePropertyNames.Contains(key))
                {
                    continue;
                }

                string value = null;
                var objValue = item.Value;
                // 如果有自定义转换
                if (options.ConvertToValue != null)
                {
                    var kv = options.ConvertToValue(key, objValue);
                    // 已完成自定义转换
                    if (kv != null && kv.Value)
                    {
                        value = kv.Key;
                    }
                    else
                    {
                        if (objValue != null)
                        {
                            value = objValue.ToString();
                        }
                    }
                }
                else
                {
                    if (objValue != null)
                    {
                        value = objValue.ToString();
                    }
                }

                if (options.IgnoreNullOrEmpty && string.IsNullOrEmpty(value))
                {
                    continue;
                }

                var name = options.IsHump ? key.FristLower() : key;
                names.Add(name);
                values.Add(value);
            }

            return ToOrderbyString(names, values, options);
        }

        /// <summary>
        /// 转换为有序排列的字符串
        /// </summary>
        /// <param name="names">名称列表</param>
        /// <param name="values">值列表</param>
        /// <param name="options">有序选项</param>
        /// <returns>排列后的字符串</returns>
        private static string ToOrderbyString(IList<string> names, IList<string> values, OrderbyOptions options)
        {
            if (names.IsNullOrCount0() || values.IsNullOrCount0())
            {
                return null;
            }
            if (!options.AppendPropValues.IsNullOrCount0())
            {
                foreach (var item in options.AppendPropValues)
                {
                    var name = options.IsHump ? item.Key.FristLower() : item.Key;
                    names.Add(name);
                    values.Add(item.Value);
                }
            }

            // 对属性名进行排序
            for (var i = 0; i < names.Count; i++)
            {
                for (var j = i + 1; j < names.Count; j++)
                {
                    if (options.Sort == SortType.ASC)
                    {
                        if (names[i].CompareTo(names[j]) > 0)
                        {
                            var temp = names[i];
                            names[i] = names[j];
                            names[j] = temp;

                            temp = values[i];
                            values[i] = values[j];
                            values[j] = temp;
                        }
                        else
                        {
                            if (names[i].CompareTo(names[j]) < 0)
                            {
                                var temp = names[i];
                                names[i] = names[j];
                                names[j] = temp;

                                temp = values[i];
                                values[i] = values[j];
                                values[j] = temp;
                            }
                        }
                    }
                }
            }

            // 组合字符串
            var result = new StringBuilder();
            for (var i = 0; i < names.Count; i++)
            {
                var str = $"{names[i]}{options.EqualSeparator}{values[i]}{options.Separator}";
                result.Append(str);
            }

            result.Remove(result.Length - 1, 1);

            return result.ToString();
        }
    }

    /// <summary>
    /// 有序选项
    /// @ 黄振东
    /// </summary>
    public class OrderbyOptions
    {
        /// <summary>
        /// 排序，默认为升序
        /// </summary>
        public SortType Sort
        {
            get;
            set;
        } = SortType.ASC;

        /// <summary>
        /// 是否转持驼峰，默认为是
        /// </summary>
        public bool IsHump
        {
            get;
            set;
        } = true;

        /// <summary>
        /// 相等符号，默认为=
        /// </summary>
        public string EqualSeparator
        {
            get;
            set;
        } = "=";

        /// <summary>
        /// 分隔符，默认为
        /// </summary>
        public string Separator
        {
            get;
            set;
        } = "&";

        /// <summary>
        /// 忽略发展名称数组
        /// </summary>
        public string[] IgnorePropertyNames
        {
            get;
            set;
        }

        /// <summary>
        /// 忽略null或空，默认为是
        /// </summary>
        public bool IgnoreNullOrEmpty
        {
            get;
            set;
        } = true;

        /// <summary>
        /// 转换到值回调
        /// 参数1：属性名
        /// 参数2：值
        /// 参数3：返回值：key：自定义转换后的值。value：是否成功转换（如果为否，则会使用默认转换；如果自定义已转换，请设置true）
        /// </summary>
        public Func<string, object, KeyValueInfo<string, bool>> ConvertToValue
        {
            get;
            set;
        }

        /// <summary>
        /// 追加的属性名称字典，key：属性名，value：值
        /// </summary>
        public IDictionary<string, string> AppendPropValues
        {
            get;
            set;
        }
    }
}
