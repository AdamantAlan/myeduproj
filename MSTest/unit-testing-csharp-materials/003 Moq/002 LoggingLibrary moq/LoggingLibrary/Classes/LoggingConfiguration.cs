using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingLibrary
{
    public class LoggingConfiguration : ILoggingConfiguration
    {
        // Определение необходимости логирования стэка вызовов
        public bool LogStackFor(LogLevel level)
        {
            if (level == LogLevel.Error)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
