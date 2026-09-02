namespace Algorithms.Patterns.Graph
{
    public class ShortDistance
    {
        public int GetShortDistance(Dictionary<int, List<(int node, bool isEmpty)>> graph, (int node, bool isEmpty) from, (int node, bool isEmpty) to)
        {
            var visited = new bool[graph.Count];
            var queue = new Queue<(int node, int distance)>();

            queue.Enqueue((from.node, 0));
            visited[from.node] = true;

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();

                if(currentNode.node == to.node) return currentNode.distance;

                foreach (var v in graph[currentNode.node])
                {
                    if (visited[v.node] is false && v.isEmpty)
                    {
                        visited[v.node] = true;
                        queue.Enqueue((v.node, currentNode.distance + 1));
                    }
                }
            }

            return -1;
        }

        public static int ShortestPath(int[][] graph)
        {
            var start = FindStart(graph);

            var queue = new Queue<(int row, int col, int dist)>();
            var visited = new HashSet<(int row, int col)>();

            queue.Enqueue((start.row, start.col, 0));
            visited.Add((start.row, start.col));

            var directions = new (int dr, int dc)[]
            {
        (1, 0),
        (-1, 0),
        (0, 1),
        (0, -1)
            };

            while (queue.Count > 0)
            {
                var (row, col, dist) = queue.Dequeue();

                if (InBounds(row, col, graph) && graph[row][col] == 3)
                    return dist;

                foreach (var (dr, dc) in directions)
                {
                    var newRow = row + dr;
                    var newCol = col + dc;

                    if (
                        InBounds(newRow, newCol, graph) &&
                        graph[newRow][newCol] != 1 &&
                        !visited.Contains((newRow, newCol))
                    )
                    {
                        queue.Enqueue((newRow, newCol, dist + 1));
                        visited.Add((newRow, newCol));
                    }
                }
            }

            return -1;
        }

        private static bool InBounds(int row, int col, int[][] graph)
        {
            return row >= 0 &&
                   row < graph.Length &&
                   col >= 0 &&
                   col < graph[0].Length;
        }

        private static (int row, int col) FindStart(int[][] graph)
        {
            for (var row = 0; row < graph.Length; row++)
            {
                for (var col = 0; col < graph[0].Length; col++)
                {
                    if (graph[row][col] == 2)
                        return (row, col);
                }
            }

            return (-1, -1);
        }
    }
}
