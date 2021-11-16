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
            foreach (var methodToRemote in typeDefinition.Methods.Where(x => x.IsPrivate || x.IsAssembly || x.IsFamilyOrAssembly || x.IsFamilyAndAssembly).ToList())
            {
                var visibleAttrExist = methodToRemote.HasAttribute("MakeVisibleAttribute");
                if (!visibleAttrExist)
                {
                    typeDefinition.Methods.Remove(methodToRemote);
                }
            }

            //no access from outside
            foreach (var propertyToRemote in typeDefinition.Properties.Where(x => x.GetMethod == null).ToList())
            {
                var visibleAttrExist = propertyToRemote.HasAttribute("MakeVisibleAttribute");
                if (!visibleAttrExist)
                {
                    typeDefinition.Properties.Remove(propertyToRemote);
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
