using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.Model
{
    /// <summary>
    /// JqG网格树信息
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    public class JqGridTreeInfo<IdT> : SimpleInfo<IdT>
    {
        /// <summary>
        /// 父ID
        /// </summary>
        [JsonProperty("parentId")]
        [Display(AutoGenerateField = false)]
        [MessagePack.Key("parentId")]
        public IdT ParentId
        {
            get;
            set;
        }

        /// <summary>
        /// 层级，从0开始
        /// </summary>
        [JsonProperty("level")]
        [Display(AutoGenerateField = true)]
        [MessagePack.Key("level")]
        public byte Level
        {
            get;
            set;
        }

        /// <summary>
        /// 是否叶子节点
        /// </summary>
        [JsonProperty("isLeaf")]
        [Display(AutoGenerateField = true)]
        [MessagePack.Key("isLeaf")]
        public bool IsLeaf
        {
            get;
            set;
        }

        /// <summary>
        /// 是否展开
        /// </summary>
        [JsonProperty("expanded")]
        [Display(AutoGenerateField = true)]
        [MessagePack.Key("expanded")]
        public bool Epanded
        {
            get => !IsLeaf;
        }
    }
}
