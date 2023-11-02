using OnlineCoffeeShop.Domain.Aggregates.Order;
using OnlineCoffeeShop.Domain.Exceptions;

namespace OnlineCoffeeShop.Domain.Factories.Order;
public class OrderFactory : IOrderFactory
{
    private List<OrderLine> _orderLines = default!;

    public void AddOrderLine(int productId, int quantity)
    {
        if (_orderLines == null)
        {
            _orderLines = new List<OrderLine>();
        }

        this._orderLines.Add(new OrderLine(productId, quantity));
    }

    public IOrderFactory WithOrderLines(List<OrderLine> orderLines)
    {
        this._orderLines = orderLines;
        return this;
    }
    public Aggregates.Order.Order Build()
    {
        if (!this._orderLines?.Any() == true)
        {
            throw new InvalidOrderException("Invalid order lines");
        }

        return new Aggregates.Order.Order(
            this._orderLines);
    }
}
