using FileManagerLibrary.DataAccessObjects;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// InternalsVisibleTo - указывает, что типы, которые обычно доступны только в текущей сборке будут доступны также в сборке,
// которая определена в параметрах.
[assembly: InternalsVisibleTo("FileManagerLibrary.Tests")]

namespace FileManagerLibrary
{
    class FileManager
    {
        private IDataAccessObject dataAccessObject;

        public FileManager()
        {
            dataAccessObject = new FileDataObject();
        }

        public FileManager(IDataAccessObject dataAccessObject)
        {
            this.dataAccessObject = dataAccessObject;
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
