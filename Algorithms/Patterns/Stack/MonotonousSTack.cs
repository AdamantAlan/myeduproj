namespace Algorithms.Patterns.Stack
{
    /// <summary>
    /// Монотонный стэк 0(n)
    /// найти ближайший слева или справа
    /// </summary>
    internal class MonotonousSTack
    {
        /// <summary>
        /// Получить массив в котором берется вместо элемента первый элемент справа больший чем текущий либо -1
        /// [1,5,2,3] -> [5, -1, 3, -1]
        /// </summary>
        public List<int> Method(int[] nums)
        {
            var result = new List<int>(Enumerable.Repeat(-1, nums.Length));
            var stack = new Stack<int>();

            for(var i = nums.Length - 1; i >= 0; i--)
            {
                var current = nums[i];

                if(i == nums.Length - 1)
                {
                    stack.Push(current);
                    continue;
                }

                while (stack.Count > 0 && current > stack.Peek())
                {
                    stack.Pop();
                }

                if (current < stack.Peek())
                {
                    result[i] = stack.Peek();
                    stack.Push(current);
                }
            }

            return result;
        }
    }
}
