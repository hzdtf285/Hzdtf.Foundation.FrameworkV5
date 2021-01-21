using Hzdtf.BasicFunction.Model;
using Hzdtf.BasicFunction.Persistence.Contract;
using Hzdtf.BasicFunction.Service.Contract;
using Hzdtf.Service.Impl;
using Hzdtf.Utility.Attr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Service.Impl
{
    /// <summary>
    /// 数据字典服务
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class DataDictionaryService : ServiceBase<int, DataDictionaryInfo, IDataDictionaryPersistence>, IDataDictionaryService
    {
    }
}
