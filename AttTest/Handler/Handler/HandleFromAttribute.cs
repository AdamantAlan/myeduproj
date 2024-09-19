namespace Handler;

public class HandleFromAttribute(string topic, int deadline) : Attribute
{
    //условность
    internal readonly string Topic = topic;
    internal readonly int Deadline = deadline;
}