namespace OnlineCoffeeShop.Application.Common;
public interface IQueueRecevierService
{
    Task<string> RecieveMessageAsync(string queueName);
}
