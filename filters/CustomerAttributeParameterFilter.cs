using CodingTools.Infrastructure.Api;
using CodingTools.Infrastructure.Extension.Module;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CodingTools.API.Extension.Swagger.Filters
{
    /// <summary>
    /// 自定义特性类型 参数过滤器
    /// </summary>
    public class CustomerAttributeParameterFilter : IParameterFilter
    {
        /// <summary>
        /// 申请
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <param name="context">上下文</param>
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            var apiProperty = context.PropertyInfo;
            var memberAttributes = apiProperty?.GetInlineOrMetadataTypeAttributes() ?? Enumerable.Empty<object>();

            foreach (var attribute in memberAttributes)
            {
                if (attribute is DataTypeAttribute dataTypeAttribute && !string.IsNullOrWhiteSpace(dataTypeAttribute.CustomDataType))
                {
                    parameter.Schema.Format = dataTypeAttribute.CustomDataType;
                }
            }


        }
    }

}