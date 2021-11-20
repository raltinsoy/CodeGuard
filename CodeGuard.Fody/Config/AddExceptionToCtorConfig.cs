using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CodeGuard.Fody
{
    public partial class ModuleWeaver
    {
        public bool AddExceptionToCtorConfig = true;

        public void ResolveAddExceptionToCtorConfig()
        {
            var value = Config?.Attributes("AddExceptionToCtor")
                .Select(a => a.Value)
                .SingleOrDefault();
            if (value != null)
            {
                AddExceptionToCtorConfig = XmlConvert.ToBoolean(value.ToLowerInvariant());
            }
        }
    }
}
