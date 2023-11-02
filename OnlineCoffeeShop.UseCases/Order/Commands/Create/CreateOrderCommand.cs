using MediatR;

namespace OnlineCoffeeShop.Application.Order.Commands.Create;
public class CreateOrderCommand : IRequest<int>
{
    public List<OrderLineDto> OrderLines { get; set; }
}

public class OrderLineDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}