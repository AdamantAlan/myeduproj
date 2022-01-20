using System;

namespace Calculator
{
    class Program
    {
        static void Main()
        {
            try
            {
                CalculatorTest.TestOperations();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
               
            Console.ReadKey();
        }
    }
}
