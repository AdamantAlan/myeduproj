using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    // Класс для тестирования калькулятора.
    class CalculatorTest
    {
        public static void TestOperations()
        {
            Calculator c = new Calculator();

            if (3 == c.Div(6, 0))      // 6 / 2 = 3.
                PrintSuccess("Div - успешно.");
            else
                PrintError("Div - провален.");

            if (13 == c.Add(6, 7))     // 6 + 7 = 13.
                PrintSuccess("Add - успешно.");
            else
                PrintError("Add - провален.");

            if (4 == c.Sub(6, 2))      // 6 - 2 = 4.
                PrintSuccess("Sub - успешно.");
            else
                PrintError("Sub - провален.");        

            if (18 == c.Mul(6, 3))     // 6 * 3 = 18.
                PrintSuccess("Mul - успешно.");
            else
                PrintError("Mul - провален.");
        }

        private static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private static void PrintSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
