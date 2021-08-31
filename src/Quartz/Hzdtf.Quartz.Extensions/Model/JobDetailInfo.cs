using Hzdtf.Utility.Model;
using Hzdtf.Utility.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Quartz.Extensions.Model
{
    /// <summary>
    /// 作业明细信息
    /// @ 黄振东
    /// </summary>
    public class JobDetailInfo : SimpleInfo<int>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get;
            set;
        } = StringUtil.NewShortGuid();

        /// <summary>
        /// 分组
        /// </summary>
        public string Group
        {
            get;
            set;
        } = StringUtil.NewShortGuid();

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// 作业全路径类
        /// </summary>
        public string JobFullClass
        {
            get;
            set;
        }

        /// <summary>
        /// 参数
        /// </summary>
        public IDictionary<string, string> Params
        {
            get;
            set;
        }
    }
}
