using System.Collections.Generic;

namespace _002_ConstructorInjection
{
    // В данном примере мы уменьшаем связанность выделяя интерфейс IDataAccessObject для объектов работающих с файловой системой.
    // В данном примере происходит внедрение зависимости через конструктор.
    public class FileManager
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
