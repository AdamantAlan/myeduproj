using Algorithms.Patterns.PrefixArray;

namespace Algorithms.Frameworks
{
    internal class PrefixArray
    {

        private readonly RunPrefix runPrefix;
        private readonly SumArray sumArray;

        public int[] ConvertToPrefixArray(int[] nums)
        {
            var prefixArray = new int[nums.Length + 1];
            prefixArray[0] = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                var prefixIndex = i + 1;
                prefixArray[prefixIndex] = prefixArray[i] + nums[i];
            }

            return prefixArray;
        }

        public int GetSum(int[] prefixIndex, int leftIndex, int rightIndex)
        {
            return prefixIndex[rightIndex + 1] - prefixIndex[leftIndex];
        }
    }
}
