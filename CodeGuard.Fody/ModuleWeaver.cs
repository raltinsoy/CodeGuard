using Fody;
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGuard.Fody
{
    public partial class ModuleWeaver : BaseModuleWeaver
    {
        public override bool ShouldCleanReference => true;

        private MethodReference _objectConstructorReference;

        private MethodReference _sdkExportExceptionConstructorReference;

        private IEnumerable<TypeDefinition> _types;

        public override void Execute()
        {
            var objectDefinition = FindTypeDefinition("System.Object");
            var constructorDefinition = objectDefinition.Methods.First(x => x.IsConstructor);
            _objectConstructorReference = ModuleDefinition.ImportReference(constructorDefinition);

            var reflectionType = typeof(SDKExportException);
            var exceptionCtor = reflectionType.GetConstructor(new Type[] { });
            _sdkExportExceptionConstructorReference = ModuleDefinition.ImportReference(exceptionCtor);

            _types = ModuleDefinition.GetTypes().Where(x => x.IsClass && x.BaseType != null);

            CleanNotVisibleDefinitions();
            ContentResolver();
            CleanAttributes();
        }

        //TODO: bu listeye sonra bak
        public override IEnumerable<string> GetAssembliesForScanning()
        {
            //yield return "netstandard";
            //yield return "mscorlib";
            yield break;
        }
    }
}
