using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLPlanBiyori.Common
{
    public class ExecLogger : ILogger
    {
        private readonly ExecLogService _logService;

        public ExecLogger(ExecLogService logService)
        {
            _logService = logService;
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                _logService.Log(formatter(state, exception));
            }
        }
    }
}
