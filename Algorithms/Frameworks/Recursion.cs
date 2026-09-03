namespace Algorithms.Frameworks
{
    internal class Recursion
    {
        void Dfs(Node node, int sum)
        {
            if (node is null)
                return;

            sum += node.Value;

            Dfs(node.Left, sum);
            Dfs(node.Right, sum);
        }

        int Dfs(Node node)
        {
            if (node is null)
                return 0;

            var left = Dfs(node.Left);
            var right = Dfs(node.Right);

            return Math.Max(left, right) + 1;
        }
    }
}
