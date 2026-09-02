namespace Algorithms.Patterns.BinarySearch
{
    /// <summary>
    /// Обычный бинарный поиск O(log(n))
    /// </summary>
    internal class BaseBinarySearch
    {
        /// <summary>
        /// Найти индекс числа target последнего вхождения
        /// </summary>
        public int GetIndex(int[] nums, int target)
        {
            var left = 0;
            var right = nums.Length;

            while (right - left > 1)
            {
                var half = left + (right - left) / 2;
                var halfNum = nums[half];

                if (halfNum > target) 
                    right = half;
                else 
                    left = half;
            }


            return nums[left] == target ? left : -1;
        }
    }
}
