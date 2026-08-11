namespace Algorithms.Patterns.SlidingWindow
{
    /// <summary>
    /// Окно фиксированной длинны O(n)
    /// дана переменная - размер окна
    /// требуется работать с подряд идущими элементами
    /// </summary>
    /// <remarks>
    /// Накопление окна
    /// цикл для перебора остальных окон
    /// переход к следующему окну
    /// </remarks>
    internal class FixedWindow
    {
        /// <summary>
        /// Вернуть максимальную сумму countNums подряд идущих элементов 
        /// </summary>
        public int GetSumKNums(int[] nums, in int countNums)
        {
            var windowSum = default(int);

            //Накопление - первое окно 
            for (int i = 0; i < countNums; i++)
                windowSum += nums[i];

            var maxSum = windowSum;

            for (int right = countNums; right < nums.Length; right++)
            {
                var left = right - countNums;
                windowSum = windowSum - nums[left] + nums[right];
                maxSum = maxSum > windowSum ? maxSum : windowSum;
            }

            return maxSum;
        }
    }
}
