using System.Collections.Generic;

namespace _002_ConstructorInjection
{
    public class StubFileDataObject : IDataAccessObject
    {
        public List<string> GetFiles()
        {
            List<string> list = new List<string>();
            list.Add("file1.txt");
            list.Add("file2.log");
            list.Add("file3.exe");
            list.Add("main.log");

            return list;
        }
    }
}
