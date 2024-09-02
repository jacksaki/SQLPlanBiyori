using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace SQLPlanBiyori.Scripting
{
    public class GlobalAssembly:AssemblyBase
    {
        public GlobalAssembly(Assembly asm):base(asm)
        {
        }
        public override bool IsGlobal
        {
            get
            {
                return true;
            }
        }
        private static List<GlobalAssembly> _assemblies = null;
        public static IEnumerable<GlobalAssembly> GetAll()
        {
            if (_assemblies != null)
            {
                return _assemblies.OrderBy(x => x.Assembly.FullName); 
            }

            _assemblies = AssemblyLoadContext.Default.Assemblies.Select(a => new GlobalAssembly(a)).ToList();
            return _assemblies.OrderBy(x=>x.Assembly.FullName);
        }

    }
}
