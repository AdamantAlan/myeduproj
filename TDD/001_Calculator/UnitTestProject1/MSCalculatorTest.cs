using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    // Класс для тестирования калькулятора.
    [TestClass]   // TestClass -  указывает, что класс содержит тестовый код.
    public class MSCalculatorTest
    {
        [TestMethod]   // TestMethod -  указывает, что метод содержит тестовый код.
        public void MSTestOperations()
        {
            Calculator c = new Calculator();

            Assert.AreEqual(8, c.Add(3, 5));    // 3 + 5 = 8.
            Assert.AreEqual(4, c.Sub(6, 2));    // 6 - 2 = 4.
            Assert.AreEqual(24, c.Mul(8, 3));   // 8 * 3 = 24.
            Assert.AreEqual(2, c.Div(6, 3));    // 6 / 3 = 2.
        }
    }
}
