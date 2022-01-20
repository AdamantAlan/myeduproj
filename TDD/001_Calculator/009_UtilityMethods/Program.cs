using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace _009_UtilityMethods
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Run tests!");

            Console.ReadKey();
        }
    }

    [TestFixture]
    class UtilityMethodsTest
    {
        [Test]
        public void Fail()
        {
            // Метод создает тест, который не будет пройден с сообщением Fail!.
            Assert.Fail("Fail!");
        }

        [Test]
        //[Ignore]
        public void Ignore()
        {
            // Этот метод помечает тест, который будет игнорирован при запуске всех тестов
            // и не будет выполнен.
            Assert.Ignore("Ignore");
        }
    }
}
