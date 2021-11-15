﻿using Fody;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKGenerator.Fody
{
    public class ModuleWeaver : BaseModuleWeaver
    {
        public override bool ShouldCleanReference => true;

        public override void Execute()
        {
            var types = ModuleDefinition.GetAllTypes();
            var _typeDefinition = types.Last();

            RemoveNotVisibleDefinitions(_typeDefinition);

            var methodsToVisit = _typeDefinition.GetMethods().Concat(_typeDefinition.GetConstructors())
                .Where(method => method.HasBody && !method.IsAbstract);

            var reflectionType = typeof(Exception);
            var exceptionCtor = reflectionType.GetConstructor(new Type[] { });
            var exceptionConstructorReference = ModuleDefinition.ImportReference(exceptionCtor);

            foreach (var method in methodsToVisit.ToList())
            {
                ClearContentAndAddException(method, exceptionConstructorReference);
            }
        }

        private void RemoveNotVisibleDefinitions(TypeDefinition typeDefinition)
        {
            foreach (var methodToRemote in typeDefinition.Methods.Where(x => x.IsPrivate || x.IsAssembly || x.IsFamilyOrAssembly || x.IsFamilyAndAssembly).ToList())
            {
                typeDefinition.Methods.Remove(methodToRemote);
            }

            //no access from outside
            foreach (var propertyToRemote in typeDefinition.Properties.Where(x => x.GetMethod == null).ToList())
            {
                typeDefinition.Properties.Remove(propertyToRemote);
            }

            foreach (var fieldToRemote in typeDefinition.Fields.Where(x => x.IsPrivate || x.IsAssembly || x.IsFamilyOrAssembly || x.IsFamilyAndAssembly).ToList())
            {
                typeDefinition.Fields.Remove(fieldToRemote);
            }
        }

        //TODO: bu listeye sonra bak
        public override IEnumerable<string> GetAssembliesForScanning()
        {
            //yield return "netstandard";
            //yield return "mscorlib";
            yield break;
        }

        private void ClearContentAndAddException(MethodDefinition method, MethodReference exceptionConstructorReference)
        {
            method.Body.Instructions.Clear();

            var ilProcessor = method.Body.GetILProcessor();

            ilProcessor.Append(Instruction.Create(OpCodes.Nop));
            ilProcessor.Append(ilProcessor.Create(OpCodes.Newobj, exceptionConstructorReference));
            ilProcessor.Append(ilProcessor.Create(OpCodes.Throw));
        }
    }
}