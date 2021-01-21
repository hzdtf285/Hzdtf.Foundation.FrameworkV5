using Hzdtf.BasicFunction.Model;
using Hzdtf.BasicFunction.Service.Contract;
using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Model.Return;
using Hzdtf.Workflow.Model.Expand.Diversion;
using Hzdtf.Workflow.Service.Contract.Engine.Diversion;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Attr.ParamAttr;
using Hzdtf.Utility.Model;

namespace Hzdtf.BasicFunction.Workflow
{
    /// <summary>
    /// 查找处理者具体用户
    /// @黄振东
    /// </summary>
    [Inject]
    public partial class FindHandlerConcreteUser : IFindHandlerConcreteUser
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
        /// 根据ID查找用户信息数组
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<FindHandlerUserOutInfo> FindById([DisplayName2("ID"), Id] int id, int userId, string connectionId = null, BasicUserInfo<int> currUser = null)
        {
            ReturnInfo<FindHandlerUserOutInfo> returnInfo = new ReturnInfo<FindHandlerUserOutInfo>();

            ReturnInfo<UserInfo> reUser = UserService.Find(id, connectionId, currUser);
            if (reUser.Failure())
            {
                returnInfo.FromBasic(reUser);
                return returnInfo;
            }
            if (reUser.Data == null || reUser.Data.SystemHide || !reUser.Data.Enabled)
            {
                return returnInfo;
            }

            returnInfo.Data = new FindHandlerUserOutInfo()
            {
                ConcreteCensorship = reUser.Data,
                Users = new UserInfo[] { reUser.Data }
            };

            return returnInfo;
        }
    }
}
