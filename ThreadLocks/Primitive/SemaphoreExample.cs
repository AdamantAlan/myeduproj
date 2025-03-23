namespace ThreadLocks.Primitive
{
    /// <summary>
    /// Примитив ядра
    /// </summary>
    internal class SemaphoreExample
    {
        private static Semaphore semaphore = new Semaphore(3, 3);

        internal void Chaos()
        {
            VolatileExample FlagRef = null;
            semaphore.WaitOne(300);

            FlagRef = new VolatileExample();

            semaphore.Release();
        }

        internal void ChaosInterProcessing1()
        {
            VolatileExample FlagRef = null;
            using var shareSemaphore = new Semaphore(3, 3, "Global\\VolatileExample\\SemaphoreExample");


            semaphore.WaitOne();
            FlagRef = new VolatileExample();
            semaphore.Release();

        }

        internal void ChaosInterProcessing2()
        {
            VolatileExample FlagRef = null;
            try
            {
                using var shareSemaphore = Semaphore.OpenExisting("Global\\VolatileExample\\SemaphoreExample");
                semaphore.WaitOne();
                FlagRef = new VolatileExample();
                semaphore.Release();
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                Console.WriteLine("Semaphore does not exist.");
            }

        }
    }
}
