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
using System.Linq;

namespace CodingTools.API.Extension.Swagger.Filters
{
    /// <summary>
    /// 属性/参数原始名称 Model类型过滤器
    /// </summary>
    public class OriginalNameSchemaFilter : ISchemaFilter
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
                    if (apiProperty.MemberInfo?.Name == "profession")
                    {


                    }
                    string propertyName = apiProperty.MemberInfo?.Name;

                    var memberAttributes = apiProperty.MemberInfo?.GetInlineOrMetadataTypeAttributes() ?? Enumerable.Empty<object>();

                    schema.Properties[apiProperty.ApiName]?.Extensions.Add("orginalName", new OpenApiString(string.IsNullOrWhiteSpace(propertyName) ? apiProperty.ApiName : propertyName));

                }
            }
             
            //属性是枚举的暂无法处理

        }
    }

}