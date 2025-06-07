using System.Runtime.InteropServices;
using System.Text;

namespace UnsafeCode.UnsafeServices
{
    public class PInvokeService
    {
        //типо СИ
        [DllImport("user32.dll")]
        public static extern int MessageBox(IntPtr hWnd, string text, string caption, int type);

        //контракт между си
        //Обязательно[StructLayout(LayoutKind.Sequential)]
        //Никаких ссылочных типов(только int, float, bool, byte, IntPtr, fixed)
        //Для массивов использовать fixed внутри unsafe struct, или передавать указатель(MyStruct*)
        [StructLayout(LayoutKind.Sequential)]
        public struct MyStruct
        {
            public int id;
            public float value;
        }


        //Делегат должен быть не GC-собран → сохранить его в поле/переменной
        //Указание CallingConvention важно(Cdecl, StdCall и т.п.)
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void Callback(int value);

        [DllImport("mylib.dll")]
        public static extern void SetCallback(Callback cb);

        //Указатели, void*, IntPtr
        //void* allocate_buffer(int size);
        //void free_buffer(void* ptr);

        //void* ↔ IntPtr
        //Можно кастовать: (byte*) ptr, (MyStruct *)ptr и т.д.

        [DllImport("mylib.dll")]
        public static extern IntPtr allocate_buffer(int size);

        [DllImport("mylib.dll")]
        public static extern void free_buffer(IntPtr ptr);


        #region example StructInvocation
        //        // C (example.h)
        //#pragma pack(push, 1)

        //        typedef struct MyData
        //        {
        //            int id;
        //            int values[4];
        //            const char* name;
        //            void* userData;
        //        }
        //        MyData;

        //__declspec(dllexport)
        //void ProcessData(MyData* data);

        //#pragma pack(pop)

        [DllImport("mylib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void ProcessData(MyData* data);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public unsafe struct MyData
        {
            public int id;

            // fixed массив (только в unsafe)
            public fixed int values[4];

            public sbyte* name;        // char* (ANSI)
            public IntPtr userData;    // void* указатель на объект
        }

        public unsafe void StructInvocation()
        {
            MyData data;
            data.id = 123;

            // fixed массив
            int* valuesPtr = data.values;
            for (int i = 0; i < 4; i++)
                valuesPtr[i] = i * 10;

            // строка (ANSI)
            string name = "TestName";
            byte[] nameBytes = Encoding.ASCII.GetBytes(name + '\0');
            fixed (byte* namePtr = nameBytes)
            {
                data.name = (sbyte*)namePtr;

                // указатель на управляемый объект (временно)
                var obj = new object();
                var handle = GCHandle.Alloc(obj, GCHandleType.Pinned); // предотвратить GC
                data.userData = GCHandle.ToIntPtr(handle);

                ProcessData(&data);
                handle.Free();
            }
        }
        #endregion

        #region example P/Invoke с колбэком (callback)

        //typedef void (* LogCallback) (const char* message, void* userData);
        //__declspec(dllexport)
        //void RegisterCallback(LogCallback cb, void* userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void LogCallback(sbyte* message, void* userData);

        [DllImport("mylib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RegisterCallback(LogCallback callback, IntPtr userData);

        unsafe static void MyCallback(sbyte* message, void* userData)
        {
            string msg = Marshal.PtrToStringAnsi((IntPtr)message);

            var handle = GCHandle.FromIntPtr((IntPtr)userData);

            var context = (string)handle.Target;

            Console.WriteLine($"[{context}] native log: {msg}");
        }

        public unsafe void CallbackInvocation()
        {
            string context = "CTX-1";
            var handle = GCHandle.Alloc(context, GCHandleType.Pinned); // чтобы GC не удалил строку

            LogCallback callback = MyCallback;

            RegisterCallback(callback, GCHandle.ToIntPtr(handle));

            GC.KeepAlive(callback);
            handle.Free();
        }
        #endregion
    }
}
