using Hzdtf.Utility.Utils;
using MessagePack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace System
{
    /// <summary>
    /// 消息包扩展类
    /// @ 黄振东
    /// </summary>
    public static class MessagePackExtensions
    {
        /// <summary>
        /// 消息包序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>序列化后的数据</returns>
        public static byte[] MessagePackSerialize(this object obj) => obj != null ? MessagePackSerializer.Serialize(obj) : null;

        /// <summary>
        /// 消息包反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="data">数据</param>
        /// <returns>对象</returns>
        public static T MessagePackDeserialize<T>(this byte[] data) => data.IsNullOrLength0() ? default(T) : MessagePackSerializer.Deserialize<T>(data);

        /// <summary>
        /// 反序列化为对象
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="type">类型</param>
        /// <returns>对象</returns>
        public static object MessagePackDeserialize(this byte[] data, Type type)
        {
            if (data.IsNullOrLength0())
            {
                return null;
            }

            using (var stream = new MemoryStream(data))
            {
                return MessagePackSerializer.Deserialize(type, stream);
            }
        }
    }
}
