using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLPlanBiyori.Common
{
    public class ExecLogService
    {
        public ObservableCollection<ExecLog> Logs { get; } = new ObservableCollection<ExecLog>();

        public void Log(string message)
        {
            Logs.Add(new ExecLog(message));
        }
    }
}
