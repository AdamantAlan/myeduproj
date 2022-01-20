using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Converter;

namespace _001_TDD
{
    class Program
    {
        static void Main(string[] args)
        {
            UahConverter converter = new UahConverter(0.25, -10, 8);

            converter.Value = 100;
            converter.OutputCurrency = Currency.EUR;

            Console.WriteLine(converter.Value);
        }

    }
}
