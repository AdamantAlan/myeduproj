using ThreadLocks.Primitive;

namespace ThreadLocks.Hybrid
{
    /// <summary>
    /// Гибридный, работает на ядрах
    /// спин блокировка
    /// </summary>
    internal class SpinWaitExample
    {
        internal void Chaos()
        {
            VolatileExample FlagRef = null;
            bool spin = true;
            SpinWait spinWait = new SpinWait();

            Task.Run(() =>
            {
                while(spin) spinWait.SpinOnce();

                FlagRef = new VolatileExample
                {
                    Flag = 1
                };
            });

            spin = false;
        }
    }
}
