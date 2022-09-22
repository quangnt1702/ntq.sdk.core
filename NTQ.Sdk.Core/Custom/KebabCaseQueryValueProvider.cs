using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NTQ.Sdk.Core.Utilities;

namespace NTQ.Sdk.Core.Custom
{
    public class KebabCaseQueryValueProvider : QueryStringValueProvider

    {
        public KebabCaseQueryValueProvider(BindingSource bindingSource, IQueryCollection values, CultureInfo culture) :
            base(bindingSource, values, culture)
        {
        }


        public override bool ContainsPrefix(string prefix)
        {
            return base.ContainsPrefix(prefix.ToKebabCase());
        }

        public override ValueProviderResult GetValue(string key)
        {
            return base.GetValue(key.ToKebabCase());
        }
    }
}