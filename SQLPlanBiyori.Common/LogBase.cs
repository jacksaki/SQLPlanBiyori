using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLPlanBiyori.Common
{
    public class LogBase
    {
        public LogBase(string text)
        {
            this.Date = TimeService.Now;
            this.Text = text;
        }
        public DateTimeOffset Date { get; }
        public string Text { get; }
    }
}
