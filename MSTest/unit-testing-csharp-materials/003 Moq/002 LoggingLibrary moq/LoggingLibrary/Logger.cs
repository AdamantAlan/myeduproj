using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingLibrary
{
    public class Logger
    {
        private ISensitiveDataScruber _sensitiveDataScruber;
        private IMessageBodyGenerator _messageBodyGenerator;
        private IMessageHeaderGenerator _messageHeaderGenerator;
        private ILoggingConfiguration _loggingConfiguration;

        public Logger(ILoggingConfiguration loggingConfiguration, IMessageHeaderGenerator messageHeaderGenerator, 
            IMessageBodyGenerator messgaeBodyGenerator, ISensitiveDataScruber sensitiveDataScruber)
        {
            _sensitiveDataScruber = sensitiveDataScruber;
            _messageBodyGenerator = messgaeBodyGenerator;
            _messageHeaderGenerator = messageHeaderGenerator;
            _loggingConfiguration = loggingConfiguration;
        }

        public void CreateEntry(string message, LogLevel level)
        {
            _messageHeaderGenerator.CreateHeader(level);

            if (_loggingConfiguration.LogStackFor(level))
            {
                Console.Write("Stack: ");
            }

            string clearMessage = _sensitiveDataScruber.ClearSensitive(message);
            _messageBodyGenerator.CreateBody(clearMessage);
        }
    }
}
