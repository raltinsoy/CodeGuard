using Fody;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGuard.Fody
{
    public class ModuleWeaver : BaseModuleWeaver
    {
        public override bool ShouldCleanReference => true;

        private MethodReference _objectConstructor;

        public override void Execute()
        {
            var objectDefinition = FindTypeDefinition("System.Object");
            var constructorDefinition = objectDefinition.Methods.First(x => x.IsConstructor);
            _objectConstructor = ModuleDefinition.ImportReference(constructorDefinition);

            var types = ModuleDefinition.GetAllTypes();
            var _typeDefinition = types.Last();

            RemoveNotVisibleDefinitions(_typeDefinition);

            var methodsToVisit = _typeDefinition.GetMethods().Concat(_typeDefinition.GetConstructors())
                .Where(method => method.HasBody && !method.IsAbstract);

            var reflectionType = typeof(SDKExportException);
            var exceptionCtor = reflectionType.GetConstructor(new Type[] { });
            var exceptionConstructorReference = ModuleDefinition.ImportReference(exceptionCtor);

            foreach (var method in methodsToVisit)
            {
                var attributeRemoved = method.RemoveAttribute("DoNotClearBodyAttribute");

                //pretend like not exist
                if (!attributeRemoved)
                {
                    ClearContentAndAddException(method, exceptionConstructorReference);
                }
            }
        }

        private void RemoveNotVisibleDefinitions(TypeDefinition typeDefinition)
        {
            foreach (var methodToRemote in typeDefinition.Methods.Where(x => x.IsPrivate || x.IsAssembly || x.IsFamilyOrAssembly || x.IsFamilyAndAssembly).ToList())
            {
                var attributeRemoved = methodToRemote.RemoveAttribute("MakeVisibleAttribute");

                //pretend like not exist
                if (!attributeRemoved)
                {
                    typeDefinition.Methods.Remove(methodToRemote);
                }
            }

            //no access from outside
            foreach (var propertyToRemote in typeDefinition.Properties.Where(x => x.GetMethod == null).ToList())
            {
                var attributeRemoved = propertyToRemote.RemoveAttribute("MakeVisibleAttribute");

                //pretend like not exist
                if (!attributeRemoved)
                {
                    typeDefinition.Properties.Remove(propertyToRemote);
                }
            }

            foreach (var fieldToRemote in typeDefinition.Fields.Where(x => x.IsPrivate || x.IsAssembly || x.IsFamilyOrAssembly || x.IsFamilyAndAssembly).ToList())
            {
                var attributeRemoved = fieldToRemote.RemoveAttribute("MakeVisibleAttribute");

                //pretend like not exist
                if (!attributeRemoved)
                {
                    typeDefinition.Fields.Remove(fieldToRemote);
                }
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
            method.Body.Variables.Clear();
            //method.Body.MaxStackSize = 8; //TODO: not working

            var ilProcessor = method.Body.GetILProcessor();

            var attributeRemoved = method.RemoveAttribute("DoNotThrowExceptionAttribute");
            //pretend like not exist
            if (!attributeRemoved)
            {
                ilProcessor.Append(ilProcessor.Create(OpCodes.Nop));
                ilProcessor.Append(ilProcessor.Create(OpCodes.Newobj, exceptionConstructorReference));
                ilProcessor.Append(ilProcessor.Create(OpCodes.Throw));
            }
            else
            {
                if (method.IsConstructor)
                {
                    AddEmptyCtor(ilProcessor);
                }
                //TODO: else
            }
        }

        private void AddEmptyCtor(ILProcessor ilProcessor)
        {
            ilProcessor.Append(Instruction.Create(OpCodes.Ldarg_0));
            ilProcessor.Append(Instruction.Create(OpCodes.Call, _objectConstructor));
            ilProcessor.Append(Instruction.Create(OpCodes.Nop));
            ilProcessor.Append(Instruction.Create(OpCodes.Nop));
            ilProcessor.Append(Instruction.Create(OpCodes.Ret));
        }
    }
}
