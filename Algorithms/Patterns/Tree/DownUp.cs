using Algorithms.Frameworks;

namespace Algorithms.Patterns.Tree
{
    internal class DownUp
    {
        public int CountNodes(Node head)
        {
            //Посчитать узлы снизу вверх
            CountDown(head);

            //Посчитать самый длинный путь в дереве
            var diameter = 0;
            MaxPath(head, ref diameter);

            return diameter;
        }

        //Посчитать узлы снизу вверх
        private int CountDown(Node node)
        {
            if(node is null)
                return 0;

            var left = CountDown(node);
            var right = CountDown(node);

            return left + right + 1;
        }

        //Посчитать самый длинный путь в дереве
        private int MaxPath(Node node, ref int diameter)
        {
            if (node is null)
                return 0;

            var left = MaxPath(node.Left, ref diameter);
            var right = MaxPath(node.Right, ref diameter);

            diameter = Math.Max(diameter, left + right);

            return Math.Max(left, right) + 1;
        }

        //Является ли дерево сбалансированным те высота узла для каждого левого и правого поддерева
        //отличается не больше чем на 1
        private int IsBalance(Node node, ref bool isBalance)
        {
            if (node is null)
                return 0;

            var left = IsBalance(node.Left, ref isBalance);
            var right = IsBalance(node.Right, ref isBalance);

            if (Math.Abs(left - right) > 1)
                isBalance = false;

            return Math.Max(left, right) + 1;
        }
    }
}
