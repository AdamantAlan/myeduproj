namespace Algorithms.Patterns.Graph
{
    internal class InDegree
    {
        public static Dictionary<string, int> BuildIndegree(Dictionary<string, List<string>> graph)
        {
            var indegree = new Dictionary<string, int>();

            foreach (var node in graph)
            {
                if (!indegree.ContainsKey(node.Key))
                    indegree[node.Key] = 0;

                foreach (var neighbor in node.Value)
                {
                    if (!indegree.ContainsKey(neighbor))
                        indegree[neighbor] = 0;

                    indegree[neighbor]++;
                }
            }

            return indegree;
        }

        public static List<string> KahnTopologicalSort(Dictionary<string, List<string>> graph)
        {
            var indegree = BuildIndegree(graph);

            var queue = new Queue<string>(
                indegree
                    .Where(x => x.Value == 0)
                    .Select(x => x.Key)
            );

            var order = new List<string>();

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                order.Add(node);

                foreach (var neighbor in graph[node])
                {
                    indegree[neighbor]--;

                    if (indegree[neighbor] == 0)
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }

            if (order.Count != indegree.Count)
                return new List<string>();

            return order;
        }
    }
}
