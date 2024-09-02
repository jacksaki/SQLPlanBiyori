using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLPlanBiyori.Database;
public class DbSourceGeneratorResult
{
    public string DbContextPath
    {
        get; internal set;
    }

    public string[] EntityPaths
    {
        get; internal set;
    }
}
