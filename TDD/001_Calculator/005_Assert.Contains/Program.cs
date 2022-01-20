using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Collections;

namespace _005_Assert.Contains
{
    class Program
    {
        static void Main()
        {
        }
    }

    [TestFixture]
    class ContainsTest
    {
        [Test]
        public void Contains()
        {
            ArrayList array = new ArrayList();
            array.Add("Alex");
            array.Add("Serg");
            array.Add("John");

            // Contains используется для проверки объектов,
            // содержащихся в коллекции или массиве.
            Assert.Contains("Alex", array);
        }
    }
}
