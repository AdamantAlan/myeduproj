using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace _006_ExtractAndOverride.Tests
{
    class FileManagerUnderTest : FileManager
    {
        protected override List<string> GetLogFiles()
        {
            return new List<string> { "file1.txt", "file2.log", "file3.exe", "main.log" };
        }
    }

    [TestClass]
    public class FileManagerTests
    {
        [TestMethod]
        public void FileManager_FindFileLogByName_6()
        {
            FileManager fileManager = new FileManagerUnderTest();
            string fileName = "main.log";

            bool exists = fileManager.FindLogFile(fileName);

            Assert.IsTrue(exists, "File {0} was not found.", fileName);
        }
    }
}
