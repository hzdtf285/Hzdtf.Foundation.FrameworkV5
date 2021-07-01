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
        /// 消息包反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="data">数据</param>
        /// <param name="isSetDateTimeToLocalTime">是否设置日期时间为本地时间，默认为否</param>
        /// <param name="toLocalTimeDepth">转换为本地时间深度</param>
        /// <returns>对象</returns>
        public static T MessagePackDeserialize<T>(this byte[] data, bool isSetDateTimeToLocalTime = false, byte toLocalTimeDepth = 3)
        {
            var obj = data.IsNullOrLength0() ? default(T) : MessagePackSerializer.Deserialize<T>(data);
            if (isSetDateTimeToLocalTime)
            {
                obj.SetDateTimeToLocalTime(toLocalTimeDepth);
            }

            return obj;
        }

        /// <summary>
        /// 反序列化为对象
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="type">类型</param>
        /// <param name="isSetDateTimeToLocalTime">是否设置日期时间为本地时间，默认为否</param>
        /// <param name="toLocalTimeDepth">转换为本地时间深度</param>
        /// <returns>对象</returns>
        public static object MessagePackDeserialize(this byte[] data, Type type, bool isSetDateTimeToLocalTime = false, byte toLocalTimeDepth = 3)
        {
            if (data.IsNullOrLength0())
            {
                return null;
            }
            var obj = MessagePackSerializer.Deserialize(type, data);
            if (isSetDateTimeToLocalTime)
            {
                obj.SetDateTimeToLocalTime(toLocalTimeDepth);
            }

            return obj;
        }

        /// <summary>
        /// 判断是否消息包序列化异常
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>是否消息包序列化异常</returns>
        public static bool IsMessagePackSerializationException(this Exception ex)
        {
            return ex is MessagePackSerializationException;
        }
    }
}
