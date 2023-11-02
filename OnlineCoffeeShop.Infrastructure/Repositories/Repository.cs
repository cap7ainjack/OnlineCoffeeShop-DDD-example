using OnlineCoffeeShop.Domain.Common;
using OnlineCoffeeShop.Domain.Interfaces;

namespace OnlineCoffeeShop.Infrastructure.Repositories;
internal abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
{
    protected readonly OnlineCoffeeShopContext dbContext;

    public Repository(OnlineCoffeeShopContext context)
    {
        dbContext = context;
    }
    protected IQueryable<TEntity> All() => this.dbContext.Set<TEntity>();

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await dbContext.Set<TEntity>().AddAsync(entity);

        return entity;
    }
    public async Task SaveAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
