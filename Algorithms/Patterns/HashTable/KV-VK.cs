using System.Text;

namespace Algorithms.Patterns.HashTable
{
    /// <summary>
    /// key value - value key O(n)
    /// задача на сортировку по частотности
    /// нужно искать топ-k по некоторому свойству
    /// </summary>
    /// <remarks>
    /// Подсчет элементов - key value
    /// инверсия ключа - value key(составление списка частотности)
    /// формирование результата
    /// </remarks>
    internal class KV_VK
    {
        /// <summary>
        /// Вывести символы строки столько сколько раз они встречаются в строке и отсортировать по частотности
        /// </summary>
        public string GetStringCharDuplicated(string word)
        {
            var kv = new Dictionary<char, int>();

            foreach (var w in word)
            {
                kv[w] = kv.GetValueOrDefault(w, 0) + 1;
            }

            var vk = new SortedDictionary<int, List<char>>(Comparer<int>.Create((x, y) => y.CompareTo(x)));

            foreach (var v in kv)
            {
                var value = vk.GetValueOrDefault(v.Value, []);
                value.Add(v.Key);
            }

            var stringBuilder = new StringBuilder("");
            foreach (var v in vk)
            {
                for (var i = 0; i < v.Key; i++)
                    stringBuilder.Append(v.Value);
            }

            return stringBuilder.ToString();
        }
    }
}
