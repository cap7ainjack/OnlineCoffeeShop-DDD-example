using Ardalis.SmartEnum;

namespace OnlineCoffeeShop.Domain.Aggregates.Product;
public sealed class CurrencyEnum : SmartEnum<CurrencyEnum>
{
    public static readonly CurrencyEnum BGN = new CurrencyEnum(nameof(BGN), 1);
    public static readonly CurrencyEnum USD = new CurrencyEnum(nameof(USD), 2);
    public static readonly CurrencyEnum EUR = new CurrencyEnum(nameof(EUR), 3);

    public CurrencyEnum(string name, int value)
        : base(name, value)
    {
    }
}
