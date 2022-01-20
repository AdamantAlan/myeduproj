using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Converter;

// Обнаружение различных исключительных ситуации при тестировании одного метода

namespace ConverterTest
{
    [TestFixture]
    public class ConverterTest
    {
        [Test]
        public void TestConverterReturnsRightValue()
        {
            UahConverter converter = new UahConverter(0.25, 10, 8);

            converter.OutputCurrency = Currency.EUR;
            converter.Value = 1000;

            Assert.AreEqual(100, converter.Value);
        }

        // Класс UahConverter может сгенерировать исключительную ситуацию типа ArgumentOutOfRangeException в двух случаях,
        // поэтому такой тест не всегда будет отрабатывать правильно 
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConverterGenereteArgumentException()
        {
                UahConverter converter = new UahConverter(0.25, 10, 8);

                converter.OutputCurrency = Currency.EUR;
                converter.Value = -1000;
        }

        // Этот тест проходит только в том случае, если исключительная ситуация ArgumentOutOfRangeException была сгенерированна при
        // передаче конструктору неправильных параметров
        [Test]
        public void TestConverterGenareteCtorArgumentException()
        {
            try
            {
                UahConverter converter = new UahConverter(0.25, -10, 8);

                converter.OutputCurrency = Currency.EUR;
                converter.Value = 1000;
            }
            catch (ArgumentException ex)
            {
                StringAssert.Contains(ex.Message, "Ctor");
                return;
            }

            Assert.Fail("No exception was thrown.");
        }

        // Этот тест проходит только в том случае, если исключительная ситуация ArgumentOutOfRangeException была сгенерированна при
        // передаче свойству Value неправильного значения
        [Test]
        public void TestConverterGenareteInputValueArgumentException()
        {
            try
            {
                UahConverter converter = new UahConverter(0.25, 10, 8);

                converter.OutputCurrency = Currency.EUR;
                converter.Value = -1000;
            }
            catch (ArgumentException ex)
            {
                StringAssert.Contains(ex.ParamName, "Value");
                return;
            }

            Assert.Fail("No exception was thrown.");
        }
    }
}
