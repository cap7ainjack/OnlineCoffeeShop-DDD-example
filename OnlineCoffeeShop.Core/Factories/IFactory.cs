using OnlineCoffeeShop.Domain.Common;

namespace OnlineCoffeeShop.Domain.Factories;
public interface IFactory<out TEntity>
        where TEntity : IAggregateRoot
{
    TEntity Build();
}
