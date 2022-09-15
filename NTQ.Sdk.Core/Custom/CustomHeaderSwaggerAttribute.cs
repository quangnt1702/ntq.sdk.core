using System;
using System.Linq;
using Microsoft.OpenApi.Models;
using NTQ.Sdk.Core.Attributes;
using NTQ.Sdk.Core.Utilities;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NTQ.Sdk.Core.Custom
{
    public class CustomHeaderSwaggerAttribute : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hidden =
                context.MethodInfo.CustomAttributes.FirstOrDefault(
                    x => x.AttributeType == typeof(ModelHiddenParamsAttribute));
            if (hidden != null)
            {
                var type = hidden.ConstructorArguments.FirstOrDefault().Value;
                var propertyInfo = ((Type)type)?.GetAllHiddenPropertiesName();
                if (propertyInfo != null)
                    foreach (var s in propertyInfo)
                    {
                        if (operation.Parameters.Any(x => x.Name.ToKebabCase() == s.ToKebabCase()))
                        {
                            operation.Parameters.Remove(
                                operation.Parameters.FirstOrDefault(x => x.Name.ToKebabCase() == s.ToKebabCase()));
                        }
                    }
            }
        }
    }
}