using Microsoft.Extensions.DependencyInjection;
using NTQ.Sdk.Core.Filters;

namespace NTQ.Sdk.Core.Extensions
{
    public static class ErrorHandlingFilterConfig
    {
        public static void ConfigErrorHandlingFilter(this IServiceCollection services)
        {
            services.AddControllers(options => options.Filters.Add<ErrorHandlingFilter>());
        }
    }
}