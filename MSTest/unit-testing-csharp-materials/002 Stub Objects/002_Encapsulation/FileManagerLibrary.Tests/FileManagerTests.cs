using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileManagerLibrary.DataAccessObjects;

namespace FileManagerLibrary.Tests
{
    [TestClass]
    public class FileManagerTests
    {
        [TestMethod]
        public void FileManager_FindFileLogByName()
        {
            IDataAccessObject accessObject = new StubFileDataObject();
            // Для того, чтобы internal классы и методы были доступны в тестовой сборке, в проекте FileManagerLibrary
            // добавлен атрибут [assembly: InternalsVisibleTo("FileManagerLibrary.Tests")]
            FileManager fileManager = new FileManager(accessObject);
            string fileName = "main.log";

            bool exists = fileManager.FindLogFile(fileName);

            Assert.IsTrue(exists, "File {0} was not found.", fileName);
        }
    }
}
