using OnlineCoffeeShop.Domain.Common;

namespace OnlineCoffeeShop.Domain.Interfaces;
public interface IRepository<TEntity> where TEntity : IAggregateRoot
{
    Task SaveAsync(TEntity entity, CancellationToken cancellationToken = default);
}
