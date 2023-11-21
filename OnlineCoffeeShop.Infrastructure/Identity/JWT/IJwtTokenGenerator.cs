namespace OnlineCoffeeShop.Infrastructure.Identity;
public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
