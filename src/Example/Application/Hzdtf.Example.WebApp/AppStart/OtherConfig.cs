using Hzdtf.Utility;
using Hzdtf.Utility.AutoMapperExtensions;
using System;

namespace Hzdtf.Example.WebApp.AppStart
{
    /// <summary>
    /// 其他配置
    /// </summary>
    public static class OtherConfig
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            //UserWorkflowUtil.InitValiUserHandleVali();

            if (App.CurrConfig["Page:MaxPageSize"] != null)
            {
                App.MaxPageSize = Convert.ToInt32(App.CurrConfig["Page:MaxPageSize"]);
            }

            AutoMapperUtil.Builder();
        }
    }
}
