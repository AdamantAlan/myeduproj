using System.Collections.Generic;

namespace _001_Problem
{
    class TestDataObject
    {
        public List<string> GetFiles()
        {
            List<string> list = new List<string>();
            list.Add("main.log");
            list.Add("file1.txt");
            list.Add("file2.log");
            list.Add("file3.exe");

            return list;
        }
    }
}
