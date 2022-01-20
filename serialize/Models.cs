using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace serialize
{
        [XmlRoot("Human")]
    public class Human: IDeserializationCallback
    {
        [XmlElement]
        public int Age { get; set; }
        [XmlElement]
        public string Name { get; set; }
        public Human()
        {

        }
        public Human(int age=21,string name="default")
        {
            Age = age;
            Name = name;
        }
        [OnSerialized]
        void aaa() =>
            Console.WriteLine("1"); 
        public void OnDeserialization(object sender) =>
            Console.WriteLine("2");

    }
}
