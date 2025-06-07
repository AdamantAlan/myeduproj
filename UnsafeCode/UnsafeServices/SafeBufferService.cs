using System.Runtime.InteropServices;

namespace UnsafeCode.UnsafeServices
{
    public sealed class MySafeBuffer : SafeBuffer
    {
        public MySafeBuffer() : base(true) { }

        public override bool IsInvalid => handle == IntPtr.Zero;

        public void Init(IntPtr pointer, uint size)
        {
            SetHandle(pointer); // передаём raw-указатель
            Initialize(size);  // инициализация SafeBuffer
        }

        protected override bool ReleaseHandle()
        {
            Marshal.FreeHGlobal(handle); // освобождаем память
            return true;
        }
    }

    public class SafeBufferService
    {
        public unsafe void Example()
        {
            IntPtr nativePtr = Marshal.AllocHGlobal(100);
            using (var buffer = new MySafeBuffer())
            {
                buffer.Init(nativePtr, 100);
                buffer.Write(4, 123);//int
                var buf = buffer.Read<int>(0);
            }
        }
    }
}
