namespace OnlineCoffeeShop.Application.Common;
public interface IQueueSenderService
{
    Task SendMessageAsync<T>(T messageToSend, string queueName);
}
