using Handler;

namespace AttTest;

[HandleFrom("Trade", 3000)]
public class Contract : IContract
{
    public string Name { get; set; }
    public int Age { get; set; }
    public DateTime? TimeStamp { get; set; }
}