using OnlineCoffeeShop.Application.Identity.Interfaces;
using System.Security.Claims;

namespace OnlineCoffeeShop.WebApi.Services;

public class CurrentUserService : ICurrentUser
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (user == null)
        {
            //throw new InvalidOperationException("This request does not have an authenticated user.");
            this.UserId = "Guest";
        }
        else
        {
            this.UserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }

    public string UserId { get; }
}
