using System.Collections.Generic;
using System.IO;

namespace _002_ConstructorInjection
{
    public class FileDataObject : IDataAccessObject
    {
        public List<string> GetFiles()
        {
            string path = Directory.GetCurrentDirectory();

            List<string> list = new List<string>();

            DirectoryInfo d = new DirectoryInfo(path);

            FileInfo[] files = d.GetFiles();

            foreach (var file in files)
            {
                list.Add(file.Name);
            }

            return list;
        }
    }
}
