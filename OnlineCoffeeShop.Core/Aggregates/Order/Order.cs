using OnlineCoffeeShop.Domain.Common;
using OnlineCoffeeShop.Domain.Exceptions;

namespace OnlineCoffeeShop.Domain.Aggregates.Order;
public class Order : Entity<int>, IAggregateRoot
{
    public List<OrderLine> OrderLines { get; private set; }
    public DateTime OrderDate { get; private set; }

    internal Order(List<OrderLine> orderLines)
    {
        this.ValidateOrderLines(orderLines);
        OrderLines = orderLines;
        OrderDate = DateTime.Now;
    }

    private Order()
    {
        OrderLines = new List<OrderLine>();
    }

    private void ValidateOrderLines(List<OrderLine> orderLines)
    {
        if (!orderLines.Any() || orderLines.Any(z => z.ProductId <= 0 || z.Quantity <= 0))
        {
            throw new InvalidProductException($"Invalid product.");
        }
    }
}
