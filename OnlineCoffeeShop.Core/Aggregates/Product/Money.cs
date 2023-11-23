using OnlineCoffeeShop.Domain.Common;
using OnlineCoffeeShop.Domain.Exceptions;

using static OnlineCoffeeShop.Domain.Aggregates.ModelConstants.Common;

namespace OnlineCoffeeShop.Domain.Aggregates.Product;
public class Money : ValueObject
{
    private static readonly IEnumerable<string> AllowedCurrencies
    = new CurrencyData().GetData().Cast<string>();

    public decimal Price { get; private set; }

    public string Currency { get; private set; }

    internal Money(decimal price, string currency)
    {
        this.Validate(price, currency);

        this.Price = price;
        this.Currency = currency;
    }

    private void Validate(decimal price, string currency)
    {
        this.Validate(price);
        this.Validate(currency);

        if (AllowedCurrencies.Any(c => c == currency.ToUpper()))
        {
            return;
        }

        var allowedCurrenciyNames = string.Join(", ", AllowedCurrencies.Select(c => $"'{c}'"));

        throw new InvalidProductException($"'{currency}' is not a valid currency. Allowed values are: {allowedCurrenciyNames}.");
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
