using System;
using Unity;
using Unity.Injection;

namespace UnityTest
{
    class Program
    {
       static UnityContainer unity = new UnityContainer();
        static void Main(string[] args)
        {
            //constructor
            Console.WriteLine("Hello World!");
            unity.RegisterType<IBook, Book>();
            unity.RegisterType<ILibrary, Library>("lib", new InjectionConstructor(new ResolvedParameter<IBook>("book")));
            //var book = unity.Resolve<Library>();
            var model = unity.Resolve<Library>("lib");
            model.Book.Name = "aaaa";
            //prop
            var model2 = unity.Resolve<Library2>();
            model2.Book.Name = "123";
            //method
            var model3 = unity.Resolve<Library3>();
 


            Console.ReadKey();

        }
    }

    public interface IBook
    {
        string Name { get; set; }
    }
    public class Book : IBook
    {
        public string Name { get; set; }
    }
    public class Book2 : IBook
    {
        public string Name { get; set; }
    }
    public interface ILibrary
    {

        public IBook Book { get; set; }
    }
    public class Library:ILibrary
    {
        public IBook Book { get; set; }
        public string name { get; set; }
        public Library(IBook book)
        {
            Book = book;

        }
    }
    public class Library2 : ILibrary
    {
        [Dependency]
        public IBook Book { get; set; }
       
    }
    public class Library3 : ILibrary
    {
        [Dependency]
        public IBook Book { get; set; }
        public string Book1 { get; set; }
        [InjectionMethod]
        public void test( IBook book)
        {
            book.Name = "fdghdfg";
            Book1 = book.Name;
        }

        public string Book2 { get; set; }
        [InjectionMethod]
        public void tes2t(IBook book)
        {
            book.Name = "123123g";
            Book2 = book.Name;
        }

    }


}
