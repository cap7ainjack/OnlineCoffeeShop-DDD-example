
namespace OnlineCoffeeShop.Domain.Factories.Product;
public interface IProductFactory : IFactory<Aggregates.Product.Product>
{
    IProductFactory WithName(string name);

    IProductFactory WithDescription(string description);

    IProductFactory WithQuantity(int quantity);

    IProductFactory WithPrice(decimal price, string currency);
    IProductFactory WithImage(string imageUrl);
}
