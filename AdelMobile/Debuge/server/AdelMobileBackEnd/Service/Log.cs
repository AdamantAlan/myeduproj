using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdelMobileBackEnd.models
{
    public static class Log
    {
       internal static async Task LoggingAsync(Exception e, string nameMethod)
        {
            using (FileStream fs = new FileStream("state/log.txt", FileMode.Append, FileAccess.Write))
            {
                string message = $"[{DateTime.Now}] {nameMethod} ERROR: {e.Message}\n\n";
                byte[] error = System.Text.Encoding.Default.GetBytes(message);
                await fs.WriteAsync(error, 0, error.Length);
            }
        } 
    }
}
