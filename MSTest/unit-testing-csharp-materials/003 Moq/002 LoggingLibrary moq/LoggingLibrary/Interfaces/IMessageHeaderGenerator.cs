namespace LoggingLibrary
{
    public interface IMessageHeaderGenerator
    {
        void CreateHeader(LogLevel level);
    }
}