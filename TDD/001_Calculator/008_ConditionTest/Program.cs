using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Collections;

namespace _008_ConditionTest
{
    class Program
    {
        static void Main()
        {
            Calculator calc = new Calculator();
            Console.WriteLine("2 + 2 = 4 is {0}", calc.Add(2, 2) == 4);

            Console.ReadKey();
        }
    }

    // Методы проверяют конкретные условия, названы по имени условия: IsTrue, IsFalse ...
    [TestFixture]
    class ConditionTest
    {
        Calculator calc;

        [SetUp]
        public void Init()
        {
            calc = new Calculator();
        }

        [Test]
        public void IsTrue()
        {
            // 2 + 2 = 4 - True.
            Assert.IsTrue(calc.Add(2, 2) == 4);
        }

        [Test]
        public void IsFalse()
        {
            // 3 * 6 = 20 - False.
            Assert.IsFalse(calc.Mul(3, 6) == 20);
        }

        [Test]
        public void IsNan()
        {
            double d = double.NaN;

            Assert.IsNaN(d);   // d is not a number.
            Assert.IsNaN(0 / 0f);   // 0/0f is not a number.
        }

        [Test]
        public void IsEmpty()
        {
            Assert.IsEmpty("");
            Assert.IsNotEmpty("Hello!");

            Assert.IsEmpty(new ArrayList());
            Assert.IsNotEmpty(new List<string> { "one", "two" });
        }
    }
}
