using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using OnlineCoffeeShop.Application.Common;
using System.Text.Json;

namespace OnlineCoffeeShop.Infrastructure.QueueService;
internal class QueueService: IQueueService
{
    public IConfiguration _config;
    public QueueService(IConfiguration config)
    {
        this._config = config;
    }

    public async Task SendMessageAsync<T>(T messageToSend, string queueName)
    {
        string connectionString = this._config.GetConnectionString("AzureServiceBus");
        await using var client = new ServiceBusClient(connectionString);

        ServiceBusSender sender = client.CreateSender(queueName);

        string messageBody = JsonSerializer.Serialize(messageToSend);
        ServiceBusMessage message = new ServiceBusMessage(messageBody);

        await sender.SendMessageAsync(message);
    }
}
