using Algorithms.Patterns.SlidingWindow;

namespace Algorithms.Frameworks
{
    internal class SlidingWindow
    {
       public NonIntersectingWindows NonIntersectingWindows = new();
       public IntersectingWindows IntersectingWindows = new();
       public FixedWindow FixedWindow = new();
    }
}
