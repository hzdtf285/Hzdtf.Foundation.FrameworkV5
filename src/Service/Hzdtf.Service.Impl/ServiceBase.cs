using Hzdtf.Service.Contract;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Page;
using Hzdtf.Utility.Model.Return;
using System;
using System.Collections.Generic;
using Hzdtf.Persistence.Contract.Data;
using Hzdtf.Persistence.Contract.Basic;
using Hzdtf.Utility.Data;
using Hzdtf.Utility.Enums;
using Hzdtf.Utility.Utils;
using Hzdtf.Utility.Attr.ParamAttr;
using System.ComponentModel.DataAnnotations;
using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Model.Identitys;
using Hzdtf.Utility.Localization;
using System.Text;

namespace Hzdtf.Service.Impl
{
    /// <summary>
    /// 服务基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    /// <typeparam name="ModelT">模型类型</typeparam>
    /// <typeparam name="PersistenceT">持久化类型</typeparam>
    public abstract partial class ServiceBase<IdT, ModelT, PersistenceT> : BasicServiceBase, IService<IdT, ModelT>, IGetObject<IPersistenceConnection> 
        where ModelT : SimpleInfo<IdT>
        where PersistenceT : IPersistence<IdT, ModelT>
    {
        #region 属性与字段

        /// <summary>
        /// 持久化
        /// </summary>
        public PersistenceT Persistence
        {
            get;
            set;
        }

        /// <summary>
        /// ID
        /// </summary>
        public IIdentity<IdT> Identity
        {
            get;
            set;
        }

        /// <summary>
        /// 本地化
        /// </summary>
        public ILocalization Localize
        {
            get;
            set;
        }

        #endregion

        #region IGetObject<IPersistenceConnection>

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns>对象</returns>
        public IPersistenceConnection Get() => Persistence;

        #endregion

        #region IService<ModelT> 接口

        #region 读取

        /// <summary>
        /// 根据ID查找模型前事件
        /// </summary>
        public event Action<ReturnInfo<ModelT>, IdT, CommonUseData, string> Finding;

        /// <summary>
        /// 执行根据ID查找模型前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnFinding(ReturnInfo<ModelT> returnInfo, IdT id, CommonUseData comData = null, string connectionId = null)
        {
            if (Finding != null)
            {
                Finding(returnInfo, id, comData, connectionId);
            }
        }

        /// <summary>
        /// 根据ID查找模型后事件
        /// </summary>
        public event Action<ReturnInfo<ModelT>, IdT, CommonUseData, string> Finded;

        /// <summary>
        /// 执行根据ID查找模型后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnFinded(ReturnInfo<ModelT> returnInfo, IdT id, CommonUseData comData = null, string connectionId = null)
        {
            if (Finded != null)
            {
                Finded(returnInfo, id, comData, connectionId);
            }
        }

        /// <summary>
        /// 根据ID查找模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<ModelT> Find([Id] IdT id, CommonUseData comData = null, string connectionId = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<ModelT> returnInfo = new ReturnInfo<ModelT>();
                BeforeFind(returnInfo, id, ref connectionId, comData);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnFinding(returnInfo, id, comData, connectionId: connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<ModelT>((reInfo) =>
                {
                    return Persistence.Select(id, comData: comData, connectionId: connectionId);
                }, returnInfo);

                AfterFind(returnInfo, id, ref connectionId, comData);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnFinded(returnInfo, id, comData, connectionId: connectionId);

                return returnInfo;
            }
            catch (Exception ex)
            {
                isClose = true;
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (isClose)
                {
                    Persistence.Release(connectionId);
                }
            }
        }

        /// <summary>
        /// 根据ID查找模型列表前事件
        /// </summary>
        public event Action<ReturnInfo<IList<ModelT>>, IdT[], CommonUseData, string> Findsing;

        /// <summary>
        /// 执行根据ID查找模型前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="ids">ID集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnFindsing(ReturnInfo<IList<ModelT>> returnInfo, IdT[] ids, CommonUseData comData = null, string connectionId = null)
        {
            if (Findsing != null)
            {
                Findsing(returnInfo, ids, comData, connectionId);
            }
        }

        /// <summary>
        /// 根据ID查找模型列表后事件
        /// </summary>
        public event Action<ReturnInfo<IList<ModelT>>, IdT[], CommonUseData, string> Findsed;

        /// <summary>
        /// 执行根据ID查找模型后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        protected void OnFindsed(ReturnInfo<IList<ModelT>> returnInfo, IdT[] ids, CommonUseData comData = null, string connectionId = null)
        {
            if (Findsed != null)
            {
                Findsed(returnInfo, ids, comData, connectionId);
            }
        }

        /// <summary>
        /// 根据ID查找模型列表
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<IList<ModelT>> Find([DisplayName2("Id集合"), ArrayNotEmpty] IdT[] ids, CommonUseData comData = null, string connectionId = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<IList<ModelT>> returnInfo = new ReturnInfo<IList<ModelT>>();
                BeforeFind(returnInfo, ids, ref connectionId, comData);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnFindsing(returnInfo, ids, comData, connectionId: connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<IList<ModelT>>((reInfo) =>
                {
                    return Persistence.Select(ids, comData: comData, connectionId: connectionId);
                }, returnInfo);

                AfterFind(returnInfo, ids, ref connectionId, comData);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnFindsed(returnInfo, ids, comData, connectionId: connectionId);

                return returnInfo;
            }
            catch (Exception ex)
            {
                isClose = true;
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (isClose)
                {
                    Persistence.Release(connectionId);
                }
            }
        }

        /// <summary>
        /// 根据ID判断模型是否存在前事件
        /// </summary>
        public event Action<ReturnInfo<bool>, IdT, CommonUseData, string> Existsing;

        /// <summary>
        /// 执行根据ID判断模型是否存在前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnExistsing(ReturnInfo<bool> returnInfo, IdT id, CommonUseData comData = null, string connectionId = null)
        {
            if (Existsing != null)
            {
                Existsing(returnInfo, id, comData, connectionId);
            }
        }

        /// <summary>
        /// 根据ID判断模型是否存在后事件
        /// </summary>
        public event Action<ReturnInfo<bool>, IdT, CommonUseData, string> Existsed;

        /// <summary>
        /// 执行根据ID判断模型是否存在后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnExistsed(ReturnInfo<bool> returnInfo, IdT id, CommonUseData comData = null, string connectionId = null)
        {
            if (Existsed != null)
            {
                Existsed(returnInfo, id, comData, connectionId);
            }
        }

        /// <summary>
        /// 根据ID判断模型是否存在
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<bool> Exists([Id] IdT id, CommonUseData comData = null, string connectionId = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
                BeforeExists(returnInfo, id, ref connectionId, comData);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnExistsing(returnInfo, id, comData, connectionId: connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<bool>((reInfo) =>
                {
                    return Persistence.Count(id, comData: comData, connectionId: connectionId) > 0;
                }, returnInfo);

                AfterExists(returnInfo, id, ref connectionId, comData);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnExistsed(returnInfo, id, comData, connectionId: connectionId);

                return returnInfo;
            }
            catch (Exception ex)
            {
                isClose = true;
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (isClose)
                {
                    Persistence.Release(connectionId);
                }
            }
        }

        /// <summary>
        /// 统计模型数前事件
        /// </summary>
        public event Action<ReturnInfo<int>, CommonUseData, string> Counting;

        /// <summary>
        /// 执行统计模型数前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnCounting(ReturnInfo<int> returnInfo, CommonUseData comData = null, string connectionId = null)
        {
            if (Counting != null)
            {
                Counting(returnInfo, comData, connectionId);
            }
        }

        /// <summary>
        /// 统计模型数后事件
        /// </summary>
        public event Action<ReturnInfo<int>, CommonUseData, string> Counted;

        /// <summary>
        /// 执行统计模型数后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnCounted(ReturnInfo<int> returnInfo, CommonUseData comData = null, string connectionId = null)
        {
            if (Counted != null)
            {
                Counted(returnInfo, comData, connectionId);
            }
        }

        /// <summary>
        /// 统计模型数
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<int> Count(CommonUseData comData = null, string connectionId = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<int> returnInfo = new ReturnInfo<int>();
                BeforeCount(returnInfo, ref connectionId, comData);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnCounting(returnInfo, comData, connectionId: connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<int>((reInfo) =>
                {
                    return Persistence.Count(comData: comData, connectionId);
                }, returnInfo);

                AfterCount(returnInfo, ref connectionId, comData);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnCounted(returnInfo, comData, connectionId: connectionId);

                return returnInfo;
            }
            catch (Exception ex)
            {
                isClose = true;
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (isClose)
                {
                    Persistence.Release(connectionId);
                }
            }
        }

        /// <summary>
        /// 查询模型列表前事件
        /// </summary>
        public event Action<ReturnInfo<IList<ModelT>>, CommonUseData, string> Querying;

        /// <summary>
        /// 执行查询模型列表前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnQuerying(ReturnInfo<IList<ModelT>> returnInfo, CommonUseData comData = null, string connectionId = null)
        {
            if (Querying != null)
            {
                Querying(returnInfo, comData, connectionId);
            }
        }

        /// <summary>
        /// 查询模型列表后事件
        /// </summary>
        public event Action<ReturnInfo<IList<ModelT>>, CommonUseData, string> Queryed;

        /// <summary>
        /// 执行查询模型列表后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnQueryed(ReturnInfo<IList<ModelT>> returnInfo, CommonUseData comData = null, string connectionId = null)
        {
            if (Queryed != null)
            {
                Queryed(returnInfo, comData, connectionId);
            }
        }

        /// <summary>
        /// 查询模型列表
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<IList<ModelT>> Query(CommonUseData comData = null, string connectionId = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<IList<ModelT>> returnInfo = new ReturnInfo<IList<ModelT>>();
                BeforeQuery(returnInfo, ref connectionId, comData);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnQuerying(returnInfo, comData, connectionId: connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<IList<ModelT>>((reInfo) =>
                {
                    return Persistence.Select(comData: comData, connectionId);
                }, returnInfo);

                AfterQuery(returnInfo, ref connectionId, comData);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnQueryed(returnInfo, comData, connectionId: connectionId);

                return returnInfo;
            }
            catch (Exception ex)
            {
                isClose = true;
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (isClose)
                {
                    Persistence.Release(connectionId);
                }
            }
        }

        /// <summary>
        /// 查询模型列表并分页前事件
        /// </summary>
        public event Action<ReturnInfo<PagingInfo<ModelT>>, int, int, FilterInfo, CommonUseData, string> QueryPaging;

        /// <summary>
        /// 执行查询模型列表并分页前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnQueryPaging(ReturnInfo<PagingInfo<ModelT>> returnInfo, int pageIndex, int pageSize, FilterInfo filter, CommonUseData comData = null, string connectionId = null)
        {
            if (QueryPaging != null)
            {
                QueryPaging(returnInfo, pageIndex, pageSize, filter, comData, connectionId);
            }
        }

        /// <summary>
        /// 查询模型列表并分页后事件
        /// </summary>
        public event Action<ReturnInfo<PagingInfo<ModelT>>, int, int, FilterInfo, CommonUseData, string> QueryPaged;

        /// <summary>
        /// 执行查询模型列表并分页后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnQueryPaged(ReturnInfo<PagingInfo<ModelT>> returnInfo, int pageIndex, int pageSize, FilterInfo filter, CommonUseData comData = null, string connectionId = null)
        {
            if (QueryPaged != null)
            {
                QueryPaged(returnInfo, pageIndex, pageSize, filter, comData, connectionId);
            }
        }

        /// <summary>
        /// 查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<PagingInfo<ModelT>> QueryPage([PageIndex] int pageIndex, [PageSize] int pageSize, FilterInfo filter = null, CommonUseData comData = null, string connectionId = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<PagingInfo<ModelT>> returnInfo = new ReturnInfo<PagingInfo<ModelT>>();
                BeforeQueryPage(returnInfo, pageIndex, pageSize, ref connectionId, filter, comData);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnQueryPaging(returnInfo, pageIndex, pageSize, filter, comData, connectionId: connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<PagingInfo<ModelT>>((reInfo) =>
                {
                    return Persistence.SelectPage(pageIndex, pageSize, filter, comData: comData, connectionId: connectionId);
                }, returnInfo);

                AfterQueryPage(returnInfo, pageIndex, pageSize, ref connectionId, filter, comData);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnQueryPaged(returnInfo, pageIndex, pageSize, filter, comData, connectionId: connectionId);

                return returnInfo;
            }
            catch (Exception ex)
            {
                isClose = true;
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (isClose)
                {
                    Persistence.Release(connectionId);
                }
            }
        }

        #endregion

        #region 写入

        /// <summary>
        /// 添加模型前事件
        /// </summary>
        public event Action<ReturnInfo<bool>, ModelT, CommonUseData, string> Adding;

        /// <summary>
        /// 执行模型前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnAdding(ReturnInfo<bool> returnInfo, ModelT model, CommonUseData comData = null, string connectionId = null)
        {
            if (Adding != null)
            {
                Adding(returnInfo, model, comData, connectionId);
            }
        }

        /// <summary>
        /// 添加模型后事件
        /// </summary>
        public event Action<ReturnInfo<bool>, ModelT, CommonUseData, string> Added;

        /// <summary>
        /// 执行模型后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnAdded(ReturnInfo<bool> returnInfo, ModelT model, CommonUseData comData = null, string connectionId = null)
        {
            if (Added != null)
            {
                Added(returnInfo, model, comData, connectionId);
            }
        }

        /// <summary>
        /// 添加模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<bool> Add([DisplayName2("模型"), Required, Model] ModelT model, CommonUseData comData = null, string connectionId = null)
        {
            if (model is PersonTimeInfo<IdT>)
            {
                SetCreateInfo(model, comData);
            }

            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
                BeforeAdd(returnInfo, model, ref connectionId, comData);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnAdding(returnInfo, model, comData, connectionId: connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                if (!DbBuilderId() && Identity.IsEmpty(model.Id))
                {
                    model.Id = Identity.New();
                }

                returnInfo = ExecReturnFunc<bool>((reInfo) =>
                {
                    return Persistence.Insert(model, comData: comData, connectionId: connectionId) > 0;
                }, returnInfo);

                AfterAdd(returnInfo, model, ref connectionId, comData);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnAdded(returnInfo, model, comData, connectionId: connectionId);

                return returnInfo;
            }
            catch (Exception ex)
            {
                isClose = true;
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (isClose)
                {
                    Persistence.Release(connectionId);
                }
            }
        }

        /// <summary>
        /// 添加模型列表前事件
        /// </summary>
        public event Action<ReturnInfo<bool>, IList<ModelT>, CommonUseData, string> Addsing;

        /// <summary>
        /// 执行模型列表前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="models">模型列表</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnAddsing(ReturnInfo<bool> returnInfo, IList<ModelT> models, CommonUseData comData = null, string connectionId = null)
        {
            if (Addsing != null)
            {
                Addsing(returnInfo, models, comData, connectionId);
            }
        }

        /// <summary>
        /// 添加模型列表后事件
        /// </summary>
        public event Action<ReturnInfo<bool>, IList<ModelT>, CommonUseData, string> Addsed;

        /// <summary>
        /// 执行模型列表后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="models">模型列表</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnAddsed(ReturnInfo<bool> returnInfo, IList<ModelT> models, CommonUseData comData = null, string connectionId = null)
        {
            if (Addsed != null)
            {
                Addsed(returnInfo, models, comData, connectionId);
            }
        }

        /// <summary>
        /// 添加模型列表
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<bool> Add([DisplayName2("模型列表"), MultiModel] IList<ModelT> models, CommonUseData comData = null, string connectionId = null)
        {
            foreach (ModelT model in models)
            {
                SetCreateInfo(model, comData);
            }

            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
                BeforeAdd(returnInfo, models, ref connectionId, comData);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnAddsing(returnInfo, models, comData, connectionId: connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                if (!DbBuilderId())
                {
                    foreach (var m in models)
                    {
                        if (Identity.IsEmpty(m.Id))
                        {
                            m.Id = Identity.New();
                        }
                    }
                }

                returnInfo = ExecReturnFunc<bool>((reInfo) =>
                {
                    return Persistence.Insert(models, comData: comData, connectionId: connectionId) > 0;
                }, returnInfo);

                AfterAdd(returnInfo, models, ref connectionId, comData);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnAddsed(returnInfo, models, comData, connectionId: connectionId);

                return returnInfo;
            }
            catch (Exception ex)
            {
                isClose = true;
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (isClose)
                {
                    Persistence.Release(connectionId);
                }
            }
        }

        /// <summary>
        /// 设置模型前事件
        /// </summary>
        public event Action<ReturnInfo<bool>, ModelT, CommonUseData, string> Seting;

        /// <summary>
        /// 执行设置模型前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnSeting(ReturnInfo<bool> returnInfo, ModelT model, CommonUseData comData = null, string connectionId = null)
        {
            if (Seting != null)
            {
                Seting(returnInfo, model, comData, connectionId);
            }
        }

        /// <summary>
        /// 设置模型后事件
        /// </summary>
        public event Action<ReturnInfo<bool>, ModelT, CommonUseData, string> Seted;

        /// <summary>
        /// 执行设置模型后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnSeted(ReturnInfo<bool> returnInfo, ModelT model, CommonUseData comData = null, string connectionId = null)
        {
            if (Seted != null)
            {
                Seted(returnInfo, model, comData, connectionId);
            }
        }

        /// <summary>
        /// 设置模型
        /// 如果ID存在则修改，否则添加
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<bool> Set([DisplayName2("模型"), Required, Model] ModelT model, CommonUseData comData = null, string connectionId = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
                BeforeSet(returnInfo, model, ref connectionId, comData);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnSeting(returnInfo, model, comData, connectionId: connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFuncAndConnectionId<bool>((reInfo, connId) =>
                {
                    bool exists = false;
                    if (!Identity.IsEmpty(model.Id))
                    {
                        ReturnInfo<bool> existsReturnInfo = Exists(model.Id, comData, connId);
                        if (existsReturnInfo.Failure())
                        {
                            reInfo.FromBasic(existsReturnInfo);
                            return false;
                        }
                        exists = existsReturnInfo.Data;
                    }

                    ReturnInfo<bool> re = exists ? ModifyById(model, comData, connId) : Add(model, comData, connId);
                    reInfo.FromBasic(re);

                    return re.Data;
                }, returnInfo, connectionId: connectionId);

                AfterSet(returnInfo, model, ref connectionId, comData);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnSeted(returnInfo, model, comData, connectionId: connectionId);

                return returnInfo;
            }
            catch (Exception ex)
            {
                isClose = true;
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (isClose)
                {
                    Persistence.Release(connectionId);
                }
            }
        }

        /// <summary>
        /// 根据ID修改模型前事件
        /// </summary>
        public event Action<ReturnInfo<bool>, ModelT, CommonUseData, string> ModifyByIding;

        /// <summary>
        /// 执行根据ID修改模型前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnModifyByIding(ReturnInfo<bool> returnInfo, ModelT model, CommonUseData comData = null, string connectionId = null)
        {
            if (ModifyByIding != null)
            {
                ModifyByIding(returnInfo, model, comData, connectionId);
            }
        }

        /// <summary>
        /// 根据ID修改模型后事件
        /// </summary>
        public event Action<ReturnInfo<bool>, ModelT, CommonUseData, string> ModifyByIded;

        /// <summary>
        /// 执行根据ID修改模型后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnModifyByIded(ReturnInfo<bool> returnInfo, ModelT model, CommonUseData comData = null, string connectionId = null)
        {
            if (ModifyByIded != null)
            {
                ModifyByIded(returnInfo, model, comData, connectionId);
            }
        }

        /// <summary>
        /// 根据ID修改模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<bool> ModifyById([DisplayName2("模型"), Required, Model] ModelT model, CommonUseData comData = null, string connectionId = null)
        {
            return ExecReturnFuncAndConnectionId<bool>((reInfo, connId) =>
            {
                OptimissticLockHandle(model, reInfo, comData: comData, connectionId: connId);
                if (reInfo.Failure())
                {
                    return false;
                }

                SetModifyInfo(model, comData);

                BeforeModifyById(reInfo, model, ref connectionId, comData);
                if (reInfo.Failure())
                {
                    return false;
                }

                OnModifyByIding(reInfo, model, comData, connectionId: connectionId);
                if (reInfo.Failure())
                {
                    return false;
                }

                var result = Persistence.UpdateById(model, comData: comData, connectionId: connectionId) > 0;

                AfterModifyById(reInfo, model, ref connectionId, comData);
                if (reInfo.Failure())
                {
                    return false;
                }

                OnModifyByIded(reInfo, model, comData, connectionId: connectionId);

                return result;
            }, connectionId: connectionId, accessMode: AccessMode.MASTER);
        }

        /// <summary>
        /// 根据ID移除模型前事件
        /// </summary>
        public event Action<ReturnInfo<bool>, IdT, CommonUseData, string> RemoveByIding;

        /// <summary>
        /// 执行根据ID移除模型前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnRemoveByIding(ReturnInfo<bool> returnInfo, IdT id, CommonUseData comData = null, string connectionId = null)
        {
            if (RemoveByIding != null)
            {
                RemoveByIding(returnInfo, id, comData, connectionId);
            }
        }

        /// <summary>
        /// 根据ID移除模型后事件
        /// </summary>
        public event Action<ReturnInfo<bool>, IdT, CommonUseData, string> RemoveByIded;

        /// <summary>
        /// 执行根据ID移除模型后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnRemoveByIded(ReturnInfo<bool> returnInfo, IdT id, CommonUseData comData = null, string connectionId = null)
        {
            if (RemoveByIded != null)
            {
                RemoveByIded(returnInfo, id, comData, connectionId);
            }
        }

        /// <summary>
        /// 根据ID移除模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<bool> RemoveById([Id] IdT id, CommonUseData comData = null, string connectionId = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
                BeforeRemoveById(returnInfo, id, ref connectionId, comData);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnRemoveByIding(returnInfo, id, comData, connectionId: connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<bool>((reInfo) =>
                {
                    return Persistence.DeleteById(id, comData: comData, connectionId: connectionId) > 0;
                }, returnInfo);

                AfterRemoveById(returnInfo, id, ref connectionId, comData);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnRemoveByIded(returnInfo, id, comData, connectionId: connectionId);

                return returnInfo;
            }
            catch (Exception ex)
            {
                isClose = true;
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (isClose)
                {
                    Persistence.Release(connectionId);
                }
            }
        }

        /// <summary>
        /// 根据ID数组移除模型前事件
        /// </summary>
        public event Action<ReturnInfo<bool>, IdT[], CommonUseData, string> RemoveByIdsing;

        /// <summary>
        /// 执行根据ID数组移除模型前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="ids">ID集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnRemoveByIdsing(ReturnInfo<bool> returnInfo, IdT[] ids, CommonUseData comData = null, string connectionId = null)
        {
            if (RemoveByIdsing != null)
            {
                RemoveByIdsing(returnInfo, ids, comData, connectionId);
            }
        }

        /// <summary>
        /// 根据ID数组移除模型后事件
        /// </summary>
        public event Action<ReturnInfo<bool>, IdT[], CommonUseData, string> RemoveByIdsed;

        /// <summary>
        /// 执行根据ID数组移除模型后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="ids">ID集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnRemoveByIdsed(ReturnInfo<bool> returnInfo, IdT[] ids, CommonUseData comData = null, string connectionId = null)
        {
            if (RemoveByIdsed != null)
            {
                RemoveByIdsed(returnInfo, ids, comData, connectionId);
            }
        }

        /// <summary>
        /// 根据ID数组移除模型
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<bool> RemoveByIds([DisplayName2("ID集合"), ArrayNotEmpty] IdT[] ids, CommonUseData comData = null, string connectionId = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
                BeforeRemoveByIds(returnInfo, ids, ref connectionId, comData);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnRemoveByIdsing(returnInfo, ids, comData, connectionId: connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<bool>((reInfo) =>
                {
                    return Persistence.DeleteByIds(ids, comData: comData, connectionId: connectionId) > 0;
                }, returnInfo);

                AfterRemoveByIds(returnInfo, ids, ref connectionId, comData);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnRemoveByIdsed(returnInfo, ids, comData, connectionId: connectionId);

                return returnInfo;
            }
            catch (Exception ex)
            {
                isClose = true;
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (isClose)
                {
                    Persistence.Release(connectionId);
                }
            }
        }

        /// <summary>
        /// 清空所有模型前事件
        /// </summary>
        public event Action<ReturnInfo<bool>, CommonUseData, string> Clearing;

        /// <summary>
        /// 执行清空所有模型前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnClearing(ReturnInfo<bool> returnInfo, CommonUseData comData = null, string connectionId = null)
        {
            if (Clearing != null)
            {
                Clearing(returnInfo, comData, connectionId);
            }
        }

        /// <summary>
        /// 清空所有模型后事件
        /// </summary>
        public event Action<ReturnInfo<bool>, CommonUseData, string> Cleared;

        /// <summary>
        /// 执行清空所有模型后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnCleared(ReturnInfo<bool> returnInfo, CommonUseData comData = null, string connectionId = null)
        {
            if (Cleared != null)
            {
                Cleared(returnInfo, comData, connectionId);
            }
        }

        /// <summary>
        /// 清空所有模型
        /// </summary>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<bool> Clear(CommonUseData comData = null, string connectionId = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
                BeforeClear(returnInfo, ref connectionId, comData);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnClearing(returnInfo, comData, connectionId: connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<bool>((reInfo) =>
                {
                    return Persistence.Delete(comData: comData, connectionId: connectionId) > 0;
                });

                AfterClear(returnInfo, ref connectionId, comData);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnCleared(returnInfo, comData, connectionId: connectionId);                
                
                return returnInfo;
            }
            catch (Exception ex)
            {
                isClose = true;
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (isClose)
                {
                    Persistence.Release(connectionId);
                }
            }
        }

        #endregion

        #endregion

        #region 受保护的方法

        /// <summary>
        /// 执行返回函数且带有连接ID
        /// </summary>
        /// <typeparam name="OutT">输出类型</typeparam>
        /// <param name="func">函数</param>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="accessMode">访问模式</param>
        /// <returns>返回信息</returns>
        protected ReturnInfo<OutT> ExecReturnFuncAndConnectionId<OutT>(Func<ReturnInfo<OutT>, string, OutT> func, ReturnInfo<OutT> returnInfo = null, string connectionId = null, AccessMode accessMode = AccessMode.MASTER)
        {
            return ExecReturnFunc<OutT>((reInfo) =>
            {
                OutT result = default(OutT);
                ExecProcConnectionId((connId) =>
                {
                    result = func(reInfo, connId);
                }, connectionId: connectionId, accessMode);

                return result;
            }, returnInfo);
        }

        /// <summary>
        /// 执行返回函数
        /// </summary>
        /// <typeparam name="OutT">输出类型</typeparam>
        /// <param name="func">函数</param>
        /// <param name="returnInfo">返回信息</param>
        /// <returns>返回信息</returns>
        protected ReturnInfo<OutT> ExecReturnFunc<OutT>(Func<ReturnInfo<OutT>, OutT> func, ReturnInfo<OutT> returnInfo = null)
        {
            if (returnInfo == null)
            {
                returnInfo = new ReturnInfo<OutT>();
            }

            returnInfo.Data = func(returnInfo);

            return returnInfo;
        }

        /// <summary>
        /// 执行连接ID过程
        /// 如果传过来的连接ID为空，则会创建新的连接ID，结束后会自动注释连接ID，否则不会
        /// </summary>
        /// <param name="action">动作</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="accessMode">访问模式</param>
        protected void ExecProcConnectionId(Action<string> action, string connectionId = null, AccessMode accessMode = AccessMode.MASTER)
        {
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                connectionId = Persistence.NewConnectionId(accessMode);

                try
                {
                    action(connectionId);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    Persistence.Release(connectionId);
                }
            }
            else
            {
                action(connectionId);
            }
        }

        /// <summary>
        /// 设置创建信息
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="comData">通用数据</param>
        protected void SetCreateInfo(ModelT model, CommonUseData comData = null)
        {
            if (model is PersonTimeInfo<IdT>)
            {
                PersonTimeInfo<IdT> p = model as PersonTimeInfo<IdT>;
                p.SetCreateInfo(comData.GetCurrUser() as BasicUserInfo<IdT>);
            }
        }

        /// <summary>
        /// 设置修改信息
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="comData">通用数据</param>
        protected void SetModifyInfo(ModelT model, CommonUseData comData = null)
        {
            if (model is PersonTimeInfo<IdT>)
            {
                PersonTimeInfo<IdT> p = model as PersonTimeInfo<IdT>;
                p.SetModifyInfo(comData.GetCurrUser() as BasicUserInfo<IdT>);
            }
        }

        #endregion

        #region 虚方法

        /// <summary>
        /// 根据ID查找模型前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        protected virtual void BeforeFind(ReturnInfo<ModelT> returnInfo, IdT id, ref string connectionId, CommonUseData comData = null) { }

        /// <summary>
        /// 根据ID查找模型后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        protected virtual void AfterFind(ReturnInfo<ModelT> returnInfo, IdT id, ref string connectionId, CommonUseData comData = null) { }

        /// <summary>
        /// 根据ID集合查找模型列表前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        protected virtual void BeforeFind(ReturnInfo<IList<ModelT>> returnInfo, IdT[] ids, ref string connectionId, CommonUseData comData = null) { }

        /// <summary>
        /// 根据ID集合查找模型列表后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        protected virtual void AfterFind(ReturnInfo<IList<ModelT>> returnInfo, IdT[] ids, ref string connectionId, CommonUseData comData = null) { }

        /// <summary>
        /// 根据ID判断模型是否存在前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        protected virtual void BeforeExists(ReturnInfo<bool> returnInfo, IdT id, ref string connectionId, CommonUseData comData = null) { }

        /// <summary>
        /// 根据ID判断模型是否存在后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        protected virtual void AfterExists(ReturnInfo<bool> returnInfo, IdT id, ref string connectionId, CommonUseData comData = null) { }

        /// <summary>
        /// 统计模型数前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        protected virtual void BeforeCount(ReturnInfo<int> returnInfo, ref string connectionId, CommonUseData comData = null) { }

        /// <summary>
        /// 统计模型数后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        protected virtual void AfterCount(ReturnInfo<int> returnInfo, ref string connectionId, CommonUseData comData = null) { }

        /// <summary>
        /// 查询模型列表前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        protected virtual void BeforeQuery(ReturnInfo<IList<ModelT>> returnInfo, ref string connectionId, CommonUseData comData = null) { }

        /// <summary>
        /// 查询模型列表后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        protected virtual void AfterQuery(ReturnInfo<IList<ModelT>> returnInfo, ref string connectionId, CommonUseData comData = null) { }

        /// <summary>
        /// 执行查询模型列表并分页前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="filter">筛选</param>
        /// <param name="comData">通用数据</param>
        protected virtual void BeforeQueryPage(ReturnInfo<PagingInfo<ModelT>> returnInfo, int pageIndex, int pageSize, ref string connectionId, FilterInfo filter = null, CommonUseData comData = null) { }

        /// <summary>
        /// 执行查询模型列表并分页后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="filter">筛选</param>
        /// <param name="comData">通用数据</param>
        protected virtual void AfterQueryPage(ReturnInfo<PagingInfo<ModelT>> returnInfo, int pageIndex, int pageSize, ref string connectionId, FilterInfo filter = null, CommonUseData comData = null) { }

        /// <summary>
        /// 添加模型前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        protected virtual void BeforeAdd(ReturnInfo<bool> returnInfo, ModelT model, ref string connectionId, CommonUseData comData = null) { }

        /// <summary>
        /// 添加模型后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        protected virtual void AfterAdd(ReturnInfo<bool> returnInfo, ModelT model, ref string connectionId, CommonUseData comData = null) { }

        /// <summary>
        /// 添加模型列表前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="models">模型列表</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        protected virtual void BeforeAdd(ReturnInfo<bool> returnInfo, IList<ModelT> models, ref string connectionId, CommonUseData comData = null) { }

        /// <summary>
        /// 添加模型列表后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="models">模型列表</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        protected virtual void AfterAdd(ReturnInfo<bool> returnInfo, IList<ModelT> models, ref string connectionId, CommonUseData comData = null) { }

        /// <summary>
        /// 设置模型前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        protected virtual void BeforeSet(ReturnInfo<bool> returnInfo, ModelT model, ref string connectionId, CommonUseData comData = null) { }

        /// <summary>
        /// 设置模型后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        protected virtual void AfterSet(ReturnInfo<bool> returnInfo, ModelT model, ref string connectionId, CommonUseData comData = null) { }

        /// <summary>
        /// 根据ID修改模型前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        protected virtual void BeforeModifyById(ReturnInfo<bool> returnInfo, ModelT model, ref string connectionId, CommonUseData comData = null) { }

        /// <summary>
        /// 根据ID修改模型后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        protected virtual void AfterModifyById(ReturnInfo<bool> returnInfo, ModelT model, ref string connectionId, CommonUseData comData = null) { }

        /// <summary>
        /// 根据ID移除模型前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        protected virtual void BeforeRemoveById(ReturnInfo<bool> returnInfo, IdT id, ref string connectionId, CommonUseData comData = null) { }

        /// <summary>
        /// 根据ID移除模型后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        protected virtual void AfterRemoveById(ReturnInfo<bool> returnInfo, IdT id, ref string connectionId, CommonUseData comData = null) { }

        /// <summary>
        /// 根据ID集合移除模型前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        protected virtual void BeforeRemoveByIds(ReturnInfo<bool> returnInfo, IdT[] ids, ref string connectionId, CommonUseData comData = null) { }

        /// <summary>
        /// 根据ID集合移除模型后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        /// <returns>返回信息</returns>
        protected virtual void AfterRemoveByIds(ReturnInfo<bool> returnInfo, IdT[] ids, ref string connectionId, CommonUseData comData = null) { }

        /// <summary>
        /// 清空所有模型前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        protected virtual void BeforeClear(ReturnInfo<bool> returnInfo, ref string connectionId, CommonUseData comData = null) { }

        /// <summary>
        /// 清空所有模型后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="comData">通用数据</param>
        protected virtual void AfterClear(ReturnInfo<bool> returnInfo, ref string connectionId, CommonUseData comData = null) { }

        /// <summary>
        /// 是否支持乐观锁，如果支持，则会在更新时，会判断修改时间，默认不支持，如果要改为支持，请在子类重写
        /// </summary>
        /// <returns>是否支持乐观锁</returns>
        protected virtual bool IsSupportOptimisticLock() => false;

        /// <summary>
        /// 乐观锁处理，注意：根据ID和修改时间来判断
        /// </summary>
        /// <typeparam name="ReturnDataT">返回数据类型</typeparam>
        /// <param name="model">模型</param>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="mode">访问模式，默认为主库</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        protected virtual void OptimissticLockHandle<ReturnDataT>(ModelT model, ReturnInfo<ReturnDataT> returnInfo, AccessMode mode = AccessMode.MASTER, CommonUseData comData = null, string connectionId = null)
        {
            // 如果支持乐观锁，则需要先按修改时间来查询是否之前被修改过
            if (IsSupportOptimisticLock())
            {
                var temp = Persistence.SelectModifyInfoByIdAndGeModifyTime(model, mode, comData: comData, connectionId: connectionId);
                if (temp == null)
                {
                    return;
                }

                var person = temp as PersonTimeInfo<IdT>;
                var msg = Localize.Get(ServiceCodeDefine.DATA_MODIFIED_CULTURE_KEY, "数据已被[{0}]修改过,请重新加载数据");
                returnInfo.SetCodeMsg(ServiceCodeDefine.DATA_MODIFIED, string.Format(msg, person.Modifier));
            }
        }

        /// <summary>
        /// 乐观锁处理，注意：根据ID和修改时间来判断
        /// </summary>
        /// <typeparam name="ReturnDataT">返回数据类型</typeparam>
        /// <param name="models">模型列表</param>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="mode">访问模式，默认为主库</param>
        /// <param name="comData">通用数据</param>
        /// <param name="connectionId">连接ID</param>
        protected virtual void OptimissticLockHandle<ReturnDataT>(ModelT[] models, ReturnInfo<ReturnDataT> returnInfo, AccessMode mode = AccessMode.MASTER, CommonUseData comData = null, string connectionId = null)
        {
            // 如果支持乐观锁，则需要先按修改时间来查询是否之前被修改过
            if (IsSupportOptimisticLock())
            {
                var temp = Persistence.SelectModifyInfosByIdAndGeModifyTime(models, mode, comData: comData, connectionId: connectionId);
                if (temp.IsNullOrCount0())
                {
                    return;
                }

                var msg = Localize.Get(ServiceCodeDefine.DATA_MODIFIED_CULTURE_KEY, "数据已被[{0}]修改过,请重新加载数据");

                var modifyStr = new StringBuilder();
                foreach (var item in temp)
                {
                    var person = item as PersonTimeInfo<IdT>;
                    modifyStr.AppendFormat("{0},", person.Modifier);
                }
                modifyStr.Remove(modifyStr.Length - 1, 1);

                returnInfo.SetCodeMsg(ServiceCodeDefine.DATA_MODIFIED, string.Format(msg, modifyStr.ToString()));
            }
        }

        /// <summary>
        /// 乐观锁处理，注意：根据修改时间来判断，如果源模型修改时间大于新模型修改时间，则代表数据已被别人修改过
        /// </summary>
        /// <typeparam name="ReturnDataT">返回数据类型</typeparam>
        /// <param name="sourceModel">源模型</param>
        /// <param name="newModel">新模型</param>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="comData">通用数据</param>
        protected virtual void OptimissticLockHandle<ReturnDataT>(ModelT sourceModel, ModelT newModel, ReturnInfo<ReturnDataT> returnInfo, CommonUseData comData = null)
        {
            if (sourceModel == null || newModel == null)
            {
                return;
            }

            if (sourceModel is PersonTimeInfo<IdT> && newModel is PersonTimeInfo<IdT>)
            {
                var s = sourceModel as PersonTimeInfo<IdT>;
                var n = newModel as PersonTimeInfo<IdT>;
                if (s.ModifyTime > n.ModifyTime)
                {
                    var msg = Localize.Get(ServiceCodeDefine.DATA_MODIFIED_CULTURE_KEY, "数据已被[{0}]修改过,请重新加载数据");
                    returnInfo.SetCodeMsg(ServiceCodeDefine.DATA_MODIFIED, string.Format(msg, s.Modifier));
                }
            }            
        }

        /// <summary>
        /// 是否由DB生成ID，默认为是。如果为是，则由数据库自增生成。如果为否则，默认添加方法会自动调用Identity.New()生成ID
        /// </summary>
        /// <returns>是否由DB生成ID</returns>
        protected virtual bool DbBuilderId() => true;

        #endregion
    }
}
