using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace convert
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the text");

            string str = Console.ReadLine();
            Encoding dstEncodingFormat = Encoding.GetEncoding("windows-1251");
            byte[] originalByteString = srcEncodingFormat.GetBytes(str);
            byte[] convertedByteString = Encoding.Convert(dstEncodingFormat, srcEncodingFormat,
            originalByteString);
            string finalString = dstEncodingFormat.GetString(convertedByteString);


            Console.WriteLine("Ascii converted string: {0}", finalString);

            Console.ReadKey();
        }
    }
}
