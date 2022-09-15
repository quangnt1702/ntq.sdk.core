using System;

namespace NTQ.Sdk.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class StartTimeAttribute:Attribute
    {
        
    }
    
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class EndTimeAttribute:Attribute
    {
        
    }
    
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DateTimeFieldAttribute:Attribute
    {
        
    }
}