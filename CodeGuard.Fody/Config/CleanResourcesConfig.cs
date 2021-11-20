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
        public bool CleanResourcesConfig = true;

        public void ResolveCleanResourcesConfig()
        {
            var value = Config?.Attributes("CleanResources")
                .Select(a => a.Value)
                .SingleOrDefault();
            if (value != null)
            {
                CleanResourcesConfig = XmlConvert.ToBoolean(value.ToLowerInvariant());
            }
        }
    }
}
