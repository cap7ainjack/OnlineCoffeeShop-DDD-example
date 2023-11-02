using OnlineCoffeeShop.Application.Product.Queries.Common;
using OnlineCoffeeShop.Domain.Aggregates.Order;
using OnlineCoffeeShop.Domain.Interfaces;

namespace OnlineCoffeeShop.Application.Product;
public interface IProductRepository : IRepository<Domain.Aggregates.Product.Product>
{
    Task<Domain.Aggregates.Product.Product> AddAsync(Domain.Aggregates.Product.Product entity);


    Task<ProductOutputModel?> GetDetails(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ProductOutputModel>> GetProducts(
        CancellationToken cancellationToken = default);

    Task<List<Domain.Aggregates.Product.Product>> GetProductsByIds(
    IEnumerable<int> ids,
    CancellationToken cancellationToken = default);

    Task DecreaseProductQuantity(IEnumerable<OrderLine> lines);
}
