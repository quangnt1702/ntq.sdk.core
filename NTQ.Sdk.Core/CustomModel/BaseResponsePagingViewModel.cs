using System.Collections.Generic;

namespace NTQ.Sdk.Core.CustomModel
{
    public class BaseResponsePagingViewModel<T> 
    {
        public PagingMetadata Metadata { get; set; }
        public List<T> Data { get; set; }
    }
    
    public class PagingMetadata
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public int Total { get; set; }
    }
}