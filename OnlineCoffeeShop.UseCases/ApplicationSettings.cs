namespace OnlineCoffeeShop.Application;
public class ApplicationSettings
{
    public ApplicationSettings() => this.Secret = default!;

    public string Secret { get; private set; }

    public string Audience { get; private set; }

    public string Authority { get; private set; }
}
