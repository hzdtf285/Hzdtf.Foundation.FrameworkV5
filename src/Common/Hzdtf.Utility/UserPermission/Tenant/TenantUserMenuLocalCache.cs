﻿using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Cache;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hzdtf.Utility.Utils;

namespace Hzdtf.Utility.UserPermission.Merchant
{
    /// <summary>
    /// 租户用户菜单本地缓存
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    [Inject]
    public class TenantUserMenuLocalCache<IdT> : SingleTypeLocalMemoryBase<string, IDictionary<string, string[]>>, ITenantUserMenuPermissionCache<IdT>, ITenantUserMenuPermission<IdT>
    {
        /// <summary>
        /// 字典缓存
        /// </summary>
        private static readonly IDictionary<string, IDictionary<string, string[]>> dicCache = new Dictionary<string, IDictionary<string, string[]>>(20);

        /// <summary>
        /// 最后一次访问时间字典
        /// </summary>
        private static readonly IDictionary<string, DateTime> dicLastAccessTime = new Dictionary<string, DateTime>(20);

        /// <summary>
        /// 同步字典缓存
        /// </summary>
        private static readonly object syncDicCache = new object();

        /// <summary>
        /// 商户用户菜单读取
        /// </summary>
        public ITenantUserMenuReader<IdT> MerchantIdUserMenuReader
        {
            get;
            set;
        }

        /// <summary>
        /// 用户是否拥有权限
        /// </summary>
        /// <param name="merchantId">商户ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="menuCode">菜单编码</param>
        /// <param name="funCode">功能编码</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        public ReturnInfo<bool> UserHavePermission(IdT merchantId, IdT userId, string menuCode, string funCode, CommonUseData comData = null)
        {
            var re = new ReturnInfo<bool>();
            var key = GetKey(merchantId, userId);
            var userMenuFunCodes = Get(key);
            if (userMenuFunCodes == null)
            {
                var reMenuFunCodes = MerchantIdUserMenuReader.GetHavePermissionMenuFunCodes(merchantId, userId, comData);
                if (reMenuFunCodes.Failure())
                {
                    re.FromBasic(reMenuFunCodes);
                    return re;
                }
                if (reMenuFunCodes.Data == null)
                {
                    Add(key, new Dictionary<string, string[]>(0));

                    return re;
                }
                else
                {
                    Add(key, reMenuFunCodes.Data);
                    userMenuFunCodes = reMenuFunCodes.Data;
                }
            }
            else
            {
                dicLastAccessTime[key] = DateTime.Now;
            }

            if (userMenuFunCodes.ContainsKey(menuCode))
            {
                var funCodes = userMenuFunCodes[menuCode];
                if (funCodes.IsNullOrLength0())
                {
                    return re;
                }

                re.Data = funCodes.Where(p => p == funCode).Any();
            }

            return re;
        }

        /// <summary>
        /// 获取时间范围内没有访问的键数组
        /// </summary>
        /// <param name="timeSpan">时间范围</param>
        /// <returns>时间范围内没有访问的键数组</returns>
        public string[] GetWithTSNotAccessKeys(TimeSpan timeSpan)
        {
            return dicLastAccessTime.Where(p => (DateTime.Now - p.Value) >= timeSpan).Select(p => p.Key).ToArray();
        }

        /// <summary>
        /// 移除时间范围内没有访问的用户
        /// </summary>
        /// <param name="timeSpan">时间范围</param>
        /// <returns>是否移除成功</returns>
        public bool RemoveWithTSNotAccess(TimeSpan timeSpan)
        {
            var ids = GetWithTSNotAccessKeys(timeSpan);
            if (ids.IsNullOrLength0())
            {
                return false;
            }

            return Remove(ids);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>是否添加成功</returns>
        public override bool Add(string key, IDictionary<string, string[]> value)
        {
            if (Exists(key))
            {
                return false;
            }
            lock (syncDicCache)
            {
                try
                {
                    dicCache.Add(key, value);
                    dicLastAccessTime.Add(key, DateTime.Now);
                }
                catch (ArgumentException) // 忽略添加相同的键异常，为了预防密集的线程过来
                {
                    System.Console.WriteLine($"{this.GetType().Name}.发生相同添加相同的key异常(程序忽略),key:{key}.value:{value}");
                }

                return true;
            }
        }

        /// <summary>
        /// 初始化缓存
        /// </summary>
        /// <param name="merchantId">商户ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        public BasicReturnInfo InitCache(IdT merchantId, IdT userId, CommonUseData comData = null)
        {
            var key = GetKey(merchantId, userId);
            var result = Get(key);
            if (result == null)
            {
                var reMenuFunCodes = MerchantIdUserMenuReader.GetHavePermissionMenuFunCodes(merchantId, userId, comData);
                if (reMenuFunCodes.Failure())
                {
                    return reMenuFunCodes;
                }
                if (reMenuFunCodes.Data == null)
                {
                    Add(key, new Dictionary<string, string[]>(0));
                }
                else
                {
                    Add(key, reMenuFunCodes.Data);
                }

                return reMenuFunCodes;
            }

            return new BasicReturnInfo();
        }

        /// <summary>
        /// 根据商户ID和用户ID移除缓存
        /// </summary>
        /// <param name="merchantId">商户ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否移除成功</returns>
        public bool RemoveCache(IdT merchantId, IdT userId)
        {
            return Remove(GetKey(merchantId, userId));
        }

        /// <summary>
        /// 根据商户ID和用户ID移除缓存
        /// </summary>
        /// <param name="merchantId">商户ID</param>
        /// <param name="userIds">用户ID数组</param>
        /// <returns>是否移除成功</returns>
        public bool RemoveCache(IdT merchantId, IdT[] userIds)
        {
            if (userIds.IsNullOrLength0())
            {
                return false;
            }

            var keys = new string[userIds.Length];
            for (var i = 0; i < keys.Length; i++)
            {
                keys[i] = GetKey(merchantId, userIds[i]);
            }

            return Remove(keys);
        }

        /// <summary>
        /// 根据商户ID和用户ID移除缓存
        /// </summary>
        /// <param name="merchantIdMapUserIds">商户ID映射用户ID，key：商户ID，value：用户ID</param>
        /// <returns>是否移除成功</returns>
        public bool RemoveCache(params KeyValueInfo<IdT, IdT>[] merchantIdMapUserIds)
        {
            if (merchantIdMapUserIds.IsNullOrLength0())
            {
                return false;
            }

            var keys = new string[merchantIdMapUserIds.Length];
            for (var i = 0; i < keys.Length; i++)
            {
                keys[i] = GetKey(merchantIdMapUserIds[i].Key, merchantIdMapUserIds[i].Value);
            }

            return Remove(keys);
        }

        /// <summary>
        /// 根据商户ID清空缓存
        /// </summary>
        /// <param name="merchantId">商户ID</param>
        /// <returns>是否清空成功</returns>
        public bool ClearCache(IdT merchantId)
        {
            var keys = dicCache.Where(p => p.Key.StartsWith($"{merchantId}.")).Select(p => p.Key).ToArray();
            return Remove(keys);
        }

        /// <summary>
        /// 移除键
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>是否移除成功</returns>
        public override bool Remove(string key)
        {
            if (dicCache.ContainsKey(key))
            {
                dicCache.Remove(key);
            }
            if (dicLastAccessTime.ContainsKey(key))
            {
                dicLastAccessTime.Remove(key);
            }

            return false;
        }

        /// <summary>
        /// 移除键数组
        /// </summary>
        /// <param name="keys">键数组</param>
        /// <returns>是否移除成功</returns>
        public override bool Remove(string[] keys)
        {
            foreach (var key in keys)
            {
                if (dicCache.ContainsKey(key))
                {
                    dicCache.Remove(key);
                }
                if (dicLastAccessTime.ContainsKey(key))
                {
                    dicLastAccessTime.Remove(key);
                }
            }

            return true;
        }

        /// <summary>
        /// 清空
        /// </summary>
        public override void Clear()
        {
            dicCache.Clear();
            dicLastAccessTime.Clear();
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <returns>缓存</returns>
        protected override IDictionary<string, IDictionary<string, string[]>> GetCache() => dicCache;

        /// <summary>
        /// 获取同步的缓存对象，是为了线程安全
        /// </summary>
        /// <returns>同步的缓存对象</returns>
        protected override object GetSyncCache() => syncDicCache;

        /// <summary>
        /// 获取键
        /// </summary>
        /// <param name="merchantId">商户ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>键</returns>
        private string GetKey(IdT merchantId, IdT userId) => $"{merchantId}.{userId}";
    }
}
