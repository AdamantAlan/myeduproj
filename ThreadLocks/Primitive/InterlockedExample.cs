namespace ThreadLocks.Primitive
{
    /// <summary>
    /// Примитив Clr
    /// </summary>
    internal class InterlockedExample
    {
        internal void Chaos()
        {
            long FlagValue = 0;
            string FlagString = "Пусто"; //а нам и не нужно, строки immutable
            VolatileExample FlagRef = null;

            Interlocked.Add(ref FlagValue, 5);
            Console.WriteLine(FlagValue);

            FlagValue = 0;
            Interlocked.Increment(ref FlagValue);

            FlagValue = 1;
            Interlocked.Decrement(ref FlagValue);

            FlagValue = 0;
            long oldValue = Interlocked.Exchange(ref FlagValue, 20);

            FlagValue = 0;
            oldValue = Interlocked.CompareExchange(ref FlagValue, 20, 0);

            var oldValueRef = Interlocked.CompareExchange(ref FlagRef, new VolatileExample(), null);
            oldValueRef = Interlocked.Exchange(ref FlagRef, new VolatileExample());
        }
    }
}
