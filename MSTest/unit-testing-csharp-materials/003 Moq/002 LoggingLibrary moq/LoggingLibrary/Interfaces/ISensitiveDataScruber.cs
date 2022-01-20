namespace LoggingLibrary
{
    public interface ISensitiveDataScruber
    {
        string ClearSensitive(string message);
    }
}