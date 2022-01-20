using System;
using System.Collections.Generic;

namespace Collection
{
    // Тестируемый класс
    class UserCollection<T> 
    {
        List<T> col = new List<T>(); 

        public void Add(T item)
        {
            col.Add(item);
        }

        public bool Remove(T item)
        {
           return col.Remove(item);
        }

        public int Count { get { return col.Count; } }
    }
}
