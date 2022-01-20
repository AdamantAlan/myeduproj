using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingLibrary
{
    public class SensitiveDataScruber : ISensitiveDataScruber
    {
        // Удаление лишних значений из строки
        public string ClearSensitive(string message)
        {
            Debug.WriteLine("SensitiveDataScruber.ClearSensitive");
            return message;
        }
    }
}
