using CodingTools.Infrastructure.Api;
using CodingTools.Infrastructure.Extension.Module;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using CodingTools.API.Extension.Swagger.Filters;

namespace CodingTools.API.Extension.Swagger.Filters
{
    /// <summary>
    /// Enum类型 参数过滤器
    /// </summary>
    public class XEnumNamesParameterFilter : IParameterFilter
    {
        /// <summary>
        /// 申请
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <param name="context">上下文</param>
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            var typeInfo = context.ParameterInfo.ParameterType;
            if (typeInfo.IsEnum)
            {
                var names = Enum.GetNames(context.ParameterInfo.ParameterType);
                parameter.Extensions.Add("x-enumNames", new Microsoft.OpenApi.Any.OpenApiString(JsonConvert.SerializeObject(typeInfo.GetFieldAndDescription())));

            }
            else if (typeInfo.IsGenericType && !parameter.Extensions.ContainsKey("x-enumNames"))
            {
                foreach (var genericArgumentType in typeInfo.GetGenericArguments())
                {
                    if (genericArgumentType.IsEnum)
                    {
                        if (!parameter.Extensions.ContainsKey("x-enumNames"))
                        {
                            parameter.Extensions.Add("x-enumNames", new Microsoft.OpenApi.Any.OpenApiString(JsonConvert.SerializeObject(genericArgumentType.GetFieldAndDescription())));

                        }
                    }
                }
            }
        }
    }
}