﻿using Hzdtf.Utility.Json;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Json序列化选项扩展类
    /// @ 黄振东
    /// </summary>
    public static class JsonSerializerOptionsExtensioins
    {
        /// <summary>
        /// 添加默认的JSON选项
        /// </summary>
        /// <param name="builder">MVC生成器</param>
        /// <param name="jsonOptions">JSON配置回调</param>
        /// <returns>MVC生成器</returns>
        public static IMvcBuilder AddDefaultJsonOptions(this IMvcBuilder builder, Action<JsonOptions> jsonOptions = null)
        {
            builder.AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                 options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                 options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                 options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                 options.JsonSerializerOptions.Converters.Add(new DateTimeLocalJsonConvert());
                 options.JsonSerializerOptions.Converters.Add(new DateTimeNullLocalJsonConvert());

                 if (jsonOptions == null)
                 {
                     return;
                 }

                 jsonOptions(options);
             });

            return builder;
        }
    }
}
