namespace Algorithms.Patterns.TwoPointers
{
    /// <summary>
    /// С двух сторон O(n)
    /// по условию дан отсортированный массив
    /// задача на проверку палиндрома
    /// ответ формируется за счет сужения области с двух сторон
    /// </summary>
    /// <remarks>
    /// Работает только для отсортированного массива
    /// Сначала инициализация left и right
    /// цикл while l < r 
    /// логика движения указателей
    /// </remarks>
    internal class BothSides
    {
        /// <summary>
        /// Найти два числа в отсортированном массиве что дают сумму targetSum
        /// </summary>
        public (long left, long right) SearchTwoNumThatSumEqualTargetSum(int[] nums, int targetSum) 
        {
            var left = 0;
            var right = nums.Length - 1;
            int currSum = default(int);

            while (left < right)
            {
                currSum = nums[left] + nums[right];

                if (currSum == targetSum)
                {
                    return (left, right);
                }

                _ = currSum > targetSum ? --right : ++left;
            }

            return (-1, -1);
        }

        public bool IsPalindrome(string word)
        {
            var left = 0;
            var right = word.Length - 1;

            while (left < right)
            {
                var isEqual = word[left] == word[right];

                if(isEqual == false) return false;

                left++;
                right--;
            }

            return true;
        }
    }
}
