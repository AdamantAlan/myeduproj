namespace Algorithms.Patterns.SlidingWindow
{
    /// <summary>
    /// Непересекающиеся окна O(n)
    /// нужно работать с подряд идущими группами элементов
    /// один элемент принадлежит одной группе(группы не пересекаются)
    /// </summary>
    /// <remarks>
    /// Инициализация
    /// цикл l < nums.Lenght
    /// расширение окна
    /// обработка окна
    /// переход на новое окно
    /// </remarks>
    internal class NonIntersectingWindows
    {
        /// <summary>
        /// Получить сжатый массив с подряд идущими числами в одном элементе
        /// </summary>
        public List<string> GetZipNums(int[] nums)
        {
            var left = default(int);
            var right = default(int);
            var result = new List<string>();

            while (left < nums.Length)
            {
                while (right + 1 < nums.Length && nums[right + 1] - nums[right] == 1)
                {
                    right++;
                }

                if (right != left)
                    result.Add($"{nums[left]}-{nums[right]}");
                else
                    result.Add(nums[left].ToString());

                left = ++right;
            }

            return result;
        }

        /// <summary>
        /// Сжать массив, оставить повторяющийся элемент и количество его повторений 
        /// </summary>
        public List<string> GetZipNumsWithCount(string[] nums)
        {
            var left = default(int);
            var right = default(int);
            var result = new List<string>();

            while (left < nums.Length)
            {
                while (right + 1 < nums.Length && nums[right + 1] == nums[right])
                {
                    right++;
                }

                if (right != left)
                {
                    result.Add($"{nums[left]}");
                    result.Add($"{right - left + 1}");
                }
                else
                    result.Add(nums[left].ToString());

                left = ++right;
            }

            return result;
        }
    }
}
