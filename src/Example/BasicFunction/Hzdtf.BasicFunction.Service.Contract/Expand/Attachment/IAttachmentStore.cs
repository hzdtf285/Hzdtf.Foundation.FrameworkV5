using Hzdtf.BasicFunction.Model.Expand.Attachment;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Service.Contract.Expand.Attachment
{
    /// <summary>
    /// 附件存储接口
    /// @ 黄振东
    /// </summary>
    public interface IAttachmentStore
    {
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <param name="attachmentStream">附件流</param>
        /// <returns>返回信息</returns>
        ReturnInfo<IList<string>> Upload(CommonUseData comData = null, params AttachmentStreamInfo[] attachmentStream);

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <param name="fileAddress">文件地址</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> Remove(CommonUseData comData = null, params string[] fileAddress);
    }
}
