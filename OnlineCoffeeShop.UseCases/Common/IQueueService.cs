namespace OnlineCoffeeShop.Application.Common;
public interface IQueueService
{
    Task SendMessageAsync<T>(T messageToSend, string queueName);
}
