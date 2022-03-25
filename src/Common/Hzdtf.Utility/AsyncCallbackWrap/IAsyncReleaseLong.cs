using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.AsyncCallbackWrap
{
    /// <summary>
    /// 异步释放长整型接口
    /// 提供对本地线程阻塞，在本进程内，另处进行线程继续执行(释放)
    /// @ 黄振东
    /// </summary>
    public interface IAsyncReleaseLong : IAsyncReleaseWrap<long>
    {
    }
}
