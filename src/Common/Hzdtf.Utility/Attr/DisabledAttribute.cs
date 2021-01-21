﻿using System; 
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Attr
{
    /// <summary>
    /// 禁止特性
    /// @ 黄振东
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DisabledAttribute : Attribute
    {
    }
}
