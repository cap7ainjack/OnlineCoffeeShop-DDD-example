using OnlineCoffeeShop.Application.Order;
using OnlineCoffeeShop.Domain.Aggregates.Order;

namespace OnlineCoffeeShop.Infrastructure.Repositories;
internal class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(OnlineCoffeeShopContext db)
: base(db) { }
}
