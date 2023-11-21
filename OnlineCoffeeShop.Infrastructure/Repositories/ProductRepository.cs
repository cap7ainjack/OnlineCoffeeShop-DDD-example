using Microsoft.EntityFrameworkCore;
using OnlineCoffeeShop.Application.Product;
using OnlineCoffeeShop.Application.Product.Queries.Common;
using OnlineCoffeeShop.Domain.Aggregates.Order;
using OnlineCoffeeShop.Domain.Aggregates.Product;

namespace OnlineCoffeeShop.Infrastructure.Repositories;
internal class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(OnlineCoffeeShopContext db)
    : base(db) { }

    public async Task<ProductOutputModel?> GetDetails(int id, CancellationToken cancellationToken = default)
        => await AllAvailable()
                        .Select(z => new ProductOutputModel() // TODO: Add automapper
                        {
                            Description = z.Description,
                            Id = z.Id,
                            Name = z.Name,
                            ImageUrl = z.ImageUrl
                        })
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync(cancellationToken);


    public async Task<IEnumerable<ProductOutputModel>> GetProducts(
    CancellationToken cancellationToken = default)

    {
        var products = await this.AllAvailable()
             .Select(z => new ProductOutputModel()
             {
                 Description = z.Description,
                 Id = z.Id,
                 Name = z.Name,
                 ImageUrl = z.ImageUrl
             })
             .ToListAsync(cancellationToken); ;

        return products;
    }

    public async Task<List<Product>> GetProductsByIds(
    IEnumerable<int> ids,
    CancellationToken cancellationToken = default)
    => await this
        .All()
        .Where(z => ids.Contains(z.Id))
        .ToListAsync();
    public async Task DecreaseProductQuantity(IEnumerable<OrderLine> lines)
    {
        foreach (var item in lines)
        {
            Product founded = await this.All().FirstOrDefaultAsync(z => z.Id == item.ProductId);
            if (founded != null)
            {
                founded.DecreaseQuantity(item.Quantity);
            }
        }
    }

    private IQueryable<Product> AllAvailable()
    => this
        .All()
        .Where(car => car.Quantity > 0);

}
