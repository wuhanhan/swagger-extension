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
    /// 属性/参数原始名称 参数过滤器
    /// </summary>
    public class OriginalNameParameterFilter : IParameterFilter
    {
        /// <summary>
        /// 申请
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <param name="context">上下文</param>
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            var propertyName = context.PropertyInfo?.Name;
            parameter.Extensions.Add("orginalName", new OpenApiString(string.IsNullOrWhiteSpace(propertyName) ? parameter.Name : propertyName));
        }
    }

}