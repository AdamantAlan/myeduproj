var thread = new Thread(() =>
{
    try
    {
        Console.Write("A");
        throw new Exception();
    }
    catch
    {
        Console.Write("B");
    }
});

thread.Start();
thread.Join();

Console.Write("C");