using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class ProcessNewOrder
    {
        private readonly ILogger<ProcessNewOrder> _logger;
        private readonly ServiceBusClient _serviceBusClient;

        public ProcessNewOrder(ILogger<ProcessNewOrder> logger, ServiceBusClient serviceBusClient)
        {
            _logger = logger;
            _serviceBusClient = serviceBusClient;
        }

        [Function(nameof(ProcessNewOrder))]
        public async Task Run(
            [ServiceBusTrigger("ordersqueue", Connection = "coffeeshopservicebus_SERVICEBUS")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            // Send the message to another queue
            var sender = _serviceBusClient.CreateSender("productquantityupdatequeue");
            var newMessage = new ServiceBusMessage(message.Body)
            {
                ContentType = message.ContentType,
                MessageId = message.MessageId
            };

            await sender.SendMessageAsync(newMessage);


            // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
    }
}
