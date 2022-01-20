using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogginLibrary.Tests.Mocks
{
    class MockSensitiveDataScruber : ISensitiveDataScruber
    {
        public bool ClearSensitiveWasCalled { get; private set; }
        public string ClearSensitive(string message)
        {
            ClearSensitiveWasCalled = true;
            return message;
        }
    }
}
