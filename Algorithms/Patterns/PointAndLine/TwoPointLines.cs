namespace Algorithms.Patterns.PointAndLine
{
    /// <summary>
    /// Два указателя на отрезках O(n+m)
    /// найти пересечение в нескольких списках отрезков или временных промежутках
    /// </summary>
    internal class TwoPointLines
    {
        /// <summary>
        /// Даны массивы отрезков, объединить их
        /// </summary>
        /// <returns></returns>
        public List<(int X, int Y)> GetIntersectedLines(List<(int X, int Y)> lines1, List<(int X, int Y)> lines2) 
        {
            var p1 = 0;
            var p2 = 0;
            var result = new List<(int X, int Y)>();

            while (p1 < lines1.Count && p2 < lines2.Count)
            {
                if (Math.Max(lines1[p1].X, lines2[p2].X) <= Math.Min(lines1[p1].Y, lines2[p2].Y))
                {
                    result.Add((Math.Max(lines1[p1].X, lines2[p2].X), Math.Min(lines1[p1].Y, lines2[p2].Y)));
                }

                if (lines1[p1].Y < lines2[p2].Y)
                    p1++;
                else
                    p2++;
            }

            return result;
        }
    }
}
