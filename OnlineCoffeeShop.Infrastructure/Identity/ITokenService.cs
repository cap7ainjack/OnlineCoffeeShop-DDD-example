using IdentityModel.Client;

namespace OnlineCoffeeShop.Infrastructure.Identity;
internal interface ITokenService
{
    Task<TokenResponse> GetToken(string scope);
}
