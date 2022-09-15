using System;

namespace NTQ.Sdk.Core.Attributes
{
    public class ModelHiddenParamsAttribute : Attribute
    {
        public Type Type { get; set; }

        public ModelHiddenParamsAttribute(Type type)
        {
            Type = type;
        }
    }
}