using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Collections;
using CalculatorTest;

namespace _004_Assert
{
    class Program
    {
        static void Main()
        {
        }
    }

    [TestFixture]
    class MyClassTest
    {
        [Test]
        public void AreSame()
        {
            string a = "hello";
            string b = "world!";
            a = b;

            // AreSame - проверяет, ссылаются ли переменные на одну и ту же область памяти.
            Assert.AreSame(a, b);
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

    [TestFixture]
    class ComparisonsTest
    {
        private int a, b;

        [SetUp]
        public void Init()
        {
            a = 10;
            b = 20;
        }

        [Test]
        public void Greater()
        {
            // Проверяет, является ли один объект больше, чем другой (a > b).
            Assert.Greater(a, b);

            // a >= b.
            Assert.GreaterOrEqual(a, b);
        }

        [Test]
        public void Less()
        {
            // Проверяет, является ли один объект меньше, чем другой (a < b).
            Assert.Less(a, b);

            // a <= b.
            Assert.LessOrEqual(a, b);
        }
    }

    [TestFixture]
    class TypeAssertsTest
    {
        [Test]
        public void InInstanceOf()
        {
            // Методы для проверки типов объектов.
            Assert.IsInstanceOf(typeof(object), "Hello");
            Assert.IsNotInstanceOf(typeof(string), 5);
        }
    }

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
        [Ignore]
        public void Ignore()
        {
            throw new Exception();
           
            // Этот метод помечает тест, который будет игнорирован при запуске всех тестов
            // и не будет выполнен.
            Assert.Ignore("Ignore");
        }
    }
}
