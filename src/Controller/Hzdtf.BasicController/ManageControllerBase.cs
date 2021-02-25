using Hzdtf.Service.Contract;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Hzdtf.BasicController
{
    /// <summary>
    /// 管理控制器基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">Id类型</typeparam>
    /// <typeparam name="ModelT">模型类型</typeparam>
    /// <typeparam name="ServiceT">服务类型</typeparam>
    /// <typeparam name="PageInfoT">页面信息类型</typeparam>
    public abstract partial class ManageControllerBase<IdT, ModelT, ServiceT, PageInfoT> : PageDataControllerBase<IdT, ModelT, ServiceT, PageInfoT>
        where ModelT : SimpleInfo<IdT>
        where ServiceT : IService<IdT, ModelT>
        where PageInfoT : PageInfo<IdT>
    {
        /// <summary>
        /// 根据ID查找模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回信息</returns>
        [HttpGet("{id}")]
        public virtual ReturnInfo<ModelT> Get(IdT id) => Service.Find(id);

        /// <summary>
        /// 添加模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <returns>返回信息</returns>
        [HttpPost]
        public virtual ReturnInfo<bool> Post(ModelT model) => Service.Add(model);

        /// <summary>
        /// 修改模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="model">模型</param>
        /// <returns>返回信息</returns>
        [HttpPut("{id}")]
        public virtual ReturnInfo<bool> Put(IdT id, ModelT model)
        {
            model.Id = id;

            return Service.ModifyById(model);
        }

        /// <summary>
        /// 移除模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回信息</returns>
        [HttpDelete("{id}")]
        public virtual ReturnInfo<bool> Delete(IdT id) => Service.RemoveById(id);

        /// <summary>
        /// 批量添加模型列表
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <returns>返回信息</returns>
        [HttpPost("BatchAdd")]
        public virtual ReturnInfo<bool> BatchAdd(IList<ModelT> models) => Service.Add(models);

        /// <summary>
        /// 根据ID集合批量移除模型
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <returns>返回信息</returns>
        [HttpDelete("BatchRemove")]
        public virtual ReturnInfo<bool> BatchRemove(IdT[] ids) => Service.RemoveByIds(ids);

        /// <summary>
        /// 统计模型数量
        /// </summary>
        /// <returns>返回信息</returns>
        [HttpDelete("Count")]
        public virtual ReturnInfo<int> Count() => Service.Count();

        /// <summary>
        /// 根据ID获取是否存在模型
        /// </summary>
        /// <returns>返回信息</returns>
        [HttpGet("Exists/{id}")]
        public virtual ReturnInfo<bool> Exists(IdT id) => Service.Exists(id);
    }
}
