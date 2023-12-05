using OnlineCoffeeShop.Domain.Aggregates.Order;
using OnlineCoffeeShop.Domain.Common;

namespace OnlineCoffeeShop.Domain.Events;
public class OrderSubmittedEvent : BaseEvent
{
    private IEnumerable<OrderLineItemDto> _lines { get; set; }
    public OrderSubmittedEvent(IEnumerable<OrderLineItemDto> lines)
    {
        this._lines = lines;
    }


    public IEnumerable<OrderLineItemDto> Lines
    {
        get { return this._lines; }
    }
}

public class OrderLineItemDto
{
    public OrderLineItemDto(int productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }


    public int ProductId { get; private set; }
    public int Quantity { get; private set; }
}
