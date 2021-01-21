﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.Utility.Vali.Model.Prop
{
    /// <summary>
    /// 最小长度验证
    /// @ 黄振东
    /// </summary>
    public class MinLengthVali : PropValiBase<MinLengthAttribute>
    {
        /// <summary>
        /// 执行验证
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="value">值</param>
        /// <param name="displayName">显示名称</param>
        /// <param name="valiAttr">验证特性</param>
        /// <returns>基本错误消息</returns>
        protected override string ExecVali(object model, object value, string displayName, MinLengthAttribute valiAttr)
        {
            if (value == null)
            {
                return null;
            }

            return value.ToString().Length < valiAttr.Length ? valiAttr.FormatErrorMessage(displayName) : null;
        }
    }
}
