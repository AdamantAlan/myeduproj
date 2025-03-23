namespace ThreadLocks.Primitive
{
    /// <summary>
    /// Примитив Clr
    /// </summary>
    internal class ReaderWriterLockExample
    {
        internal void Chaos()
        {
            VolatileExample flag = null;

            ReaderWriterLock readerWriterLock = new ReaderWriterLock();

            readerWriterLock.AcquireWriterLock(Timeout.Infinite);
            flag = new() { Flag = 3 };
            readerWriterLock.ReleaseWriterLock();

            readerWriterLock = new ReaderWriterLock();
            Console.WriteLine(flag.Flag);
            readerWriterLock.ReleaseReaderLock();
        }
    }
}
