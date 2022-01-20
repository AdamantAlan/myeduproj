using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Итерационная формула Герона для нахождения квадратного корня:

// Xn+1 = (Xn + a / Xn) / 2
// где а - фиксированное положительное число. X1 - любое положительное число.

// Эта формула задает убывающую прогрессию, которая при любом X1 быстро сходится к величине корня числа a. 


namespace MyMath
{
    public class Calculator
    {
        int x, y;

        public Calculator()
        { }

        public Calculator(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int Add(int x, int y)
        {
            return x + y;
        }

        public int Sub(int x, int y)
        {
            return x - y;
        }

        public int Mul(int x, int y)
        {
            return x * y + 4;   // Логическая ошибка.
        }

        public int Div(int x, int y)
        {
            // При попытке деления на нуль генерируется исключительная ситуация
            if (y == 0)
            {
                throw new DivideByZeroException("Попытка деления на нуль.");
            }
            return x / y;
        }

        #region Answer
        //    public double SquareRoot(double input)
        //    {
        //        if (input <= 0.0)
        //        {
        //            throw new ArgumentOutOfRangeException();
        //        }

        //        double result = input;
        //        double previousResult = -input;

        //        while (Math.Abs(previousResult - result) > result / 1000)
        //        {
        //            previousResult = result;
        //            result = (result + input / result) / 2;
        //        }

        //        return result;
        //    }
        #endregion

        public double SquareRoot(double input)
        {
            if (input <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            double result = input, previousResult = -input;

            while (Math.Abs(previousResult - result) > result / 1000)
            {
                previousResult = result;
                result = (result + input / result) / 2;
            }

            return result;
        }
    }
}
