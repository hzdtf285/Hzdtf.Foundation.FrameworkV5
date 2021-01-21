﻿using Hzdtf.Example.Model;
using Hzdtf.Utility.Attr;
using Hzdtf.Workflow.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Example.Controller.Expand
{
    /// <summary>
    /// 测试表单工作流控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [Menu("TestForm")]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TestFormWorkflowController : WorkflowFormControllerBase<TestFormInfo>
    {       
        /// <summary>
        /// 获取工作流编码
        /// </summary>
        /// <returns>工作流编码</returns>
        protected override string GetWorkflowCode() => WorkflowDefine.TEST_FORM;
    }
}
