using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility
{
    /// <summary>
    /// 公共编码定义
    /// @ 黄振东
    /// </summary>
    public static class CommonCodeDefine
    {
        /// <summary>
        /// 没有权限
        /// </summary>
        public const int NOT_PERMISSION = 400;

        /// <summary>
        /// 没有权限文化键
        /// </summary>
        public const string NOT_PERMISSION_CULTURE_KEY = "Common.Platform.M..NotPermission";

        /// <summary>
        /// 禁止访问文化键
        /// </summary>
        public const string DISABLED_ACCESS_CULTURE_KEY = "Common.Platform.M..DissabledAccess";

        /// <summary>
        /// 操作成功文化键
        /// </summary>
        public const string OPER_SUCCESS_KEY = "Common.Platform.M..OperSuccess";

        /// <summary>
        /// 操作失败文化键
        /// </summary>
        public const string OPER_FAILURE_KEY = "Common.Platform.M..OperFailure";

        /// <summary>
        /// 系统发生异常
        /// </summary>
        public const string SYSTEM_EXCEPTION_KEY = "Common.Platform.M..SystemException";
    }
}
