namespace ThreadLocks.Primitive
{
    /// <summary>
    /// Примитивный, работает на clr
    /// откладывает выполнение кода, пока не вызовутся все сигналы(в примере это три сигнала)
    /// </summary>
    internal class CountdownEventExample
    {
        internal void Chaos()
        {
            VolatileExample FlagRef = null;
            using var countdownEvent = new CountdownEvent(3);

            for (int i = 0; i < 3; i++)
                Task.Run(() => { countdownEvent.Signal(); });

            countdownEvent.Wait();
            FlagRef = new VolatileExample();
        }
    }
}
