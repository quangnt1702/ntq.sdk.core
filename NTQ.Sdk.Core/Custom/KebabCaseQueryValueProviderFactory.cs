using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NTQ.Sdk.Core.Custom
{
    public class KebabCaseQueryValueProviderFactory : IValueProviderFactory
    {
        public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            KebabCaseQueryValueProvider item = new KebabCaseQueryValueProvider(BindingSource.Query,
                context.ActionContext.HttpContext.Request.Query, CultureInfo.CurrentCulture);
            context.ValueProviders.Add(item);
            return Task.CompletedTask;
        }
    }
}