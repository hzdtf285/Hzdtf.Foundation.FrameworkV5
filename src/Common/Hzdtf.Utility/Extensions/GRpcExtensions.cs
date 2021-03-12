using Hzdtf.Utility;
using Hzdtf.Utility.Model;
using Hzdtf.Utility.Model.Return;
using Hzdtf.Utility.Utils;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grpc.Net.Client
{
    /// <summary>
    /// GRpc渠道辅助类
    /// @ 黄振东
    /// </summary>
    public static class GRpcChannelUtil
    {
        /// <summary>
        /// 创建一个渠道，执行完业务处理方法后，会自动关闭渠道连接
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="action">回调业务处理方法</param>
        /// <param name="exAction">发生异常回调，如果为null，则不会捕获异常</param>
        /// <param name="customerOptions">自定义选项配置</param>
        /// <param name="options">选项配置</param>
        public static void CreateChannel(string address, Action<GrpcChannel, Metadata> action, Action<RpcException> exAction = null, Action<GrpcChannelCustomerOptions> customerOptions = null, GrpcChannelOptions options = null)
        {
            var headers = new Metadata();
            var cusOptions = new GrpcChannelCustomerOptions();
            if (customerOptions != null)
            {
                customerOptions(cusOptions);
            }

            if (cusOptions.IsAddToken && (cusOptions.GetTokenFunc != null || App.GetTokenFunc != null))
            {
                var token = cusOptions.GetTokenFunc != null ? cusOptions.GetTokenFunc() : App.GetTokenFunc();
                headers.Add($"{AuthUtil.AUTH_KEY}", token.AddBearerToken());
            }
            if (cusOptions.IsAddEventId && App.GetEventIdFunc != null)
            {
                headers.Add(App.EVENT_ID_KEY, App.GetEventIdFunc());
            }

            if (exAction == null)
            {
                if (options == null)
                {
                    using (var channel = GrpcChannel.ForAddress(address))
                    {
                        action(channel, headers);
                    }
                }
                else
                {
                    using (var channel = GrpcChannel.ForAddress(address, options))
                    {
                        action(channel, headers);
                    }
                }
            }
            else
            {
                try
                {
                    if (options == null)
                    {
                        using (var channel = GrpcChannel.ForAddress(address))
                        {
                            action(channel, headers);
                        }
                    }
                    else
                    {
                        using (var channel = GrpcChannel.ForAddress(address, options))
                        {
                            action(channel, headers);
                        }
                    }
                }
                catch (RpcException ex)
                {
                    exAction(ex);
                }
            }
        }

        /// <summary>
        /// 判断请求内容类型是否GRpc
        /// </summary>
        /// <param name="requestContentType">请求内容</param>
        /// <returns>请求内容类型是否GRpc</returns>
        public static bool IsRequestGRpc(string requestContentType)
        {
            if (string.IsNullOrWhiteSpace(requestContentType))
            {
                return false;
            }

            return "application/grpc".Equals(requestContentType.ToLower());
        }

        /// <summary>
        /// 抛出RPC异常
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="msg">消息</param>
        public static void ThrowRpcException(this Exception ex, string msg)
        {
            var status = new Status(StatusCode.Unknown, msg, ex);
            throw new RpcException(status);
        }

        /// <summary>
        /// 如果返回值是错误则抛出RPC异常
        /// </summary>
        /// <param name="basicReturn">基本返回</param>
        public static void ThrowReturnFailureRpcException(this BasicReturnInfo basicReturn)
        {
            if (basicReturn == null || basicReturn.Success())
            {
                return;
            }

            new BusinessException(basicReturn.Code, basicReturn.Msg, basicReturn.Desc).ThrowRpcException(basicReturn.Msg);
        }
    }

    /// <summary>
    /// GRpc渠道自定义选项配置
    /// @ 黄振东
    /// </summary>
    public class GrpcChannelCustomerOptions
    {
        /// <summary>
        /// 是否添加Token，设置后，会调用App.GetTokenFunc获取Token
        /// 默认为是
        /// </summary>
        public bool IsAddToken
        {
            get;
            set;
        } = true;

        /// <summary>
        /// 获取Token回调
        /// </summary>
        public Func<string> GetTokenFunc
        {
            get;
            set;
        }

        /// <summary>
        /// 是否添加事件ID，设置后，会调用App.GetEventIdFunc获取事件ID
        /// 默认为是
        /// </summary>
        public bool IsAddEventId
        {
            get;
            set;
        } = true;
    }
}
