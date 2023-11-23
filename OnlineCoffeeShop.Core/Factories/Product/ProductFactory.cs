using OnlineCoffeeShop.Domain.Aggregates.Product;
using OnlineCoffeeShop.Domain.Exceptions;

namespace OnlineCoffeeShop.Domain.Factories.Product;
public class ProductFactory : IProductFactory
{
    private string name = default!;
    private string description = default!;
    private int quantity = default!;
    private Money money = default!;
    private string imageUrl = default!;

    private bool nameSet = false;
    private bool priceSet = false;

    public IProductFactory WithDescription(string description)
    {
        this.description = description;
        return this;
    }

    public IProductFactory WithImage(string imageUrl)
    {
        this.imageUrl = imageUrl;
        return this;
    }

    public IProductFactory WithName(string name)
    {
        this.name = name;
        this.nameSet = true;
        return this;
    }

    public IProductFactory WithPrice(decimal price, int currency)
        => this.WithPrice(new Money(price, CurrencyEnum.FromValue(currency)));

    public IProductFactory WithPrice(Money money)
    {
        this.money = money;
        this.priceSet = true;
        return this;
    }

    public IProductFactory WithQuantity(int quantity)
    {
        this.quantity = quantity;
        return this;
    }

    public Aggregates.Product.Product Build()
    {
        if (!this.nameSet || !this.priceSet)
        {
            throw new InvalidProductException("Name and price must have a value.");
        }

        return new Aggregates.Product.Product(
            this.name, this.quantity, this.description, this.money, this.imageUrl);
    }
}
