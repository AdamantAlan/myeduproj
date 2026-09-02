using System;

namespace Algorithms.Patterns.BinarySearch
{
    /// <summary>
    /// Двойной бинарный поиск O(log(n))
    /// </summary>
    internal class DoubleBinarySearch
    {
        public (int start, int end) GetRange(int[] nums, int target) 
        {
            var left = -1;
            var right = nums.Length - 1;
            var first = -1;

            while (right - left > 1)
            {
                var half = left + (right - left) / 2;
                var halfNum = nums[half];

                if (halfNum < target)
                    left = half;
                else
                    right = half;
            }

            first = nums[right] == target ? right : -1;

            left = 0;
            right = nums.Length;
            var last = -1;

            while (right - left > 1)
            {
                var half = left + (right - left) / 2;
                var halfNum = nums[half];

                if (halfNum <= target)
                    left = half;
                else
                    right = half;
            }
            last = nums[left] == target ? left : -1;

            return (first, last);
        }
    }
}
