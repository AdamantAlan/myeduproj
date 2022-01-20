using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingLibrary
{
    public class MessageBodyGenerator : IMessageBodyGenerator
    {
        // Создание тела сообщения
        public void CreateBody(string message)
        {
            Debug.Write("MessageBodyGenerator.CreateBody");
        }
    }
}
