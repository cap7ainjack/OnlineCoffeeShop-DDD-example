namespace OnlineCoffeeShop.Domain.Aggregates.Product;
internal class CurrencyData
{
    public Type EntityType => typeof(string);

    public IEnumerable<object> GetData()
        => new List<string>
        {
             "BGN", "USD", "EUR"  
        };
}
