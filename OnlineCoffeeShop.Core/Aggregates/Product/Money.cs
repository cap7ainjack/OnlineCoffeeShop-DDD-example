using OnlineCoffeeShop.Domain.Common;
using OnlineCoffeeShop.Domain.Exceptions;

using static OnlineCoffeeShop.Domain.Aggregates.ModelConstants.Common;

namespace OnlineCoffeeShop.Domain.Aggregates.Product;
public class Money : ValueObject
{
    private static readonly IEnumerable<string> AllowedCurrencies
    = new CurrencyData().GetData().Cast<string>();

    public decimal Price { get; private set; }

    public CurrencyEnum Currency { get; private set; }

    internal Money(decimal price, CurrencyEnum currency)
    {
        this.Validate(price, currency);

        this.Price = price;
        this.Currency = currency;
    }

    private void Validate(decimal price, CurrencyEnum currency)
    {
        this.Validate(price);
        this.Validate(currency);
    }

    private void Validate(decimal price)
    => Guard.AgainstZeroDecimal<InvalidPriceException>(
        price,
        nameof(Price));
}
