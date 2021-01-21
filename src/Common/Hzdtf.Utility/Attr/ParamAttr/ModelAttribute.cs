﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.Utility.Attr.ParamAttr
{
    /// <summary>
    /// 模型特性
    /// @ 黄振东
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ModelAttribute : ValidationAttribute
    {
    }
}
