﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGuard.Fody
{
    public partial class ModuleWeaver
    {
        public void CleanResources()
        {
            //for WPF xamls etc.
            ModuleDefinition.Resources.Clear();
        }
    }
}
