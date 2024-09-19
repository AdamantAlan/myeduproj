namespace Handler;

public interface IContract
{
    string Name { get; set; }
    int Age { get; set; }
    DateTime? TimeStamp { get; set; }
}