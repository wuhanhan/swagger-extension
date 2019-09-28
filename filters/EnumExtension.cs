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

namespace CodingTools.API.Extension.Swagger.Filters
{
    /// <summary>
    /// Enum类型扩展
    /// </summary>
    public static class EnumTypeExtension
    {
        /// <summary>
        /// 获得字段和描述
        /// </summary>
        /// <param name="enumType">类型</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetFieldAndDescription(this Type enumType)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (enumType.IsEnum)
            {
                var fields = enumType.GetFields();
                fields?.ToList().ForEach(p =>
                {
                    var attributes = p.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);

                    if (attributes != null && attributes.Count() > 0)
                    {
                        if (attributes[0] is System.ComponentModel.DescriptionAttribute attribute)
                        {
                            dic.Add(p.Name, attribute.Description);
                        }
                        else
                        {
                            dic.Add(p.Name, p.Name);
                        }
                    }

                });

            }
            return dic;
        }
    }
}