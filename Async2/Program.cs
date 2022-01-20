using System;
using System.Threading;
using System.Threading.Tasks;

namespace Async2
{
    class Program
    {
        static async Task Main(string[] args)
        {


            Task.WaitAll(TrueMethodAsync(), Method()) ;

        }

        static async Task  Method()
        {
            Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine("#");
                }
                
            });

        }

        static async Task<string> MethodReturn()
        {
            Task<string> s =   Task.Run(() =>
            {
                return "Lorem10";

            });;
            Console.WriteLine(s);
            return await s ;
        }


        static async Task TrueMethodAsync()
        {
         Task.Run(() =>
         {
             for (int i = 0; i < 100; i++)
             {
                 Console.WriteLine("*");
             }
         });
         await   Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine("**");
                }
            });
        }


    }
    
}
