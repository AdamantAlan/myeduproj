namespace ThreadLocks.Primitive
{
    /// <summary>
    /// Примитивный, работает на clr
    /// спин блокировка без Thread.Yield()
    /// </summary>
    internal class SpinLockExample
    {
        internal void Chaos()
        {
            VolatileExample FlagRef = null;
            bool spin = false;
            SpinLock spinLock = new SpinLock();

            Task.Run(() =>
            {
                spinLock.Enter(ref spin);
                FlagRef = new VolatileExample
                {
                    Flag = 1
                };
                spinLock.Exit();
            });

            spin = false;
        }
    }
}
