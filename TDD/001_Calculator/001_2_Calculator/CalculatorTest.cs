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
        static Calculator c = new Calculator();

        public static void TestOperations()
        {
            TestAddMethodReturnsRightValue();
            TestSubMethodReturnsRightValue();
            TestDivMethodReturnsRightValue();
            TestMulMethodReturnsRightValue();
            TestDivMethodGenerateDivideByZeroException();
        }

        public static void TestAddMethodReturnsRightValue()
        {
            try
            {
                if (13 == c.Add(6, 7))     // 6 + 7 = 13.
                    PrintSuccess(MethodBase.GetCurrentMethod().Name + "- успешно.");
                else
                    PrintError(MethodBase.GetCurrentMethod().Name + " - провален.");
            }
            catch (Exception ex)
            {
                PrintError(MethodBase.GetCurrentMethod().Name + " - провален.");
                Console.WriteLine(ex.Message);
            }
        }

        public static void TestSubMethodReturnsRightValue()
        {
            try
            {
                if (4 == c.Sub(6, 2))      // 6 - 2 = 4.
                    PrintSuccess(MethodBase.GetCurrentMethod().Name + " - успешно.");
                else
                    PrintError(MethodBase.GetCurrentMethod().Name + " - провален.");
            }
            catch (Exception ex)
            {
                PrintError(MethodBase.GetCurrentMethod().Name + " - провален.");
                Console.WriteLine(ex.Message);
            }

        }

        public static void TestDivMethodReturnsRightValue()
        {
            try
            {
                if (3 == c.Div(6, 2))      // 6 / 2 = 3.
                    PrintSuccess(MethodBase.GetCurrentMethod().Name + " - успешно.");
                else
                    PrintError(MethodBase.GetCurrentMethod().Name + " - провален.");
            }
            catch (Exception ex)
            {
                PrintError(MethodBase.GetCurrentMethod().Name + " - провален.");
                Console.WriteLine(ex.Message);
            }

        }

        public static void TestMulMethodReturnsRightValue()
        {
            try
            {
                if (18 == c.Mul(6, 3))     // 6 * 3 = 18.
                    PrintSuccess(MethodBase.GetCurrentMethod().Name + " - успешно.");
                else
                    PrintError(MethodBase.GetCurrentMethod().Name + " - провален.");
            }
            catch (Exception ex)
            {
                PrintError(MethodBase.GetCurrentMethod().Name + " - провален.");
                Console.WriteLine(ex.Message);
            }
        }

        public static void TestDivMethodGenerateDivideByZeroException()
        {
            try
            {
                c.Div(6, 0);    // DivideByZeroException
                PrintError(MethodBase.GetCurrentMethod().Name + " - провален.");
            }
            catch (DivideByZeroException ex)
            {
                PrintSuccess(MethodBase.GetCurrentMethod().Name + " - успешно.");
            }
            catch (Exception ex)
            {
                PrintError(MethodBase.GetCurrentMethod().Name + " - провален.");
            }
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
