namespace ThreadLocks.Primitive
{
    /// <summary>
    /// Примитив ядра
    /// </summary>
    internal class MutexExample
    {
        internal void Chaos()
        {
            VolatileExample flagRef = null;
            using var mutex = new Mutex();

            mutex.WaitOne();
            flagRef = new VolatileExample();
            mutex.ReleaseMutex();
        }

        internal void ChaosInterProcessing1()
        {
            VolatileExample flagRef = null;
            try
            {
                using var mutex = new Mutex(true, "Global\\VolatileExample\\MutexExample");
                mutex.WaitOne();
                flagRef = new VolatileExample();
                mutex.ReleaseMutex();
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                Console.WriteLine("Semaphore does not exist.");
            }
        }

        internal void ChaosInterProcessing2()
        {
            VolatileExample flagRef = null;
            try
            {
                using var mutex = new Mutex(true, "Global\\VolatileExample\\MutexExample");
                mutex.WaitOne();
                flagRef = new VolatileExample();
                mutex.ReleaseMutex();
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                Console.WriteLine("Semaphore does not exist.");
            }
        }
    }
}
