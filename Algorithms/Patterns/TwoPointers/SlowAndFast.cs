namespace Algorithms.Patterns.TwoPointers
{
    /// <summary>
    /// Быстрый и медленный O(n)
    /// изменить исходную строку/массив
    /// сохранить исходный порядок
    /// </summary>
    /// <remarks>
    /// Сначала инициализация ставим указатели на начало массива
    /// цикл while fastPoint < nums.lenght
    /// логика движения указателей
    /// </remarks>
    internal class SlowAndFast
    {
        /// <summary>
        /// Вытеснить все 0 в конец массива(все не нулевые элементы в начало)
        /// </summary>
        public int[] MoveZeroToEndArray(int[] nums)
        {
            var slow = default(int);
            var fast = default(int);

            while (fast < nums.Length)
            {
                if (nums[fast] != 0)
                {
                    (nums[slow], nums[fast]) = (nums[fast], nums[slow]);

                    slow++;
                }

                fast++;
            }

            return nums;
        }

        /// <summary>
        /// Заменить все подряд идущие пробелы на один(странная задача, все равно null останется)
        /// </summary>
        public char?[] ChangeWhitespacesToOneWhitespace(char?[] chars)
        {
            var slow = default(int);
            var fast = default(int);

            while (fast < chars.Length)
            {
                var countWhitespaces = default(int);

                if (chars[fast] is ' ')
                { 
                    countWhitespaces++;
                }
                else if (countWhitespaces > 1)
                {
                    for (var i = slow; i < fast - 1; i++)
                    {
                        chars[i] = null;
                    }

                    countWhitespaces = default(int);
                    slow = fast;
                }

                fast++;
            }

            return chars;
        }
    }
}
