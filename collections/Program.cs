using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;

namespace collections
{
    class Program
    {



        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ArrayList array =  new() { 1,2,3};
            foreach(var i in array )
            {
                Console.WriteLine(i);
            }
            CancellationTokenSource source = new CancellationTokenSource();

            Task task = new Task(() => MethodAsync(source.Token),source.Token);
            while (true)
            {
                Console.WriteLine("e");
                await Task.Delay(100);
                Console.WriteLine("e");
                await Task.Delay(100);
                Console.WriteLine("e");
                source.Cancel();
            }
        
        }


        public static async Task MethodAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            Console.WriteLine("b");
            await Task.Delay(100);
            Console.WriteLine("e");
            await Task.Delay(100);
            Console.WriteLine("e");
            await Task.Delay(100);
            Console.WriteLine("e");
            await Task.Delay(100);
            Console.WriteLine("e");
            await Task.Delay(100);
            Console.WriteLine("e");
            await Task.Delay(100);
            token.ThrowIfCancellationRequested();
        }
    }
}
