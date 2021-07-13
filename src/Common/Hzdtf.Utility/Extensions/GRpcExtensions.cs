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
using System.Diagnostics;

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
        [Obsolete("此方法因不能复用GRpc通道，性能差，故弃用。请使用GetGRpcClient")]
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

            var channel = options == null ? GrpcChannel.ForAddress(address) : GrpcChannel.ForAddress(address, options);
            ExecCallBusiness(address, cusOptions, channel, headers, eventId, action, exAction);
        }

        /// <summary>
        /// 执行回调业务
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="options">自定义配置</param>
        /// <param name="channel">已经创建的渠道</param>
        /// <param name="headers">头</param>
        /// <param name="eventId">事件ID</param>
        /// <param name="action">回调业务动作</param>
        /// <param name="exAction">发生异常回调，如果为null，则不会捕获异常</param>
        private static void ExecCallBusiness(string address, ChannelCustomerOptions options, GrpcChannel channel, Metadata headers, 
            string eventId, Action<GrpcChannel, Metadata> action, Action<RpcException> exAction)
        {
            RpcException rpcEx = null;
            Exception exce = null;
            Stopwatch watch = new Stopwatch();
            try
            {
                watch.Start();
                action(channel, headers);
                watch.Stop();
            }
            catch (RpcException ex)
            {
                watch.Stop();
                exce = rpcEx = ex;
            }
            catch (Exception ex)
            {
                watch.Stop();
                exce = ex;
            }
            finally
            {
                channel.Dispose();
            }

            if (App.InfoEvent != null)
            {
                App.InfoEvent.RecordAsync($"grpc发起请求地址:{address}.接口:{options.Api}.耗时:{watch.ElapsedMilliseconds}ms",
                    exce, "CreateChannel", eventId, address, options.Api);
            }
            if (rpcEx != null && exAction != null)
            {
                exAction(rpcEx);
            }
            else if (exce != null)
            {
                throw new Exception(exce.Message, exce);
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

        /// <summary>
        /// 获取GRpc客户端
        /// 需要在App.GetGRpcClient里设置获取GRpc客户端工厂的实现
        /// </summary>
        /// <typeparam name="GRpcClientT">GRpc客户端类型</typeparam>
        /// <param name="action">回调业务处理方法</param>
        /// <param name="exAction">发生异常回调，如果为null，则不会捕获异常</param>
        /// <param name="customerOptions">自定义选项配置</param>
        /// <returns>GRpc客户端</returns>
        public static GRpcClientT GetGRpcClient<GRpcClientT>(Action<GRpcClientT, Metadata> action, Action<RpcException> exAction = null, Action<ChannelCustomerOptions> customerOptions = null)
            where GRpcClientT : ClientBase<GRpcClientT>
        {
            if (App.GetGRpcClientFactory == null)
            {
                throw new ArgumentNullException("App.GetGRpcClientFactory未定义");
            }
            var client = App.GetGRpcClientFactory.GetRpcClient<GRpcClientT>();
            if (action != null)
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

                RpcException rpcEx = null;
                Exception exce = null;
                Stopwatch watch = new Stopwatch();
                try
                {
                    watch.Start();
                    action(client, headers);
                    watch.Stop();
                }
                catch (RpcException ex)
                {
                    watch.Stop();
                    exce = rpcEx = ex;
                }
                catch (Exception ex)
                {
                    watch.Stop();
                    exce = ex;
                }

                if (App.InfoEvent != null)
                {
                    App.InfoEvent.RecordAsync($"grpc发起请求.接口:{cusOptions.Api}.耗时:{watch.ElapsedMilliseconds}ms",
                        exce, "GetGRpcClient", eventId, cusOptions.Api);
                }
                if (rpcEx != null && exAction != null)
                {
                    exAction(rpcEx);
                }
                else if (exce != null)
                {
                    throw new Exception(exce.Message, exce);
                }
            }

            return client;
        }
    }    
}
