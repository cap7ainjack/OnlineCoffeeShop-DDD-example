using MediatR;
using OnlineCoffeeShop.Application.Common.Models;
using OnlineCoffeeShop.Application.Identity.Interfaces;

namespace OnlineCoffeeShop.Application.Identity.Commands.Login;
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<LoginOutputModel>>
{
    private readonly IIdentityService identity;

    public LoginUserCommandHandler(IIdentityService identity)
    {
        this.identity = identity;
    }

    public async Task<Result<LoginOutputModel>> Handle(
        LoginUserCommand request,
        CancellationToken cancellationToken)
    {
        var result = await this.identity.Login(request);

        if (!result.Succeeded)
        {
            return result.Errors;
        }

        var user = result.Data;

        return new LoginOutputModel(user.Token);
    }
}
