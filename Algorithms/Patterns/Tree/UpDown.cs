using Algorithms.Frameworks;

namespace Algorithms.Patterns.Tree
{
    internal class UpDown
    {
        //найти есть ли сумма target по узлам от вешины до листа
        private bool HasSum(Node? node, int sum, int target)
        {
            if (node is null)
                return false;

            sum += node.Value;

            // Проверяем только в листе
            if (node.Left is null && node.Right is null)
                return sum == target;

            return HasSum(node.Left, sum, target)
                || HasSum(node.Right, sum, target);
        }
    }
}
