using System;

namespace testMoq
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
    public interface ILoggerDependency
    {
        string GetCurrentDirectory();
        string GetDirectoryByLoggerName(string loggerName);
        string DefaultLogger { get; }
    }
    public interface ILogWriter
    {
        string GetLogger();
        void SetLogger(string logger);
        void Write(string message);
    }
    public class Logger
    {
        private readonly ILogWriter _logWriter;

        public Logger(ILogWriter logWriter)
        {
            _logWriter = logWriter;
        }

        public void WriteLine(string message)
        {
            _logWriter.Write(message);
        }
    }
}
