﻿using MediatR;
using OnlineCoffeeShop.Application.Product;
using OnlineCoffeeShop.Domain.Events;

namespace OnlineCoffeeShop.Application.Order.EventsHandlers;
internal class SetProductQuantityOnOrderComplete : INotificationHandler<OrderSubmittedEvent>
{
    private readonly IProductRepository _productRepository;

    public SetProductQuantityOnOrderComplete(IProductRepository productRepository)
    {
        this._productRepository = productRepository;
    }

    public async Task Handle(OrderSubmittedEvent notification, CancellationToken cancellationToken)
    {
        await this._productRepository.DecreaseProductQuantity(notification.Lines);
    }
}
