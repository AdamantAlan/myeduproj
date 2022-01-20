using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogginLibrary.Tests.Mocks
{
    public class MockLoggingConfiguration : ILoggingConfiguration
    {
        public bool LogStackFor(LogLevel level)
        {
            return false;
        }
    }
}
