namespace Algorithms.Patterns.SlidingWindow
{
    /// <summary>
    /// Пересекающиеся окна O(n)
    /// найти самую длинную/короткую последовательность с определенными свойствами
    /// элементы окна пересекаются с другими окнами
    /// </summary>
    /// <remarks>
    /// Инициализация l=0, r = -1, res = 0
    /// Инициализация окна
    /// цикл l < nums.Lenght
    /// расширение окна
    /// обработка окна
    /// переход на новое окно
    /// </remarks>
    internal class IntersectingWindows
    {
        public int GetMaxLenghtWindowWithOne(int[] nums, int maxChangeZeroToOne)
        {
            var left = default(int);
            var right = -1;
            var maxLenghtWindowWithOne = 0;
            var zerosCount = 0;

            while (left < nums.Length)
            {
                while (right + 1 < nums.Length && (nums[right + 1] == 1 || zerosCount < maxChangeZeroToOne))
                {
                    if (nums[right + 1] == 0)
                        zerosCount++;

                    right++;
                }

                maxLenghtWindowWithOne = Math.Max(maxLenghtWindowWithOne, right - left + 1);

                if (nums[left] == 0)
                    zerosCount--;

                left++;
            }

            return maxLenghtWindowWithOne;
        }
    }
}