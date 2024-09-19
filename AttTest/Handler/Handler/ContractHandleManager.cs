namespace Handler;

public static class ContractHandleManager
{
    public static async Task<bool> GetHandledContractData(IContract contract)
    {
        try
        {
            var handlerSettings =
                (HandleFromAttribute)contract.GetType().GetCustomAttributes(typeof(HandleFromAttribute), false)
                    .FirstOrDefault()!;

            if (handlerSettings is null)
                throw new Exception("Не найден топик");

            if (handlerSettings.Topic != "Trade")
                throw new Exception("Не верный топик");

            await Task.Delay(handlerSettings.Deadline);

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}