using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _004_InterfaceInjection.Tests
{
    [TestClass]
    public class FileManagerTests
    {
        [TestMethod]
        public void FileManager_FindFileLogByName_4()
        {
            IDataAccessObject accessObject = new StubFileDataObject();
            FileManager fileManager = new FileManager();
            string fileName = "main.log";

            bool exists = fileManager.FindLogFile(fileName, accessObject); // Dependency Injection

            Assert.IsTrue(exists, "File {0} was not found.", fileName);
        }
    }
}
