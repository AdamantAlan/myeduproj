using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Microsoft.Practices.Unity;

namespace _005_DI_Container
{
    [TestFixture]
    class FileManagerTest
    {
        [Test]
        public void FindLogFileTest5()
        {
            // Создание DI контейнера.
            IUnityContainer container = new UnityContainer();

            // Регистрирование типов интерфейсов и классов, которые их реализуют для создания зависимостей.
            container.RegisterType<IDataAccessObject, StubFileDataObject>();

            // Создание объекта типа FileManager и внедрение всех зависимостей
            // в соответствии с зарегистрированными в контейнере типами.
            FileManager manager = container.Resolve<FileManager>();
            Assert.IsTrue(manager.FindLogFile("file2.log"));
        }
    }
}
