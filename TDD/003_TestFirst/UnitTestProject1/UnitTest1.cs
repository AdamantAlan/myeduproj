using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyMath;

// Методология TDD

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        #region Step1
        // Шаг №1. Создание теста для проверки работы класса, который должен уметь расчитывать квадратный корень.
        [TestMethod]
        public void BasicRootTest()
        {
            // Создание экземпляра тестируемого класса   
            Calculator calc = new Calculator();

            // Определение входного и выходного значения
            double expectedResult = 234564;

            double input = expectedResult * expectedResult;

            // Запуск тестируемого метода
            double actualResult = calc.SquareRoot(input);

            // Проверка результата
            Assert.AreEqual(expectedResult, actualResult,
                delta: expectedResult / 1000);
        }
        #endregion

        #region Step2
        // Шаг №2. Класс Rooter должен уметь считать квадратные корни в диапазоне от 1*10^-8 до 1*10^8
        // С точностью не меньше одной тысячной предпологаемого результата
        [TestMethod]
        public void RooterValueRange()
        {
            // Создание экземпляра тестируемого класса
            Calculator calc = new Calculator();

            // Проверка работоспособности в диапазоне значений
            for (double expectedResult = 1e-8;
                expectedResult < 1e+8;
                expectedResult = expectedResult * 2)
            {
                RooterOneValue(calc, expectedResult);
            }
        }


        private void RooterOneValue(Calculator calc, double expectedResult)
        {
            double input = expectedResult * expectedResult;

            double actualResult = calc.SquareRoot(input);

            // Проверка ожидаемого результата с заданнной точностью
            Assert.AreEqual(expectedResult, actualResult,
                delta: expectedResult / 1000);
        }
        #endregion

        #region Step3
        // Шаг №3. При попытке расчета корня от отрицательного числа класс Rooter должен генерировать исключение типа ArgumentUotOfRangeException
        [TestMethod]
        public void RooterTestNegativeInputx()
        {
            Calculator calc = new Calculator();

            try
            {
                calc.SquareRoot(-10);
            }
            catch (ArgumentOutOfRangeException e)
            {
                return;
            }

            Assert.Fail(); // Тест не проходит если блок Catch не срабатывает
        }
        #endregion

    }
}
