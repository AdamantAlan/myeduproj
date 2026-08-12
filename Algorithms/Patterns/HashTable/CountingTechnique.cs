namespace Algorithms.Patterns.HashTable
{
    /// <summary>
    /// Техника подсчета O(n)
    /// работа с частотой элемента
    /// анаграммы/палиндромы
    /// </summary>
    /// <remarks>
    /// Подсчет элементов
    /// Обработка
    /// </remarks>
    internal class CountingTechnique
    {
        /// <summary>
        /// Понять может ли быть слово палиндромом если переставить его символы
        /// </summary>
        public bool IsPossiblePalindrome(string word)
        {
            var dict = new Dictionary<char, int>();

            foreach(var w in word)
            {
                dict[w] = dict.GetValueOrDefault(w, 0) + 1;
            }

            var countOdds = 0;
            foreach(var v in dict.Values)
            {
                if(v % 2 != 0) countOdds++;
                if(countOdds > 1) return false;
            }

            return true;
        }
    }
}
