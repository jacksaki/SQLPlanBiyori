using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SQLPlanBiyori.Scripting
{
    public class LoadedAssembly : AssemblyBase
    {
        public LoadedAssembly(string path) : base(path)
        {
            this.Path = path;
        }

        public string Path
        {
            get;
        }
        public override bool IsGlobal
        {
            get
            {
                return false;
            }
        }
    }
}
