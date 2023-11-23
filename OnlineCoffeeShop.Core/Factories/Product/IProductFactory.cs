
using OnlineCoffeeShop.Domain.Aggregates.Product;

namespace OnlineCoffeeShop.Domain.Factories.Product;
public interface IProductFactory : IFactory<Aggregates.Product.Product>
{
    IProductFactory WithName(string name);

    IProductFactory WithDescription(string description);

    IProductFactory WithQuantity(int quantity);

    IProductFactory WithPrice(decimal price, int currency);
    IProductFactory WithImage(string imageUrl);
}
