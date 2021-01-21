﻿using Hzdtf.Mini;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Hzdtf.Utility.Safety
{
    /// <summary>
    /// DES辅助类
    /// @ 黄振东
    /// </summary>
    public static class DESUtil
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <param name="key">KEY</param>
        /// <param name="iv">向量</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string plaintext, string key = null, string iv = null)
        {
            if (key == null)
            {
                key = Eternity.STD_K;
            }
            if (iv == null)
            {
                iv = Eternity.STD_V;
            }
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = null;
            try
            {
                byte[] btKey = Encoding.UTF8.GetBytes(key);
                byte[] btIV = Encoding.UTF8.GetBytes(iv);

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                byte[] inData = Encoding.UTF8.GetBytes(plaintext);
                cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write);
                cs.Write(inData, 0, inData.Length);

                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (cs != null)
                {
                    cs.Close();
                }

                ms.Close();
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="ciphertext ">密文</param>
        /// <param name="key">KEY</param>
        /// <param name="iv">向量</param>
        /// <returns>解密后的字符串</returns>
        public static string Decrypt(string ciphertext, string key = null, string iv = null)
        {
            if (key == null)
            {
                key = Eternity.STD_K;
            }
            if (iv == null)
            {
                iv = Eternity.STD_V;
            }

            byte[] btKey = Encoding.UTF8.GetBytes(key);

            byte[] btIV = Encoding.UTF8.GetBytes(iv);

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Convert.FromBase64String(ciphertext);
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);

                        cs.FlushFinalBlock();
                    }

                    return Encoding.UTF8.GetString(ms.ToArray());
                }
                catch
                {
                    return ciphertext;
                }
            }
        }
    }
}
