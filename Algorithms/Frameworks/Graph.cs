using Algorithms.Patterns.Graph;

namespace Algorithms.Frameworks
{
    internal class Graph
    {
        private ShortDistance shortDistance;

        private CountComponents countComponents;

        private InDegree inDegree;
    }

    public static class GraphExtensions
    {
        public static void AddEdge(this Dictionary<int, List<int>> graph, int from, int to)
        {
            if(!graph.ContainsKey(from))
                graph[from] = new List<int>();

            if(!graph.ContainsKey(to))
                graph[to] = new List<int>();

            graph[from].Add(to);
            graph[to].Add(from);
        }

        public static void BFS(this Dictionary<int, List<int>> graph, int from)
        {
            var visited = new bool[graph.Count];
            var queue = new Queue<int>();

            queue.Enqueue(from);
            visited[from] = true;

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                Console.WriteLine(current);

                foreach( var v in graph[current])
                {
                    if (!visited[v])
                    {
                        visited[v] = true;
                        queue.Enqueue(v);
                    }
                }
            }
        }

        public static void DFS(this Dictionary<int, List<int>> graph, int from)
        {
            var visited = new bool[graph.Count];
            var queue = new Stack<int>();

            queue.Push(from);
            visited[from] = true;

            while (queue.Count > 0)
            {
                var current = queue.Pop();

                Console.WriteLine(current);

                foreach (var v in graph[current])
                {
                    if (!visited[v])
                    {
                        visited[v] = true;
                        queue.Push(v);
                    }
                }
            }
        }
    }
}
