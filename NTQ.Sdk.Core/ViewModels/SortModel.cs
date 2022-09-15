using System.Text.Json.Serialization;
using NTQ.Sdk.Core.Attributes;

namespace NTQ.Sdk.Core.ViewModels
{
    public abstract class SortModel
    {
        [JsonIgnore] [SortDirection] 
        [Skip]
        public string SortDirection { get; set; }
        [JsonIgnore] [SortProperty] 
        [Skip]
        public string  SortProperty { get; set; }
    }
}