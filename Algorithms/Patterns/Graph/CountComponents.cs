using Algorithms.Frameworks;

namespace Algorithms.Patterns.Graph
{
    internal class CountComponents
    {
        public static int CountConnectedComponents(int[][] edges)
        {
            var (graph, vertices) = BuildGraph(edges);

            var visited = new HashSet<int>();
            var components = 0;

            foreach (var node in vertices)
            {
                if (!visited.Contains(node))
                {
                    Bfs(node, graph, visited);
                    components++;
                }
            }

            return components;
        }

        public static (Dictionary<int, List<int>> graph, HashSet<int> vertices) BuildGraph(int[][] edges)
        {
            var graph = new Dictionary<int, List<int>>();
            var vertices = new HashSet<int>();

            foreach (var edge in edges)
            {
                var u = edge[0];
                var v = edge[1];

                graph.AddEdge(u, v);

                vertices.Add(u);
                vertices.Add(v);
            }

            return (graph, vertices);
        }

        public static void Bfs(
            int start,
            Dictionary<int, List<int>> graph,
            HashSet<int> visited)
        {
            var queue = new Queue<int>();

            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                foreach (var neighbor in graph[node])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }
    }
}
