using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _007_FactoryInjection
{
    public class FileManager
    {
        private IDataAccessObject dataAccessObject;

        public FileManager()
        {
            dataAccessObject = DataAccessFactory.Create();
        }

        public bool FindLogFile(string fileName)
        {
            List<string> files = dataAccessObject.GetFiles();

            foreach (var file in files)
            {
                if (file.Contains(fileName))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
