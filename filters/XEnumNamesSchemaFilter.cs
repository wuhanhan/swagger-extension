using CodingTools.Infrastructure.Api;
using CodingTools.Infrastructure.Extension.Module;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingTools.API.Extension.Swagger.Filters
{
    /// <summary>
    /// Enum类型 Model类型过滤器
    /// </summary>
    public class XEnumNamesSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// 申请
        /// </summary>
        /// <param name="schema">结构</param>
        /// <param name="context">上下文</param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var typeInfo = context.ApiModel.Type;
            if (typeInfo.IsEnum)
            {
                schema.Extensions.Add("x-enumNames", new Microsoft.OpenApi.Any.OpenApiString(JsonConvert.SerializeObject(typeInfo.GetFieldAndDescription())));
            }
            else if (schema.Enum.Count > 0)
            {
                //var names = schema.Enum.Select(n => n.ToString());
                //schema.Extensions.Add("x-enumNames", new Microsoft.OpenApi.Any.OpenApiString(string.Join('$', names)));
            }
            else if (typeInfo.IsGenericType && !schema.Extensions.ContainsKey("x-enumNames"))
            {
                foreach (var genericArgumentType in typeInfo.GetGenericArguments())
                {
                    if (genericArgumentType.IsEnum)
                    {
                        if (!schema.Extensions.ContainsKey("x-enumNames"))
                        {
                            schema.Extensions.Add("x-enumNames", new Microsoft.OpenApi.Any.OpenApiString(JsonConvert.SerializeObject(genericArgumentType.GetFieldAndDescription())));
                        }
                          
                    }
                }
            }
        }
    }

}