using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingLibrary
{
    public class MessageHeaderGenerator : IMessageHeaderGenerator
    {
        // Создание заголовка сообщения
        public void CreateHeader(LogLevel level)
        {
            Debug.WriteLine("MessageHeaderGenerator.CreateHeader");
        }
    }
}
