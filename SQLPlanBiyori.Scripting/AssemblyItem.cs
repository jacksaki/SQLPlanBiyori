using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SQLPlanBiyori.Scripting
{
    [DataContract]
    public class AssemblyItem
    {
        [DataMember(Name = "full_name")]
        public string FullName { get; set; }

    }
}
