namespace ThreadLocks.Primitive
{
    internal class VolatileExample
    {
        //убираем кэширование из регистров процессора
        internal volatile int Flag = 0;
    }
}
