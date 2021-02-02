using Hzdtf.BasicFunction.Model;
using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Utils;
using Hzdtf.Utility.Model.Page;
using System.Threading.Tasks;
using Hzdtf.BasicFunction.Service.Contract;
using Hzdtf.Utility;

namespace Hzdtf.BasicFunction.Controller
{
    /// <summary>
    /// 附件控制器
    /// @ 黄振东
    /// </summary>
    public partial class AttachmentController
    {
        /// <summary>
        /// 用户服务
        /// </summary>
        public IUserService UserService
        {
            get;
            set;
        }

        /// <summary>
        /// 根据ID查找附件
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回信息</returns>
        [HttpGet("{id}")]
        public override async Task<ReturnInfo<AttachmentInfo>> Get(int id)
        {
            ReturnInfo<AttachmentInfo> returnInfo = await Service.FindAsync(id);
            if (returnInfo.Failure())
            {
                return returnInfo;
            }
            ReturnInfo<bool> reInfo = FilterDownLoadPermissionFileAddress(returnInfo.Data);
            if (reInfo.Failure())
            {
                returnInfo.FromBasic(reInfo);
            }

            return returnInfo;
        }

        /// <summary>
        /// 执行分页获取数据
        /// </summary>
        /// <returns>分页返回信息</returns>
        [HttpGet]
        public override async Task<object> Page()
        {
            ReturnInfo<PagingInfo<AttachmentInfo>> returnInfo = await DoPageAsync();
            ReturnInfo<bool> reInfo = FilterDownLoadPermissionFileAddress(returnInfo.Data.Rows);
            if (reInfo.Failure())
            {
                returnInfo.FromBasic(reInfo);
            }

            return PagingReturnConvert.Convert<AttachmentInfo>(returnInfo);
        }

        /// <summary>
        /// 根据附件归属条件获取附件列表
        /// </summary>
        /// <returns>返回信息</returns>
        [HttpGet("List")]
        public virtual async Task<ReturnInfo<IList<AttachmentInfo>>> List()
        {
            return await Task<ReturnInfo<IList<AttachmentInfo>>>.Run(() =>
            {
                IDictionary<string, string> dicParams = Request.QueryString.Value.ToDictionaryFromUrlParams();
                ReturnInfo<IList<AttachmentInfo>> returnInfo = Service.QueryByOwner(Convert.ToInt16(dicParams.GetValue("ownerType")), Convert.ToInt32(dicParams.GetValue("ownerId")), dicParams.GetValue("blurTitle"));
                if (returnInfo.Success())
                {
                    ReturnInfo<bool> reInfo = FilterDownLoadPermissionFileAddress(returnInfo.Data);
                    if (reInfo.Failure())
                    {
                        returnInfo.FromBasic(reInfo);
                    }
                }

                return returnInfo;
            });            
        }

        /// <summary>
        /// 根据归属移除附件
        /// </summary>
        /// <param name="ownerType">归属类型</param>
        /// <param name="ownerId">归属ID</param>
        /// <returns>返回信息</returns>
        [HttpDelete("DeleteByOwner/{ownerType}/{ownerId}")]
        [Function(FunCodeDefine.REMOVE_CODE)]
        public virtual async Task<ReturnInfo<bool>> DeleteByOwner(short ownerType, int ownerId)
        {
            return await Task<ReturnInfo<bool>>.Run(() =>
            {
                return Service.RemoveByOwner(ownerType, ownerId);
            });            
        }

        /// <summary>
        /// 过滤下载权限的文件地址
        /// </summary>
        /// <param name="attachments">附件列表</param>
        /// <returns>返回信息</returns>
        protected ReturnInfo<bool> FilterDownLoadPermissionFileAddress(IList<AttachmentInfo> attachments)
        {
            if (attachments.IsNullOrCount0())
            {
                return new ReturnInfo<bool>();
            }

            ReturnInfo<bool> returnInfo = UserService.IsCurrUserPermission(MenuCode(), FunCodeDefine.DOWNLOAD_CODE);
            if (returnInfo.Code == CommonCodeDefine.NOT_PERMISSION)
            {
                foreach (var a in attachments)
                {
                    a.FileAddress = null;
                }
                returnInfo.SetSuccessMsg("操作成功");
            }

            return returnInfo;
        }

        /// <summary>
        /// 过滤下载权限的文件地址
        /// </summary>
        /// <param name="attachment">附件</param>
        /// <returns>返回信息</returns>
        protected ReturnInfo<bool> FilterDownLoadPermissionFileAddress(AttachmentInfo attachment)
        {
            if (attachment == null)
            {
                return new ReturnInfo<bool>();
            }

            ReturnInfo<bool> returnInfo = UserService.IsCurrUserPermission(MenuCode(), FunCodeDefine.DOWNLOAD_CODE);
            if (returnInfo.Code == CommonCodeDefine.NOT_PERMISSION)
            {
                attachment.FileAddress = null;
                returnInfo.SetSuccessMsg("操作成功");
            }

            return returnInfo;
        }

        /// <summary>
        /// 填充页面数据，包含当前用户所拥有的权限功能列表
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        protected override void FillPageData(ReturnInfo<PageInfo<int>> returnInfo)
        {
            var re = UserService.QueryPageData<PageInfo<int>>(MenuCode(), () =>
            {
                return returnInfo.Data;
            });
            returnInfo.FromBasic(re);
        }

        /// <summary>
        /// 创建页面数据
        /// </summary>
        /// <returns>页面数据</returns>
        protected override PageInfo<int> CreatePageData() => new PageInfo<int>();
    }
}
