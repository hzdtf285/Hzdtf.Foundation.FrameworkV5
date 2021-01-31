using Hzdtf.AUC.Contract.User;
using Hzdtf.BasicFunction.Model;
using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Attr.ParamAttr;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.BasicFunction.Service.Impl.Expand.User
{
    /// <summary>
    /// 用户验证服务
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class UserValiService : IUserVali<int, BasicUserInfo<int>>
    {
        #region 属性与字段

        /// <summary>
        /// 原生用户验证
        /// </summary>
        public IUserVali<int, UserInfo> ProtoUserVali
        {
            get;
            set;
        }

        #endregion

        #region IUserVali<BasicUserInfo> 接口

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="password">密码</param>
        /// <param name="otherData">其他数据</param>
        /// <returns>返回信息</returns>
        [ProcTrackLog(IgnoreParamValues = true)]
        public virtual ReturnInfo<BasicUserInfo<int>> Vali([DisplayName2("用户"), Required] string user, [DisplayName2("密码"), Required] string password, object otherData = null)
        {
            ReturnInfo<BasicUserInfo<int>> returnInfo = new ReturnInfo<BasicUserInfo<int>>();
            var re = ProtoUserVali.Vali(user, password);
            returnInfo.FromBasic(re);
            if (returnInfo.Success())
            {
                returnInfo.Data = re.Data;
            }

            return returnInfo;
        }

        #endregion
    }
}
