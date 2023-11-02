using OnlineCoffeeShop.Domain.Common;
using OnlineCoffeeShop.Domain.Exceptions;

using static OnlineCoffeeShop.Domain.Aggregates.ModelConstants.Common;

namespace OnlineCoffeeShop.Domain.Aggregates.Product;
public class Product : Entity<int>, IAggregateRoot
{
    private static readonly IEnumerable<string> AllowedCurrencies
    = new CurrencyData().GetData().Cast<string>();

    public string Name { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public Money Price { get; private set; }
    public int Quantity { get; private set; }

    public string ImageUrl { get; private set; } = string.Empty;
    private Product(string name, int quantity)
    {
        Name = name;
        Quantity = quantity;

        this.Price = default!;
    }
    internal Product(string name, int quantity, string description, Money price, string imageUrl)
    {
        this.Validate(name);
        this.ValidatePrice(price);
        this.ValidateImageUrl(imageUrl);

        Name = name;
        Quantity = quantity;
        Price = price;

        ImageUrl = imageUrl;
        this.Description = description;
    }

    public void DecreaseQuantity(int quantity)
    {
        this.ValidateQuantity(quantity);
        this.Quantity -= quantity;
    }
    public void IncreaseQuantity(int quantity)
    {
        this.ValidateQuantity(quantity);
        this.Quantity += quantity;
    }

    public Product UpdateName(string name)
    {
        this.Validate(name);
        this.Name = name;

        return this;
    }

    public void AddImageUrl(string imageUrl)
    {
        this.ValidateImageUrl(imageUrl);
        this.ImageUrl = (imageUrl);
    }

    public void RemoveImageUrl()
    {
        this.ImageUrl = default!;
    }

    public Product UpdatePrice(Money price)
    {
        this.ValidatePrice(price);
        this.Price = price;

        return this;
    }


    private void ValidatePrice(Money price)
    {
        var currency = price.Currency;

        if (AllowedCurrencies.Any(c => c == currency.ToUpper()))
        {
            return;
        }

        var allowedCurrenciyNames = string.Join(", ", AllowedCurrencies.Select(c => $"'{c}'"));

        throw new InvalidProductException($"'{currency}' is not a valid currency. Allowed values are: {allowedCurrenciyNames}.");
    }

    private void Validate(string name)
    => Guard.ForStringLength<InvalidProductException>(
        name,
        MinNameLength,
        MaxNameLength,
        nameof(Name));

    private void ValidateImageUrl(string imageUrl)
        => Guard.ForValidUrl<InvalidProductException>(
            imageUrl,
            nameof(this.ImageUrl));

    private void ValidateQuantity(int quantity)
=> Guard.AgainstZeroNegativeInteger<InvalidProductException>(
    quantity);
}
