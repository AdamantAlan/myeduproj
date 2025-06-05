using System.Reflection.Metadata;
using System.Runtime.InteropServices;

namespace UnsafeCode.UnsafeServices
{

    //Копировать данные между управляемой и неуправляемой памятью Copy, PtrToStructure, StructureToPtr
    //Выделить / освободить неуправляемую память AllocHGlobal, FreeHGlobal
    //Получить размер структуры SizeOf<T>()
    //Получить указатель на управляемый объект UnsafeAddrOfPinnedArrayElement
    //Управлять COM-объектами ReleaseComObject, GetIUnknownForObject
    // Проверять типы и массивы IsComObject, IsComObject(object)
    public class MarshalService
    {
        unsafe public void Methods()
        {
            // Выделяем 100 байт в unmanaged heap
            {
                IntPtr ptr = Marshal.AllocHGlobal(100);
                Marshal.FreeHGlobal(ptr);
            }

            //Копирование массива в неуправляемую память
            {
                byte[] data = new byte[] { 1, 2, 3 };
                IntPtr ptr = Marshal.AllocHGlobal(data.Length);
                Marshal.Copy(data, 0, ptr, data.Length);
                Marshal.FreeHGlobal(ptr);
            }

            //StructureToPtr и PtrToStructure
            {
                //StructureToPtr
                MyStruct s = new MyStruct { X = 1, Y = 2.5f };
                var ptr = Marshal.AllocHGlobal(Marshal.SizeOf<MyStruct>());
                Marshal.StructureToPtr(s, ptr, false);

                //PtrToStructure
                MyStruct s2 = Marshal.PtrToStructure<MyStruct>(ptr);
                Marshal.FreeHGlobal(ptr);
            }

            // Получить адрес элемента массива
            //Marshal-методы не закрепляют управляемые объекты
            {
                byte[] arr = new byte[] { 10, 20, 30 };
                var handle = GCHandle.Alloc(arr, GCHandleType.Pinned);

                var ptr = Marshal.UnsafeAddrOfPinnedArrayElement(arr, 0);
                handle.Free();
                Marshal.FreeHGlobal(ptr);
            }

            //Размер структуры
            int size = Marshal.SizeOf<MyStruct>();
        }
    }

    struct MyStruct
    {
        public int X;
        public float Y;
    }
}
