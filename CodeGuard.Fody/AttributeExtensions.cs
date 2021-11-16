using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGuard.Fody
{
    public static class AttributeExtensions
    {
        public static bool RemoveAttribute(this ICustomAttributeProvider attributeProvider, string attributeName)
        {
            var attribute = attributeProvider.CustomAttributes.FirstOrDefault(x => x.AttributeType.Name == attributeName);
            if (attribute != null)
            {
                return attributeProvider.CustomAttributes.Remove(attribute);
            }

            return false;
        }

        public static bool HasAttribute(this ICustomAttributeProvider attributeProvider, string attributeName)
        {
            return attributeProvider.CustomAttributes.Any(x => x.AttributeType.Name == attributeName);
        }
    }
}
