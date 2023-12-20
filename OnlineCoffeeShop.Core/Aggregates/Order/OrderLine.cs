using OnlineCoffeeShop.Domain.Common;
using System.Text.Json.Serialization;

namespace OnlineCoffeeShop.Domain.Aggregates.Order;
public class OrderLine: Entity<int>
{
    public int ProductId { get; private set; }
    public int Quantity { get; private set; }

    internal OrderLine(int productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }

    public OrderLine() { }

    public Order Order { get; set; } = null!;
}
