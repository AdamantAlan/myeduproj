using System.Collections.Generic;

namespace _001_Problem
{
    class FileManager
    {
        public bool FindLogFile(string fileName)
        {
            // Класс FileManager напрямую зависит от объектов доступа к данным, что затрудняет его расширение и тестирование.
            // При написании тестов для данного класса, мы будем проводить интеграционное тестирование, так как тестировать придётся
            // и другие части системы. В данном случае дополнительно будет тестироваться файловая система.

            FileDataObject obj = new FileDataObject();
            //TestDataObject obj = new TestDataObject(); // для целей тестирования нужно убрать комментарий

            List<string> files = obj.GetFiles();

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