using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Hzdtf.Utility.Utils;

namespace Hzdtf.Utility.Safety
{
    /// <summary>
    /// MD5辅助类
    /// @ 黄振东
    /// </summary>
    public static class MD5Util
    {
        /// <summary>
        /// 加密（32位)
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <param name="isToLower">是否转换为小写</param>
        /// <returns>密文</returns>
        public static string Encryption32(string plaintext, bool isToLower = false)
        {
            if (plaintext == null)
            {
                return null;
            }

            byte[] result = Encoding.Default.GetBytes(plaintext);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            string str = BitConverter.ToString(output).Replace("-", null);
            if (isToLower)
            {
                return str.ToLower();
            }

            return str;
        }

        /// <summary>
        /// 加密（16位)
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <param name="isToLower">是否转换为小写</param>
        /// <returns>密文</returns>
        public static string Encryption16(string plaintext, bool isToLower = false)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string result = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(plaintext)), 4, 8);
            result = result.Replace("-", "");
            if (isToLower)
            {
                return result.ToLower();
            }

            return result;
        }

        /// <summary>
        /// MD5流加密输出字节数组
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns>字节数组</returns>
        public static byte[] GenerateMD5Bytes(string text)
        {
            using (var mi = MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(text);
                //开始加密
                return mi.ComputeHash(buffer);
            }
        }

        /// <summary>
        /// 获取字节的MD5值
        /// </summary>
        /// <param name="bytes">字节流</param>
        /// <returns>字节的MD5值</returns>
        public static string GetBytesMD5(this byte[] bytes)
        {
            if (bytes.IsNullOrLength0())
            {
                return null;
            }

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(bytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取文件的MD5值
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>文件的MD5值</returns>
        public static string GetFileMD5(this string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return null;
            }

            return GetStreamMD5(new FileStream(fileName, FileMode.Open));
        }

        /// <summary>
        /// 获取流的MD5值
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="isCloseStream">是否关闭流</param>
        /// <returns>流的MD5值</returns>
        public static string GetStreamMD5(this Stream stream, bool isCloseStream = true)
        {
            if (stream == null)
            {
                return null;
            }

            try
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(stream);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (isCloseStream)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
        }
    }
}
