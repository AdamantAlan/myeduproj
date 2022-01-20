using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _002_ConstructorInjection.Tests
{
    [TestClass]
    public class FileManagerTests
    {
        [TestMethod]
        public void FileManager_FindFileLogByName_2()
        {
            IDataAccessObject accessObject = new StubFileDataObject();
            FileManager fileManager = new FileManager(accessObject); // Dependency Injection
            string fileName = "main.log";

            bool exists = fileManager.FindLogFile(fileName);

            Assert.IsTrue(exists, "File {0} was not found.", fileName);
        }
    }
}

