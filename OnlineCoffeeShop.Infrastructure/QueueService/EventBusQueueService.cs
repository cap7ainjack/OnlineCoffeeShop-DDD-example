using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using OnlineCoffeeShop.Application.Common;
using System.Text.Json;

namespace OnlineCoffeeShop.Infrastructure.QueueService;
internal class EventBusQueueService: IQueueSenderService, IQueueRecevierService
{
    private IConfiguration _config;

    private const string CONNECTION_STRING_SECTION = "AzureServiceBus";
    public EventBusQueueService(IConfiguration config)
    {
        this._config = config;
    }

    public async Task SendMessageAsync<T>(T messageToSend, string queueName)
    {
        string connectionString = this._config.GetConnectionString(CONNECTION_STRING_SECTION);
        await using var client = new ServiceBusClient(connectionString);

        ServiceBusSender sender = client.CreateSender(queueName);

        string messageBody = JsonSerializer.Serialize(messageToSend);
        ServiceBusMessage message = new ServiceBusMessage(messageBody);

        await sender.SendMessageAsync(message);
    }

    public async Task<string> RecieveMessageAsync(string queueName)
    {
        string connectionString = this._config.GetConnectionString(CONNECTION_STRING_SECTION);
        await using var client = new ServiceBusClient(connectionString);

        ServiceBusReceiver receiver = client.CreateReceiver(queueName);

        ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync();

        string body = receivedMessage.Body.ToString();

        // complete the message, thereby deleting it from the service
       // await receiver.CompleteMessageAsync(receivedMessage);

        return body;
    }
}
