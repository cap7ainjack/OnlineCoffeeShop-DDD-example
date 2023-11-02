using OnlineCoffeeShop.Domain.Interfaces;

namespace OnlineCoffeeShop.Application.Order;
public interface IOrderRepository : IRepository<Domain.Aggregates.Order.Order>
{
    Task<Domain.Aggregates.Order.Order> AddAsync(Domain.Aggregates.Order.Order entity);

}
