using MediatR;
using OnlineCoffeeShop.Application.Common;
using OnlineCoffeeShop.Domain.Common;
using OnlineCoffeeShop.Domain.Events;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OnlineCoffeeShop.Application.EventBus;

internal interface IEventBusScopedService
{
    Task DoWork(CancellationToken stoppingToken);
}

internal class EventBusScopedService : IEventBusScopedService
{
    IQueueRecevierService _queueRecevierService;
    private readonly IMediator _mediator;

    private int executionCount = 0;

    public EventBusScopedService(IQueueRecevierService queueRecevierService, IMediator mediator)
    {
        this._queueRecevierService = queueRecevierService;
        this._mediator = mediator;
    }

    public async Task DoWork(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            executionCount++;
            string rawBody = await _queueRecevierService.RecieveMessageAsync("ordersqueue");

            if (!string.IsNullOrEmpty(rawBody))
            {
                OrderSubmittedEvent parsedEvent = JsonSerializer.Deserialize<OrderSubmittedEvent>(rawBody);
                if (parsedEvent != null)
                {
                    await this._mediator.Publish(parsedEvent);
                }
            }
        }
    }
}