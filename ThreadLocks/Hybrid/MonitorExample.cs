using ThreadLocks.Primitive;

namespace ThreadLocks.Hybrid
{
    /// <summary>
    /// Гибридный, работает на Clr
    /// может использовать спин блокировку
    /// может использовать рекурсивную блокировка
    /// </summary>
    internal class MonitorExample
    {
        private static object objLock = new object();
        internal void Chaos()
        {
            VolatileExample FlagRef = null;

            Task.Run( () => 
            { 
                Monitor.Enter(objLock); 
                Thread.Sleep(1000); 
                Monitor.Pulse(objLock); 
            });
        }
    }
}
