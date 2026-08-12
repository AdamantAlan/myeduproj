namespace Algorithms.Patterns.PointAndLine
{
    /// <summary>
    /// Метод точекO(n*log(n))
    /// найти максимальное число одновременных событий
    /// </summary>
    /// <remarks>
    /// превращаем отрезки в точки вида [x1, +1], [y1, -1], [x2, +1], [y2, -1]...
    /// </remarks>
    internal class PointsMethod
    {
        /// <summary>
        /// Найти минимальное количество переговорных комнат чтобы все встречи состоялись
        /// </summary>
        public int GetMinCountRoom(List<(int X, int Y)> lines)
        {
            var points = new List<(int X, int Y)> ();

            foreach (var line in lines)
            {
                points.Add((line.X, 1));
                points.Add((line.Y, -1));
            }
            points = points.OrderBy(p => p.X).ToList();

            var currentRoom = 0;
            var maxRooms = 0;

            foreach (var point in points)
            {
                _ = point.Y > 0 ? currentRoom++ : currentRoom--;
                maxRooms = maxRooms > currentRoom ? maxRooms : currentRoom;
            }

            return maxRooms;
        }
    }
}
