namespace JpgRepair
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var path = "C:\\Users\\senior\\Desktop\\Новая папка\\5.jpg";
            JpgRepair.RewriteAndVerifyHeaders(path);
        }
    }
}
