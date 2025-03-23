namespace ThreadLocks.Primitive
{
    /// <summary>
    /// Примитив ядра
    /// </summary>
    internal class AutoResetEventExample
    {
        internal void Chaos()
        {
            VolatileExample flag = null;
            using AutoResetEvent autoResetEvent = new AutoResetEvent(false);

            //Это как светофор для одной машины за раз
            Task.Run(() => { autoResetEvent.WaitOne(); flag = new(); });
            autoResetEvent.Set();
        }


        internal void ChaosInterProcessing1()
        {
            VolatileExample flag = null;
            using var autoResetEvent1 = new EventWaitHandle(false, EventResetMode.AutoReset, "Global\\VolatileExample\\AutoResetExample1");
            using var autoResetEvent2 = new EventWaitHandle(false, EventResetMode.AutoReset, "Global\\VolatileExample\\AutoResetExample2");

            autoResetEvent2.WaitOne();
            flag = new();
            autoResetEvent1.Set();
        }

        internal void ChaosInterProcessing2()
        {
            VolatileExample flag = null;
            using var autoResetEvent1 = new EventWaitHandle(false, EventResetMode.AutoReset, "Global\\VolatileExample\\AutoResetExample1");
            using var autoResetEvent2 = new EventWaitHandle(false, EventResetMode.AutoReset, "Global\\VolatileExample\\AutoResetExample2");

            autoResetEvent2.Set();
            flag = new();
            autoResetEvent1.WaitOne();
        }
    }
}

