using Hzdtf.Service.Contract;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public virtual async Task<ReturnInfo<ModelT>> Get(IdT id) => await Service.FindAsync(id);

        /// <summary>
        /// 添加模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <returns>返回信息</returns>
        [HttpPost]
        public virtual async Task<ReturnInfo<bool>> Post(ModelT model) => await Service.AddAsync(model);

        /// <summary>
        /// 修改模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="model">模型</param>
        /// <returns>返回信息</returns>
        [HttpPut("{id}")]
        public virtual async Task<ReturnInfo<bool>> Put(IdT id, ModelT model)
        {
            model.Id = id;

            return await Service.ModifyByIdAsync(model);
        }

        /// <summary>
        /// 移除模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回信息</returns>
        [HttpDelete("{id}")]
        public virtual async Task<ReturnInfo<bool>> Delete(IdT id) => await Service.RemoveByIdAsync(id);

        /// <summary>
        /// 批量添加模型列表
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <returns>返回信息</returns>
        [HttpPost("BatchAdd")]
        public virtual async Task<ReturnInfo<bool>> BatchAdd(IList<ModelT> models) => await Service.AddAsync(models);

        /// <summary>
        /// 根据ID集合批量移除模型
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <returns>返回信息</returns>
        [HttpDelete("BatchRemove")]
        public virtual async Task<ReturnInfo<bool>> BatchRemove(IdT[] ids) => await Service.RemoveByIdsAsync(ids);

        /// <summary>
        /// 统计模型数量
        /// </summary>
        /// <returns>返回信息</returns>
        [HttpDelete("Count")]
        public virtual async Task<ReturnInfo<int>> Count() => await Service.CountAsync();

        /// <summary>
        /// 根据ID获取是否存在模型
        /// </summary>
        /// <returns>返回信息</returns>
        [HttpGet("Exists/{id}")]
        public virtual async Task<ReturnInfo<bool>> Exists(IdT id) => await Service.ExistsAsync(id);
    }
}
