using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NTQ.Sdk.Core.Attributes;

namespace NTQ.Sdk.Core.Utilities
{
    public static class TypeUtils
    {
        public static List<string> GetAllHiddenPropertiesName(this Type type)
        {
            List<string> listPropertiesName = new List<string>();
            foreach (var propertyInfo in type.GetProperties())
            {
                if (propertyInfo.CustomAttributes.Any(
                        (Func<CustomAttributeData, bool>)(x => x.AttributeType == typeof(HiddenAttribute))))
                {
                    if (propertyInfo.PropertyType.FullName != null && !propertyInfo.PropertyType.FullName.Contains("System") && propertyInfo.PropertyType.GetProperties().Length > 0)
                    {
                        foreach (PropertyInfo propertyChild in propertyInfo.PropertyType.GetProperties())
                        {
                            listPropertiesName.Add(propertyInfo.Name + "." + propertyChild.Name);
                        }
                    }

                    listPropertiesName.Add(propertyInfo.Name);
                }
            }

            return listPropertiesName;
        }
    }
}