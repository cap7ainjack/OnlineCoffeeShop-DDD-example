using OnlineCoffeeShop.Domain.Aggregates.Order;
using OnlineCoffeeShop.Domain.Common;

namespace OnlineCoffeeShop.Domain.Events;
public class OrderSubmittedEvent : BaseEvent
{
    private IEnumerable<OrderLine> _lines { get; set; }
    public OrderSubmittedEvent(IEnumerable<OrderLine> lines)
    {
        this._lines = lines;
    }

    public IEnumerable<OrderLine> Lines
    {
        get { return this._lines; }
    }
}
