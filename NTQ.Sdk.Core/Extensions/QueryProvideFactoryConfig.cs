using Microsoft.Extensions.DependencyInjection;
using NTQ.Sdk.Core.Custom;

namespace NTQ.Sdk.Core.Extensions
{
    public static class QueryProvideFactoryConfig
    {
        public static void ConfigureQueryProvideFactory(this IServiceCollection services)
        {
            services.AddControllers(options =>
                options.ValueProviderFactories.Add(new KebabCaseQueryValueProviderFactory()));
        }
    }
}