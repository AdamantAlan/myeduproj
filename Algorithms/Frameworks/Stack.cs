using Algorithms.Patterns.Stack;

namespace Algorithms.Frameworks
{
    internal class Stack
    {
        private readonly TempResultStack _tempResultStack = new();
        private readonly MonotonousSTack _monotonousSTack = new();
        private readonly PseudoStack _pseudoStack = new();
    }
}
