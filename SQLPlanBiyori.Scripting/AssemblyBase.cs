using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SQLPlanBiyori.Scripting
{
    public abstract class AssemblyBase
    {
        public AssemblyItem ToAssemblyItem()
        {
            return new AssemblyItem() { FullName = this.FullName };
        }

        protected AssemblyBase(Assembly asm) 
        {
            this.Assembly = asm;
        }

        protected AssemblyBase(string path)
        {
            this.Assembly = System.Reflection.Assembly.LoadFile(path);
        }

        public abstract bool IsGlobal { get; }


        public string Name
        {
            get
            {
                return this.Assembly.GetName().Name;
            }
        }
        public string FullName
        {
            get
            {
                return this.Assembly.FullName;
            }
        }

        public Assembly Assembly
        {
            get;
        }
    }
}
