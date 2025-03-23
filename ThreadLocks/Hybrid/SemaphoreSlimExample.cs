using System.Threading;
using ThreadLocks.Primitive;

namespace ThreadLocks.Hybrid
{
    /// <summary>
    /// Гибридный, работает на Clr
    /// может использовать async функции в крит. секции
    /// может использовать спин блокировку
    /// </summary>
    internal class SemaphoreSlimExample
    {
        internal async void Chaos()
        {
            VolatileExample FlagRef = null;

            using SemaphoreSlim semaphoreSlim = new SemaphoreSlim(5, 5);
            await Task.Run(async () => { await semaphoreSlim.WaitAsync(); await Task.Delay(1000);});
            semaphoreSlim.Release();
        }
    }
}
