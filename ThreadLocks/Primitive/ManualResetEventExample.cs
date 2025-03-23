namespace ThreadLocks.Primitive
{
    /// <summary>
    /// Примитив ядра
    /// </summary>
    internal class ManualResetEventExample
    {
        internal void Chaos()
        {
            VolatileExample flag = null;
            using ManualResetEvent manualResetEvent = new ManualResetEvent(false);

            //Это как светофор для для всех машин на дороге, но включать зеленый и красный нужно самому
            Task.Run(() => { manualResetEvent.WaitOne(); flag = new(); });
            manualResetEvent.Set();
            Thread.Sleep(1000);
            manualResetEvent.Reset();
        }


        internal void ChaosInterProcessing1()
        {
            VolatileExample flag = null;
            using var autoResetEvent1 = new EventWaitHandle(false, EventResetMode.ManualReset, "Global\\VolatileExample\\ManualResetExample1");
            using var autoResetEvent2 = new EventWaitHandle(false, EventResetMode.ManualReset, "Global\\VolatileExample\\ManualResetExample2");

            autoResetEvent2.WaitOne();
            flag = new();
            autoResetEvent1.Set();
        }

        internal void ChaosInterProcessing2()
        {
            VolatileExample flag = null;
            using var autoResetEvent1 = new EventWaitHandle(false, EventResetMode.ManualReset, "Global\\VolatileExample\\ManualResetExample1");
            using var autoResetEvent2 = new EventWaitHandle(false, EventResetMode.ManualReset, "Global\\VolatileExample\\ManualResetExample2");

            autoResetEvent2.Set();
            flag = new();
            autoResetEvent1.WaitOne();
            autoResetEvent1.Reset();
        }
    }
}
