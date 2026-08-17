namespace Algorithms.Patterns.Stack
{
    /// <summary>
    /// Псевдо стэк O(n)
    /// стэк можно заменить на счетчики
    /// одинаковые элементы
    /// важен только размер
    /// </summary>
    internal class PseudoStack
    {
        /// <summary>
        /// Посмотреть все ли открытые скобки закрыты корректно(одного вида)
        /// </summary>
        /// <returns></returns>
        public bool GetCorrectSkobki(char[] chars)
        {
            var onBalance = (char c) => c is '(' ? 1 : -1;
            var balance = 0;

            foreach (char c in chars)
            {
                balance += onBalance(c);
            }

            return balance == 0;
        }
    }
}
