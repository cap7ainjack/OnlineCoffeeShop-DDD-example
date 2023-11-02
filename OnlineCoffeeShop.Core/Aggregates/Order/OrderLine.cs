using OnlineCoffeeShop.Domain.Common;

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

    public Order Order { get; set; } = null!;
}
