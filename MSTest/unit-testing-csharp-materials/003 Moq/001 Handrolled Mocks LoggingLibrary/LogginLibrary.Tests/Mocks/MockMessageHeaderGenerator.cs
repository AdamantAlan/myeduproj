using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogginLibrary.Tests.Mocks
{
    public class MockMessageHeaderGenerator : IMessageHeaderGenerator
    {
        public bool CreateHeaderWasCalled { get; private set; }
        public void CreateHeader(LogLevel level)
        {
            CreateHeaderWasCalled = true;
        }
    }
}
