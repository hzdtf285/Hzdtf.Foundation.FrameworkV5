using Hzdtf.Utility.Model.Return;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Logger.Contract
{
    /// <summary>
    /// 日志业务接口
    /// @ 黄振东
    /// </summary>
    public interface ILogBusiness
    {
        /// <summary>
        /// 捕获日志执行
        /// 当发生异常时，会记录异常日志，同时触发异常回调
        /// </summary>
        /// <param name="action">执行核心</param>
        /// <param name="exceptionCallback">异常回调</param>
        /// <param name="logTags">日志标签</param>
        /// <returns>返回类型</returns>
        void TryLogExec(Action action, Action<BasicReturnInfo> exceptionCallback = null, params string[] logTags);

        /// <summary>
        /// 捕获日志执行
        /// 当发生异常时，会记录异常日志，同时触发异常回调
        /// </summary>
        /// <typeparam name="ReturnT">返回类型</typeparam>
        /// <param name="func">执行核心</param>
        /// <param name="exceptionCallback">异常回调</param>
        /// <param name="logTags">日志标签</param>
        /// <returns>返回类型</returns>
        ReturnT TryLogExec<ReturnT>(Func<ReturnT> func, Func<BasicReturnInfo, ReturnT> exceptionCallback = null, params string[] logTags);
    }
}
