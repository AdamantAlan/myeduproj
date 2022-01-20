using System;

namespace _009_LocalAbstractMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            var mgr = new FileManager();

            string fileName = "file1.txt";

            if (mgr.FindLogFile(fileName))
            {
                Console.WriteLine("File {0} is found!", fileName);
            }
            else
            {
                Console.WriteLine("File {0} is not found!", fileName);
            }

            Console.ReadKey();
        }
    }
}
