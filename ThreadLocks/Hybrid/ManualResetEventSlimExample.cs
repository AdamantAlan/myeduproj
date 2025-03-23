using ThreadLocks.Primitive;

namespace ThreadLocks.Hybrid
{
    /// <summary>
    /// Гибридный, работает на Clr
    /// может использовать спин блокировку
    /// </summary>
    internal class ManualResetEventSlimExample
    {
        internal void Chaos()
        {
            VolatileExample flag = null;
            using ManualResetEventSlim manualResetEventSlim = new ManualResetEventSlim(false);
            //Это как светофор для для всех машин на дороге, но включать зеленый и красный нужно самому
            Task.Run(() => { manualResetEventSlim.Wait(); flag = new(); manualResetEventSlim.Reset(); });
            manualResetEventSlim.Set();
        }
    }
}
