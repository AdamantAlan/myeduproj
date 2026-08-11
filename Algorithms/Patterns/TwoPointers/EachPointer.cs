namespace Algorithms.Patterns.TwoPointers
{
    /// <summary>
    /// Каждому по указателю O(n+m)
    /// по условию даны отсортированные массивы
    /// нужно искать объединение, пересечение
    /// </summary>
    /// <remarks>
    /// Работает только для отсортированного массива
    /// Сначала инициализация ставим указатели на начало каждого массива
    /// цикл while point1 < nums1.Lenght && point2 < nums2.Lenght
    /// логика движения указателей
    /// </remarks>
    internal class EachPointer
    {
        /// <summary>
        /// Даны два отсортированных массива разной длинны
        /// Найти пересекающиеся элементы
        /// </summary>
        public List<int> Intersect(int[] nums1, int[] nums2)
        {
            var pointNums1 = default(int); 
            var pointNums2 = default(int);
            var intersected = new List<int>();

            while (pointNums1 < nums1.Length && pointNums2 < nums2.Length)
            {
                if (nums1[pointNums1] == nums2[pointNums2])
                {
                    intersected.Add(nums1[pointNums1]);
                    pointNums1++;
                    pointNums2++;
                }

                _ = nums1[pointNums1] < nums2[pointNums2] ? ++pointNums1 : ++pointNums2;
            }

            return intersected;
        }

        /// <summary>
        /// Даны два отсортированных массива разной длинны
        /// нужно объединить в один отсортированный массив
        /// </summary>
        public List<int> Union(int[] nums1, int[] nums2)
        {
            var pointNums1 = default(int);
            var pointNums2 = default(int);
            var union = new List<int>();

            while (pointNums1 < nums1.Length && pointNums2 < nums2.Length)
            {
                if (nums1[pointNums1] == nums2[pointNums2])
                {
                    union.Add(nums1[pointNums1]);
                    pointNums1++;
                    pointNums2++;
                }

                if (nums1[pointNums1] < nums2[pointNums2])
                {
                    union.Add(nums1[pointNums1]);
                    pointNums1++;
                }
                if (nums1[pointNums1] > nums2[pointNums2])
                {
                    union.Add(nums1[pointNums2]);
                    pointNums2++;
                }
            }

            return union;
        }
    }
}
