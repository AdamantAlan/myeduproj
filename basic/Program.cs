using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace basic
{

    class test2
    {
        public int TryParse(string x) => Convert.ToInt32(x);

        enum e
        {
            a,
            b
        }

        public delegate void nov();
        struct abcd
        {
            private int a;
            public int GetA { get { return a; } set { a = value; } }
            public double b;
            abcd(int x)
            {
                this.a = x;
                this.b = x + 1;
            }
        }
        class abc<T> where T: new() 
        {
            public T count;
            public T ins = new T();
            public T Getcount()
            {
                
                return this.count;
            }
        }

        public delegate string d();
        class testEve{
            public event d MyCar = null;

           public void Invoke() => MyCar.Invoke(); 
        }



        class Program
        {
       
            internal static string ModelCar() => "Honda";

          
          


            static void Main(string[] args)
            {
                var nameUsers = new List<int>()
            { 12,14, 15, 16, 17 };

                var q = from el in nameUsers
                        where el > 14
                        orderby el
                        select el;

                var qq = nameUsers.Where(i => i > 10).OrderByDescending(i => i).Select(i => i);






                testEve e = new testEve();
                e.MyCar += ModelCar;
                e.MyCar += () => "Civic";
                e.Invoke();
                

                abcd myStruct;
                myStruct.b = 12;
                nov n = () => { Console.WriteLine("XYZ"); };

                abc<int> a = new abc<int>();
            }
        }
    }
}



