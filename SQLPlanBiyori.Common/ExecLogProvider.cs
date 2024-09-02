using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLPlanBiyori.Common
{
    public class ExecLoggerProvider : ILoggerProvider
    {
        private readonly ExecLogService _logService;

        public ExecLoggerProvider(ExecLogService logService)
        {
            _logService = logService;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new ExecLogger(_logService);
        }

        public void Dispose() { }
    }
}
