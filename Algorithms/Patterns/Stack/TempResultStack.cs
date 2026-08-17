namespace Algorithms.Patterns.Stack
{
    /// <summary>
    /// Стэк промежуточных результатов O(n)
    /// вычислить выражение
    /// промежуточные результаты
    /// </summary>
    internal class TempResultStack
    {
        /// <summary>
        /// Посмотреть что для каждой открывающей скобки есть закрывающая в правильной последовательности
        /// </summary>
        public bool HasOpenCloseSymbol(char[] chars)
        {
            var stack = new Stack<char>();

            foreach (char c in chars) 
            {
                if(MappingOpen.TryGetValue(c, out var open))
                {
                    stack.Push(c);
                }
                if (MappingClosed.TryGetValue(c, out var closed))
                {
                    var peek = stack.Peek();//exp если пустой

                    var needClosed = MappingOpen[peek];

                    if(closed == needClosed)
                        stack.Pop();
                    else 
                        return false;
                }
            }

            return stack.Count is 0;
        }

        private static readonly Dictionary<char, char> MappingOpen = new()
        {
            ['('] = ')',
            ['['] = ']',
            ['{'] = '}'
        };

        private static readonly Dictionary<char, char> MappingClosed = new()
        {
            [')'] = '(',
            [']'] = '[',
            ['}'] = '{'
        };
    }
}
