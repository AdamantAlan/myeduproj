using System.Drawing;

namespace Algorithms.Patterns.PointAndLine
{
    /// <summary>
    /// Метод отрезков O(n*log(n))
    /// нужно объединить,
    /// найти пересечение в одном списке отрезков или временных промежутках
    /// </summary>
    internal class LinesMethod
    {
        /// <summary>
        /// Дан массив отрезков, объединить объединяемое
        /// </summary>
        public List<(int X, int Y)> UnionIntersectedLines(List<(int X, int Y)> lines)
        {
            lines = lines.OrderBy(line => line.X).ToList();

            for (int i = 0; i < lines.Count; i++)
            {
                if(i + 1 > lines.Count) break;

                if (lines[i + 1].X <= lines[i].Y)
                {
                    var line = lines[i + 1];
                    line.X = lines[i].X;

                    lines.Remove(lines[i]);
                }
            }

            return lines;
        }
    }
}
