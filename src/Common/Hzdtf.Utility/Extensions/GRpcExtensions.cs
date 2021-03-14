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
using Hzdtf.Utility.Extensions;

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
        public static void CreateChannel(string address, Action<GrpcChannel, Metadata> action, Action<RpcException> exAction = null, Action<ChannelCustomerOptions> customerOptions = null, GrpcChannelOptions options = null)
        {
            var headers = new Metadata();
            var cusOptions = new ChannelCustomerOptions();
            if (customerOptions != null)
            {
                customerOptions(cusOptions);
            }

            var token = cusOptions.GetToken();
            if (!string.IsNullOrWhiteSpace(token))
            {
                headers.Add($"{AuthUtil.AUTH_KEY}", token.AddBearerToken());
            }
            var eventId = cusOptions.GetEventId();
            if (!string.IsNullOrWhiteSpace(eventId))
            {
                headers.Add(App.EVENT_ID_KEY, eventId);
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
}
