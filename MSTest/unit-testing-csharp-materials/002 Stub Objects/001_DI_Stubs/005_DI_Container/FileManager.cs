using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace _005_DI_Container
{
    class FileManager
    {
        public FileManager()
        {
        }

        // Свойство, через которое будет внедрена зависимость.
        [Dependency]
        public IDataAccessObject DataAccessObject { get; set; }

        public bool FindLogFile(string fileName)
        {
            if (DataAccessObject == null)
            {
                throw new ArgumentNullException("dataAccessObject", "Parameter dataAcessObject cannot be null");
            }

            List<string> files = DataAccessObject.GetFiles();

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
