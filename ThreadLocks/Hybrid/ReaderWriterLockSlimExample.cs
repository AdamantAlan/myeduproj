using ThreadLocks.Primitive;

namespace ThreadLocks.Hybrid
{
    /// <summary>
    /// Гибридный, работает на Clr
    /// может использовать спин блокировку
    /// может использовать рекурсивную блокировка
    /// </summary>
    internal class ReaderWriterLockSlimExample
    {
        internal void Chaos()
        {
            VolatileExample FlagRef = null;

            using ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();

            Task.Run(() =>
            {
                rwLock.EnterWriteLock();

                FlagRef = new VolatileExample
                {
                    Flag = 1
                };

                rwLock.ExitWriteLock();
            });

            Task.Run(() => 
            {
                rwLock.EnterReadLock();
                Console.WriteLine(FlagRef!.Flag);
                rwLock.ExitReadLock();
            });
        }
    }
}
