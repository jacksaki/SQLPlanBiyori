using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLPlanBiyori.Common;
public class TimeService
{
    public static void SetTimeProvider(TimeProvider provider)
    {
        Provider = provider;
    }
    private static TimeProvider Provider
    {
        get; set;
    } = TimeProvider.System;

    public static DateTimeOffset UtcNow => Provider.GetUtcNow();
    public static DateTimeOffset Now => Provider.GetLocalNow();
}
