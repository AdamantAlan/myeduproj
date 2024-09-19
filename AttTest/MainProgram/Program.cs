using Handler;

namespace AttTest;

class Program
{
    static async Task Main(string[] args)
    {
        var contract = await GetContractAsync(); // как будто мы получаем из вне контракт 
        var isHandled = await ContractHandleManager.GetHandledContractData(contract); // долгая обработка
        Console.WriteLine(isHandled);
    }

    static async Task<Contract> GetContractAsync()
    {
        await Task.Delay(2000);

        return new Contract()
        {
            Name = "Dima",
            Age = 26,
            TimeStamp = DateTime.Now
        };
    }
}