using System.Drawing;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;

namespace UnsafeCode.UnsafeServices
{
    //Как очищается память
    //Способ выделения                       Кто очищает?                          Как освободить
    //int[] arr = new int[10]     GC(сборщик мусора)                  Автоматически
    //fixed (int* p = arr)	      GC + временная "фиксация"           После выхода из блока fixed
    //Marshal.AllocHGlobal(...)   ручное выделение                    Marshal.FreeHGlobal(ручное)
    //stackalloc                  В стеке                             Автоматически после выхода из метода

    unsafe public class SimpleExampleService
    {
        //При работе с указателями теряем все методы
        //ТК работаем с "сырой" памятью
        public void UnsafePrimitive()
        {
            int persons = 10;
            int* personsCur = &persons;
            *personsCur += 50;

            Console.WriteLine(*personsCur);

            // получим адрес переменной
            ulong addressCur = (ulong)personsCur;
        }

        //Нельзя изменять строки, нужно переводить в char[]
        public void UnsafeString()
        {
            var text = "Hello!";
            char[] buffer = text.ToCharArray();

            unsafe
            {
                fixed (char* p = buffer)
                {
                    p[0] = 'H';
                }
            }

            string newText = new string(buffer); // "Hello!"
        }

        public void UnsafeArray()
        {

            int[] arr = new int[] { 1, 2, 3 };
            fixed (int* p = arr)
            {
                *(p + 1) = 42; //арифметика указателей учитывает размер типа (4 байта).
            }

            // Изменение через указатель
            float[] data = new float[1000];

            fixed (float* p = data)
            {
                for (int i = 0; i < 1000; i++)
                {
                    p[i] = i * 1.1f;
                }
            }

            //Коллекции — это управляемые объекты, и напрямую получить к ним T*нельзя.
            //Обходной путь: List<T> → .ToArray() или .GetBuffer()
            List<int> list = new List<int> { 1, 2, 3 };
            int[] arr2 = list.ToArray();


            fixed (int* p = arr)
            {
                *(p + 1) = 42;
            }

            //Альтернатива: Span<T> + MemoryMarshal
            //безопасно, но близко к указателям(span хранит указатель и длину массива)
            Span<int> span = stackalloc int[3] { 1, 2, 3 };

            for (int i = 0; i < span.Length; i++)
            {
                Console.WriteLine(span[i]);
            }

            //Пример: работа с List<string> через char* (посимвольно)
            List<string> originalStrings = new List<string>
        {
            "hello",
            "world",
            "unsafe"
        };

            // Преобразуем строки в массивы символов (char[])
            List<char[]> charArrays = originalStrings.ConvertAll(str => str.ToCharArray());
            foreach (char[] c in charArrays)
            {
                fixed (char* p = c)
                {
                    // Пример: заменим первую букву на 'X'
                    if (c.Length > 0)
                    {
                        p[0] = 'X';
                    }
                }
            }
            List<string> modifiedStrings = charArrays.ConvertAll(arr => new string(arr));
        }

        struct MyStruct
        {
            public int A;
            public int B;
        }
        struct MyStruct2
        {
            public int Id;
            public float Value;
        }
        struct StructWithRef
        {
            public string Name;
        }

        public void UnsafeStruct()
        {
            MyStruct s = new MyStruct { A = 1, B = 2 };
            MyStruct* structCur = &s;

            //Разыменование
            structCur->A = 50;
            (*structCur).B = 100;


            StructWithRef sRef = new StructWithRef { Name = "test" };

            fixed (char* p = sRef.Name)
            {
                Console.WriteLine(p[0]); // 't'
            }

            //Пример копирования структуры по указателю
            MyStruct s1 = new MyStruct { A = 1, B = 2 };
            MyStruct s2;

            unsafe
            {
                MyStruct* p1 = &s1;
                MyStruct* p2 = &s2;

                *p2 = *p1; // побитовая копия

                s2 = *p2; // разыменование — копия значений


                #region array struct
                MyStruct2[] array = new MyStruct2[]
                {
                    new MyStruct2 { Id = 1, Value = 1.1f },
                    new MyStruct2{ Id = 2, Value = 2.2f },
                    new MyStruct2 { Id = 3, Value = 3.3f }
                };

                unsafe
                {
                    fixed (MyStruct2* p = array)
                    {
                        for (int i = 0; i < array.Length; i++)
                        {
                            MyStruct2* itemPtr = p + i;     // указатель на элемент
                            MyStruct2 value = *itemPtr;     // разыменовываем
                            Console.WriteLine($"[{i}] Id = {value.Id}, Value = {value.Value}");

                            // Или напрямую:
                            p[i].Value += 100f; // модификация значения
                        }
                    }
                }
                #endregion
            }
        }

        class MyClass
        {
            public int A;
        }

        class PointRef
        {
            public int x;
            public int y;
            public override string ToString() => $"x: {x}  y: {y}";
        }

        public void UnsafeClass()
        {
            MyClass obj = new MyClass { A = 123 };

            // нельзя взять адрес ссылочного типа
            // MyClass* p = &obj; // не скомпилируется
            //но можно...

            #region GcHandle
            var obj1 = new MyClass { A = 10 };
            var obj2 = new MyClass { A = 20 };
            // Закрепляем объекты в памяти
            GCHandle handle1 = GCHandle.Alloc(obj1, GCHandleType.Pinned);
            GCHandle handle2 = GCHandle.Alloc(obj2, GCHandleType.Pinned);
            // Создаём стековый буфер из 2-х указателей
            Span<IntPtr> ptrSpan = stackalloc IntPtr[2];

            // Сохраняем адреса объектов (heap-ссылки)
            ptrSpan[0] = GCHandle.ToIntPtr(handle1);
            ptrSpan[1] = GCHandle.ToIntPtr(handle2);
            // Обработка: получение объектов обратно
            for (int i = 0; i < ptrSpan.Length; i++)
            {
                var handle = GCHandle.FromIntPtr(ptrSpan[i]);
                var o = (MyClass)handle.Target;
                Console.WriteLine($"Object[{i}] = {o.A}");
            }

            handle1.Free();
            handle2.Free();
            #endregion


            PointRef point = new PointRef();
            // блок фиксации указателя
            fixed (int* pX = &point.x)
            {
                *pX = 30;
            }
            fixed (int* pY = &point.y)
            {
                *pY = 150;
            }
        }

        public void UnsafeSpan()
        {
            int* ptr = (int*)Marshal.AllocHGlobal(4 * sizeof(int));

            for (int i = 0; i < 4; i++)
                ptr[i] = i + 1;

            Span<int> span = new Span<int>(ptr, 4);

            span[1] = 42;

            foreach (var x in span)
                Console.WriteLine(x); // 1, 42, 3, 4

            Marshal.FreeHGlobal((IntPtr)ptr);
        }
    }
}
