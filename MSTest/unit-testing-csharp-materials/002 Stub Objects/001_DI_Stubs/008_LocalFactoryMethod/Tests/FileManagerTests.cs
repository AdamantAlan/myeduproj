using _009_LocalAbstractMethod;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _008_LocalFactoryMethod.Tests
{
    class FileManagerUnderTest : FileManager
    {
        protected override IDataAccessObject CreateDataAccessObject()
        {
            return new StubFileDataObject();
        }
    }

    [TestClass]
    public class FileManagerTests
    {
        [TestMethod]
        public void FileManager_FindFileLogByName_5()
        {;
            FileManager fileManager = new FileManagerUnderTest();
            string fileName = "main.log";

            bool exists = fileManager.FindLogFile(fileName);

            Assert.IsTrue(exists, "File {0} was not found.", fileName);
        }
    }
}
