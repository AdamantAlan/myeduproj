namespace ThreadLocks.Primitive
{
    /// <summary>
    /// Примитивный, работает на clr
    /// синхронизирует выполнение алгоритма несколькими потоками(они все идут до барьера и ждут остальных пока они выполнятся, можно делать так несколько раз)
    /// </summary>
    internal class BarrierExample
    {
        internal void Chaos()
        {
            VolatileExample FlagRef = null;
            using var barrier = new Barrier(3, (x) => { Console.WriteLine("Ждем ве потоки и продолжаем работу"); });

            for (int i = 0; i < 3; i++)
                Task.Run(() => 
                {
                    Console.WriteLine("Много работы 1");
                    barrier.SignalAndWait();
                    Console.WriteLine("Много работы 2");
                    barrier.SignalAndWait();
                });

            FlagRef = new VolatileExample();
        }
    }
}
