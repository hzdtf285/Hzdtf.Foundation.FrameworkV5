﻿using Hzdtf.BasicFunction.Model.Expand.Attachment;
using Hzdtf.BasicFunction.Service.Contract.Expand.Attachment;
using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Hzdtf.BasicFunction.Service.Impl.Expand.Attachment
{
    /// <summary>
    /// 附件归属XML
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class AttachmentOwnerXml : IAttachmentOwnerReader
    {
        #region 属性与字段

        /// <summary>
        /// XML文件名
        /// </summary>
        private readonly string xmlFileName;

        #endregion

        #region 初始化

        /// <summary>
        /// 构造方法
        /// </summary>
        public AttachmentOwnerXml()
            : this($"{AppContext.BaseDirectory}Config/attachmentOwnerConfig.xml")
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="xmlFileName">xml文件名</param>
        public AttachmentOwnerXml(string xmlFileName) => this.xmlFileName = xmlFileName;

        #endregion

        #region IAttachmentOwnerReader 接口

        /// <summary>
        /// 根据归属类型读取附件归属信息
        /// </summary>
        /// <param name="type">归属类型</param>
        /// <param name="comData">通用数据</param>
        /// <returns>附件归属信息</returns>
        public AttachmentOwnerInfo ReaderByOwnerType(short type, CommonUseData comData = null)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFileName);

            XmlNode node = xmlDoc.SelectSingleNode($"OwnerTypes/OwnerType[@Id='{type}']");
            if (node == null)
            {
                return null;
            }

            return new AttachmentOwnerInfo()
            {
                OwnerType = type,
                AllowExpands = node.GetChildNodeInnerTextByNode("AllowExpands"),
                MaxSize = Convert.ToSingle(node.GetChildNodeInnerTextByNode("MaxSize"))
            };
        }

        #endregion
    }
}
