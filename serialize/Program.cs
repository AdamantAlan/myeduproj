using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace serialize
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Stream s = File.Create("human.xml");
            Human ss = new Human(123,"qwe");
            new XmlSerializer(typeof(Human)).Serialize(s,ss);
            s.Close();
            s = File.OpenRead("human.xml");
            var h= new XmlSerializer(typeof(Human)).Deserialize(s);
        }
    }
}
