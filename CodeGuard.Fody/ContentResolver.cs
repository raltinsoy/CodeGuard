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
    public partial class ModuleWeaver
    {
        public void ContentResolver()
        {
            foreach (var typeDefinition in _types)
            {
                var methodsToVisit = typeDefinition.GetMethods().Concat(typeDefinition.GetConstructors())
                    .Where(method => method.HasBody && !method.IsAbstract);

                foreach (var method in methodsToVisit)
                {
                    var notClearAttrExist = method.HasAttribute("DoNotClearBodyAttribute");
                    if (!notClearAttrExist)
                    {
                        ClearContentAndAddException(method);
                    }
                }
            }
        }

        private void ClearContentAndAddException(MethodDefinition method)
        {
            method.Body.Instructions.Clear();
            method.Body.Variables.Clear();
            //method.Body.MaxStackSize = 8; //TODO: not working

            var ilProcessor = method.Body.GetILProcessor();

            var notThrowExcAttrExist = method.HasAttribute("DoNotThrowExceptionAttribute");
            if (!notThrowExcAttrExist)
            {
                AddExceptionContent(ilProcessor);
            }
            else
            {
                if (method.IsConstructor)
                {
                    AddEmptyCtor(ilProcessor);
                }
                else
                {
                    AddEmpyContent(ilProcessor);
                }
            }

            method.Body.OptimizeMacros();
        }

        private void AddExceptionContent(ILProcessor ilProcessor)
        {
            ilProcessor.Append(ilProcessor.Create(OpCodes.Nop));
            ilProcessor.Append(ilProcessor.Create(OpCodes.Newobj, _sdkExportExceptionConstructorReference));
            ilProcessor.Append(ilProcessor.Create(OpCodes.Throw));
        }

        private void AddEmptyCtor(ILProcessor ilProcessor)
        {
            ilProcessor.Append(Instruction.Create(OpCodes.Ldarg_0));
            ilProcessor.Append(Instruction.Create(OpCodes.Call, _objectConstructorReference));
            ilProcessor.Append(Instruction.Create(OpCodes.Ret));
        }

        private void AddEmpyContent(ILProcessor ilProcessor)
        {
            ilProcessor.Append(Instruction.Create(OpCodes.Nop));
            ilProcessor.Append(Instruction.Create(OpCodes.Ret));
        }
    }
}
