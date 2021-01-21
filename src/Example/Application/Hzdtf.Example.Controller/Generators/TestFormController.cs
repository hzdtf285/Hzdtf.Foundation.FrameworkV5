using Hzdtf.BasicController;
using Hzdtf.Example.Model;
using Hzdtf.Example.Service.Contract;
using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Hzdtf.Utility.Utils;

namespace Hzdtf.Example.Controller
{
    /// <summary>
    /// 测试表单控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public partial class TestFormController : PagingControllerBase<int, TestFormInfo, ITestFormService, PageInfo<int>, KeywordFilterInfo>
    {
        /// <summary>
        /// 菜单编码
        /// </summary>
        /// <returns>菜单编码</returns>
        protected override string MenuCode() => "TestForm";
    }
}
