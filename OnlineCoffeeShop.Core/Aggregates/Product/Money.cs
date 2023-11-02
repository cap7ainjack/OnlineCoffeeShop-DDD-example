using OnlineCoffeeShop.Domain.Common;
using OnlineCoffeeShop.Domain.Exceptions;

using static OnlineCoffeeShop.Domain.Aggregates.ModelConstants.Common;

namespace OnlineCoffeeShop.Domain.Aggregates.Product;
public class Money : ValueObject
{
    public decimal Price { get; private set; }

    public string Currency { get; private set; }

    internal Money(decimal price, string currency)
    {
        this.Validate(price);
        this.Validate(currency);

        this.Price = price;
        this.Currency = currency;
    }

    private void Validate(decimal price)
    => Guard.AgainstZeroDecimal<InvalidPriceException>(
        price,
        nameof(Price));

    private void Validate(string currency)
    => Guard.ForStringLength<InvalidPriceException>(
        currency,
        CurrencyLength,
        CurrencyLength,
        nameof(Currency));
}
