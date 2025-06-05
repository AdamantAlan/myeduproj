using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnsafeCode.UnsafeServices
{

    //перемещать объекты в памяти во время сборки мусора,
    //освобождать неиспользуемые объекты
    //закрепить объект в памяти (Pinned)
    //препятствовать сборке объекта (Normal, Strong)
    //удерживать слабую ссылку на объект (Weak, WeakTrackResurrection)
    public class GCHandleService
    {
        unsafe public void Methods()
        {
            byte[] data = new byte[] { 1, 2, 3 };

            // Создаём закреплённый хэндл
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);

            // Проверим, закреплён ли объект
            if (!handle.IsAllocated) return;

            // Получаем указатель на начало массива
            IntPtr ptr = handle.AddrOfPinnedObject();

            // Доступ к объекту
            var recovered = (byte[])handle.Target;

            // Можно получить IntPtr
            var handlePtr = GCHandle.ToIntPtr(handle);
            // А потом обратно
            var ptrHandler = GCHandle.FromIntPtr(handlePtr);

            // Не забываем освободить
            handle.Free();
        }
    }
}
