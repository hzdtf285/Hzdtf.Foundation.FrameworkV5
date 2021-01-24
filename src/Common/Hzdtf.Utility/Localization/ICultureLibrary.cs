﻿using Hzdtf.Utility.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.Localization
{
    /// <summary>
    /// 文化库接口
    /// key：键
    /// value： { key：文化名称，value：文化对应的值 }
    /// @ 黄振东
    /// </summary>
    public interface ICultureLibrary
    {
        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        IDictionary<string, IDictionary<string, string>> Reader();
    }
}