﻿using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Attr;
using Newtonsoft.Json;

namespace Hzdtf.Utility.Data
{
    /// <summary>
    /// JSON转换
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class JsonConvert : ISerialization, IDeserialize, IObjectSerialization
    {
        #region ISerialization 接口

        /// <summary>
        /// 将对象序列化为字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>字符串</returns>
        public string Serialize(object obj)
        {
            return obj.ToJsonString();
        }

        #endregion

        #region IDeserialize 接口

        /// <summary>
        /// 将字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="str">字符串</param>
        /// <returns>对象</returns>
        public T Deserialize<T>(string str)
        {
            return str.ToJsonObject<T>();
        }

        #endregion

        #region IObjectSerialization 接口

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>序列化后的数据</returns>
        public object SerializeToObject(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            return obj.ToJsonString();
        }

        /// <summary>
        /// 将对象反序列化为对象
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>对象</returns>
        public object DeserializeToObject(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            return obj.ToString().ToJsonObject<object>();
        }

        /// <summary>
        /// 将对象反序列化为对象
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="type">类型</param>
        /// <returns>对象</returns>
        public object DeserializeToObject(object obj, Type type)
        {
            if (obj == null)
            {
                return null;
            }

            return obj.ToString().ToJsonObject(type);
        }

        #endregion
    }
}
