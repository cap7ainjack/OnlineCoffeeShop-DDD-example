using OnlineCoffeeShop.Domain.Aggregates.Order;

namespace OnlineCoffeeShop.Domain.Factories.Order;
public interface IOrderFactory : IFactory<Aggregates.Order.Order>
{
    IOrderFactory WithOrderLines(List<OrderLine> orderLines);

    void AddOrderLine(int productId, int quantity);
}
