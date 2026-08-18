namespace Algorithms.Patterns.PrefixArray
{
    /// <summary>
    /// Бегущий префикс O(n)
    /// требует суффикстного и префиксного массива, но можно заменить переменными
    /// не требует хранить все префиксы, достаточно бегущей суммы
    /// </summary>
    internal class RunPrefix
    {
        /// <summary>
        /// Найти индекс элемента, такого что сумма всех элементов слева равна сумме всех элементов справа
        /// </summary>
        /// <returns></returns>
        public int GetIndexThatLeftAndRightSumEqual(int[] nums) 
        {
            var totalSum = nums.Sum();
            var prefixSum = 0;
            var suffixSum = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                if (i < 1) continue;

                prefixSum += nums[i - 1];
                suffixSum += totalSum - prefixSum - nums[i];

                if(prefixSum == suffixSum)
                    return i;
            }

            return -1;
        }
    }
}
