using Algorithms.Patterns.Tree;

namespace Algorithms.Frameworks
{
    internal class Tree
    {
        private readonly DownUp downUp;
        private readonly UpDown upDown;
    }


    public class Node
    {
        public int Value { get; set; }

        public Node Left { get; set; }

        public Node Right { get; set; }
    }
}
