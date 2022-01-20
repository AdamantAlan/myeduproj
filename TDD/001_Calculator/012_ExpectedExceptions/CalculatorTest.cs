using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ExpectedExceptions
{
    [TestFixture]
    class CalculatorTest
    {
        // Метод теста, помеченный атрибутом ExpectedException будет пройден только тогда, когда в его теле булет 
        // обнаружена исключительная ситуация того типа, который указан в параметре этого атрибута
        [Test]
        [ExpectedException(typeof(DivideByZeroException), ExpectedMessage = "Попытка деления на нуль.")]
        public static void TestCalculatorDivideByZeroException()
        {
            new Calculator().Div(1,0);
        }
    }
}
