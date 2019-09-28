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
    /// 自定义特性类型 Model类型过滤器
    /// </summary>
    public class CustomerAttributeSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// 申请
        /// </summary>
        /// <param name="schema">结构</param>
        /// <param name="context">上下文</param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.ApiModel is ApiObject apiObject)
            {
                foreach (var apiProperty in apiObject.ApiProperties)
                {
                    string name = apiProperty.MemberInfo?.Name;
                    var memberAttributes = apiProperty.MemberInfo?.GetInlineOrMetadataTypeAttributes() ?? Enumerable.Empty<object>();

                    foreach (var attribute in memberAttributes)
                    {
                        if (attribute is DataTypeAttribute dataTypeAttribute && !string.IsNullOrWhiteSpace(dataTypeAttribute.CustomDataType))
                        {
                            schema.Properties[apiProperty.ApiName].Format = dataTypeAttribute.CustomDataType;
                        }
                    }
                }
            }
        }
    }

}