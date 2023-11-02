namespace OnlineCoffeeShop.Domain.Exceptions;
public class InvalidPriceException : BaseDomainException
{
    public InvalidPriceException()
    {
    }

    public InvalidPriceException(string error) => this.Error = error;
}
