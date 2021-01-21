using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.CodeGenerator.Model
{
    /// <summary>
    /// 功能类型枚举
    /// @ 黄振东
    /// </summary>
    public enum FunctionType : byte
    {
        /// <summary>
        /// 模型
        /// </summary>
        MODEL = 1,

        /// <summary>
        /// 持久
        /// </summary>
        PERSISTENCE = 2,

        /// <summary>
        /// 服务
        /// </summary>
        SERVICE = 3,

        /// <summary>
        /// 控制器
        /// </summary>
        CONTROLLER = 4,

        /// <summary>
        /// 路由权限配置
        /// </summary>
        ROUTE_PERMISSION_CONFIG = 5
    }
}
