using Hzdtf.Utility.Attr;
using Hzdtf.Utility.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Hzdtf.Utility;

namespace Hzdtf.BasicFunction.Controller.Other
{
    /// <summary>
    /// 图片控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        /// <summary>
        /// 生成验证码
        /// </summary>
        [HttpGet("BuilderCheckCode")]
        public FileContentResult BuilderCheckCode()
        {
            var verificationCodeRule = App.CurrConfig["VerificationCodeRule"];
            string checkCode = "EnNum".Equals(verificationCodeRule) ? NumberUtil.EnNumRandom() : NumberUtil.Random();

            var imageBytes = ImageUtil.CreateCrossPlatformCodeImg(checkCode);
            HttpContext.Session.SetString("VerificationCode", checkCode);

            return File(imageBytes, "image/png");
        }
    }
}
