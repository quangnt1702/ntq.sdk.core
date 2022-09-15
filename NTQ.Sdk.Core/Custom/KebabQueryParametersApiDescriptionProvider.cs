using System.Linq;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using NTQ.Sdk.Core.Utilities;

namespace NTQ.Sdk.Core.Custom
{
    public class KebabQueryParametersApiDescriptionProvider : IApiDescriptionProvider
    {
        public void OnProvidersExecuting(ApiDescriptionProviderContext context)
        {
            foreach (var parameter in context.Results.SelectMany(x => x.ParameterDescriptions)
                         .Where(x => x.Source.Id == "Query" || x.Source.Id == "ModelBinding"))
            {
                parameter.Name = parameter.Name.ToKebabCase();
            }
        }

        public void OnProvidersExecuted(ApiDescriptionProviderContext context)
        {
        }

        public int Order => 1;
    }
}