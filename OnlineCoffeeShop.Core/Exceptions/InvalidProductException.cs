namespace OnlineCoffeeShop.Domain.Exceptions;
internal class InvalidProductException : BaseDomainException
{
    public InvalidProductException()
    {
    }

    public InvalidProductException(string error) => this.Error = error;
}
