using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogginLibrary.Tests.Mocks
{
    public class MockMessageBodyGenerator : IMessageBodyGenerator
    {
        public bool CreateBodyWasCalled { get; private set; }
        public void CreateBody(string message)
        {
            CreateBodyWasCalled = true;
        }
    }
}
