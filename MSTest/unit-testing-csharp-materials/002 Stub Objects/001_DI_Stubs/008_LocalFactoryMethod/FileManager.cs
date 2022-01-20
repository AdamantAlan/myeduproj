using System.Collections.Generic;

namespace _009_LocalAbstractMethod
{
    public class FileManager 
    {
        public FileManager()
        {
        }

        // Использование фабричного метода для тестирования.
        // В юнит тесте данный метод можно переопределить для того чтобы он возвращал заглушку.
        protected virtual IDataAccessObject CreateDataAccessObject()
        {
            return new FileDataObject();
        }

        public bool FindLogFile(string fileName)
        {
            var dataAccessObject = CreateDataAccessObject();

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
