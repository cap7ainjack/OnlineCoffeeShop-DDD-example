using MediatR;
using OnlineCoffeeShop.Application.Common;
using OnlineCoffeeShop.Application.Exceptions;
using OnlineCoffeeShop.Application.Product;
using OnlineCoffeeShop.Domain.Events;
using OnlineCoffeeShop.Domain.Factories.Order;

namespace OnlineCoffeeShop.Application.Order.Commands.Create;
public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IOrderFactory orderFactory;
    private readonly IOrderRepository _repository;
    private readonly IProductRepository _productRepository;
    private readonly IQueueSenderService _queueService;

    public CreateOrderHandler(
        IOrderRepository repository,
        IOrderFactory orderFactory,
        IProductRepository productRepository,
        IQueueSenderService queueService)
    {
        _repository = repository;
        this.orderFactory = orderFactory;
        this._productRepository = productRepository;
        this._queueService = queueService;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderItemProductIds = request.OrderLines.Select(z => z.ProductId);

        var productsInDb = await this._productRepository.GetProductsByIds(orderItemProductIds, cancellationToken);

        foreach (var ol in request.OrderLines)
        {
            var product = productsInDb.FirstOrDefault(z => z.Id == ol.ProductId);
            if (product == null)
            {
                throw new NotFoundException(nameof(product), ol.ProductId);
            }

            if (product.Quantity < ol.Quantity)
            {
                throw new ArgumentException($"Insufficient quantity available for the product {product.Name}.");
            }

            this.orderFactory.AddOrderLine(ol.ProductId, ol.Quantity);
        }

        var order = orderFactory
            .Build();

        //  order.AddDomainEvent(new OrderSubmittedEvent(order.OrderLines));

        var added = await _repository.AddAsync(order);

        await _repository.SaveAsync(order);

        var orderLines = order.OrderLines.Select(z => new OrderLineItemDto(z.ProductId, z.Quantity));
        await this._queueService.SendMessageAsync(new OrderSubmittedEvent(orderLines), "ordersqueue");

        return added.Id;
    }
};