﻿using Hzdtf.Utility.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.AUC.Contract.IdentityAuth
{
    /// <summary>
    /// 身份退出接口
    /// @ 黄振东
    /// </summary>
    public interface IIdentityExit
    {
        /// <summary>
        /// 退出
        /// </summary>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> Exit();
    }
}
