using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace _007_TypeAsserts
{
    class Program
    {
        static void Main()
        {
        }
    }

    [TestFixture]
    class TypeAssertsTest
    {
        [Test]
        public void InInstanceOf()
        {
            // Методы для проверки типов объектов.
            Assert.IsInstanceOf(typeof(string), "Hello");
            Assert.IsNotInstanceOf(typeof(string), 5);
        }
    }
}
