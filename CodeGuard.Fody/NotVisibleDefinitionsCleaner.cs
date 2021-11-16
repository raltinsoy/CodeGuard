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
        public void CleanNotVisibleDefinitions()
        {
            foreach (var typeDefinition in _types)
            {
                RemoveNotVisibleDefinitions(typeDefinition);
            }
        }

        private void RemoveNotVisibleDefinitions(TypeDefinition typeDefinition)
        {
            foreach (var methodToRemote in typeDefinition.Methods.Where(x =>
                (!x.IsGetter) && //skip Property
                (!x.IsSetter) && //skip Property
                (x.IsPrivate || x.IsAssembly || x.IsFamilyOrAssembly || x.IsFamilyAndAssembly)).ToList())
            {
                var visibleAttrExist = methodToRemote.HasAttribute("MakeVisibleAttribute");
                if (!visibleAttrExist)
                {
                    typeDefinition.Methods.Remove(methodToRemote);
                }
            }

            //no access from outside
            foreach (var propertyToRemote in typeDefinition.Properties.ToList())
            {
                var visibleAttrExist = propertyToRemote.HasAttribute("MakeVisibleAttribute");
                if (!visibleAttrExist)
                {
                    if (propertyToRemote.GetMethod != null &&
                        (propertyToRemote.GetMethod.IsPrivate ||
                         propertyToRemote.GetMethod.IsAssembly ||
                         propertyToRemote.GetMethod.IsFamilyOrAssembly ||
                         propertyToRemote.GetMethod.IsFamilyAndAssembly))
                    {
                        typeDefinition.Methods.Remove(propertyToRemote.GetMethod);
                        typeDefinition.Properties.Remove(propertyToRemote);
                    }

                    if (propertyToRemote.SetMethod != null &&
                        (propertyToRemote.SetMethod.IsPrivate ||
                         propertyToRemote.SetMethod.IsAssembly ||
                         propertyToRemote.SetMethod.IsFamilyOrAssembly ||
                         propertyToRemote.SetMethod.IsFamilyAndAssembly))
                    {
                        typeDefinition.Methods.Remove(propertyToRemote.SetMethod);
                        typeDefinition.Properties.Remove(propertyToRemote);
                    }
                }
            }

            foreach (var fieldToRemote in typeDefinition.Fields.Where(x => x.IsPrivate || x.IsAssembly || x.IsFamilyOrAssembly || x.IsFamilyAndAssembly).ToList())
            {
                var visibleAttrExist = fieldToRemote.HasAttribute("MakeVisibleAttribute");
                if (!visibleAttrExist)
                {
                    typeDefinition.Fields.Remove(fieldToRemote);
                }
            }
        }
    }
}
