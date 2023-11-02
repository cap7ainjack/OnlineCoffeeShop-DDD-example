namespace OnlineCoffeeShop.Application.Identity.Commands.Login;
public class LoginOutputModel
{
    public LoginOutputModel(string token)
    {
        this.Token = token;
    }

    public string Token { get; }
}
