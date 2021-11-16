using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGuard.Fody
{
    public partial class ModuleWeaver
    {
        private HashSet<string> _typeLevelAttributeNames = new HashSet<string>
        {
            "CodeGuard.MakeVisibleAttribute",
            "CodeGuard.DoNotClearBodyAttribute",
            "CodeGuard.DoNotThrowExceptionAttribute"
        };

        public void CleanAttributes()
        {
            foreach (var type in _types)
            {
                ProcessType(type);
            }
        }

        private void ProcessType(TypeDefinition type)
        {
            RemoveAttributes(type, _typeLevelAttributeNames);

            foreach (var property in type.Properties)
            {
                RemoveAttributes(property, _typeLevelAttributeNames);
            }

            foreach (var field in type.Fields)
            {
                RemoveAttributes(field, _typeLevelAttributeNames);
            }

            foreach (var method in type.Methods)
            {
                RemoveAttributes(method, _typeLevelAttributeNames);
            }
        }

        internal static void RemoveAttributes(ICustomAttributeProvider member, IEnumerable<string> attributeNames)
        {
            if (!member.HasCustomAttributes)
                return;

            var attributes = member.CustomAttributes
                .Where(attribute => attributeNames.Contains(attribute.Constructor.DeclaringType.FullName));

            foreach (var customAttribute in attributes.ToList())
            {
                member.CustomAttributes.Remove(customAttribute);
            }
        }
    }
}
