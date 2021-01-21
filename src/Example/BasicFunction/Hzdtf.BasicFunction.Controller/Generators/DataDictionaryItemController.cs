using Hzdtf.BasicController;
using Hzdtf.BasicFunction.Model;
using Hzdtf.BasicFunction.Service.Contract;
using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Hzdtf.Utility.Utils;
using Hzdtf.BasicFunction.Model.Expand.DataDictionaryItem;

namespace Hzdtf.BasicFunction.Controller
{
    /// <summary>
    /// 数据字典子项控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public partial class DataDictionaryItemController : PagingControllerBase<int, DataDictionaryItemInfo, IDataDictionaryItemService, PageInfo<int>, DataDictionaryItemFilterInfo>
    {
        /// <summary>
        /// 菜单编码
        /// </summary>
        /// <returns>菜单编码</returns>
        protected override string MenuCode() => "DataDictionary";
    }
}
