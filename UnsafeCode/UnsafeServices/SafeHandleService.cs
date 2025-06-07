using System.Runtime.InteropServices;
using System.Text;

namespace UnsafeCode.UnsafeServices
{
    public sealed class MySafeHandle : SafeHandle
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool CloseHandle(IntPtr handle);

        public MySafeHandle() : base(IntPtr.Zero, true) { }

        public override bool IsInvalid => handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            return CloseHandle(handle);
        }
    }

    public class SafeHandleService
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static unsafe extern MySafeHandle CreateFile(sbyte* fileName);

        public unsafe void Example()
        {
            string name = "TestName";
            byte[] nameBytes = Encoding.ASCII.GetBytes(name + '\0');
            fixed (byte* nameBytesPtr = nameBytes)
            {
                using (var handle = CreateFile((sbyte*)nameBytesPtr))
                {
                    // работа с файлом
                } // автоматически вызовется ReleaseHandle
            }
        }
    }
}
