using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _003_Calculator_MSUnit
{
    class Program
    {
        static void Main()
        {
            Calculator c = new Calculator();

            Console.WriteLine("3 + 5 = {0}", c.Add(3, 5));
            Console.WriteLine("6 - 2 = {0}", c.Sub(6, 2));
            Console.WriteLine("8 * 3 = {0}", c.Mul(8, 3));
            Console.WriteLine("6 / 3 = {0}", c.Div(6, 3));

            Console.ReadKey();
        }
    }
}
