using System.Text.RegularExpressions;

namespace NTQ.Sdk.Core.Utilities
{
    public static class NamingConventionUtils
    {
        public static string ToKebabCase(this string o) => Regex.Replace(o, "(\\w)([A-Z])", "$1-$2").ToLower();
        public static string ToSnakeCase(this string o) => Regex.Replace(o, "(\\w)([A-Z])", "$1_$2").ToLower();
    }
}