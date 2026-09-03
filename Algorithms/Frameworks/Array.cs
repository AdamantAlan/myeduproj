namespace Algorithms.Frameworks
{
    internal class Array
    {
        void Reverse(int[] nums)
        {
            var left = 0;
            var right = nums.Length - 1;

            while (left < right)
            {
                (nums[left], nums[right]) = (nums[right], nums[left]);

                left++;
                right--;
            }
        }

        int RemoveSortDuplicates(int[] nums)
        {
            if (nums.Length == 0)
                return 0;

            var slow = 0;

            for (var fast = 1; fast < nums.Length; fast++)
            {
                if (nums[slow] != nums[fast])
                {
                    slow++;
                    nums[slow] = nums[fast];
                }
            }

            return slow + 1;
        }

        int RemoveDuplicates(int[] nums)
        {
            var seen = new HashSet<int>();
            var slow = 0;

            for (var fast = 0; fast < nums.Length; fast++)
            {
                if (seen.Add(nums[fast]))
                {
                    nums[slow] = nums[fast];
                    slow++;
                }
            }

            return slow;
        }

        int SecondMax(int[] nums)
        {
            var max = int.MinValue;
            var secondMax = int.MinValue;

            foreach (var num in nums)
            {
                if (num > max)
                {
                    secondMax = max;
                    max = num;
                }
                else if (num > secondMax && num < max)
                {
                    secondMax = num;
                }
            }

            return secondMax;
        }

        int[] TopK(int[] nums, int k)
        {
            var heap = new PriorityQueue<int, int>();

            foreach (var num in nums)
            {
                heap.Enqueue(num, num);

                if (heap.Count > k)
                    heap.Dequeue();
            }

            var result = new int[heap.Count];

            for (var i = result.Length - 1; i >= 0; i--)
                result[i] = heap.Dequeue();

            return result;
        }

        void QuickSort(int[] nums, int left, int right)
        {
            if (left >= right) 
                return;

            var pivot = nums[(left + right) / 2];

            var i = left;
            var j = right;

            while (i <= j)
            {
                while (nums[i] < pivot)
                    i++;

                while (nums[j] > pivot)
                    j--;

                if(i <= j)
                {
                    (nums[i], nums[j]) = (nums[j], nums[i]);

                    i++;
                    j--;
                }
            }

            QuickSort(nums, left, j);
            QuickSort(nums, i, right);
        }
    }
}
