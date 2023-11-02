using MediatR;
using OnlineCoffeeShop.Application.Common.Models;
using OnlineCoffeeShop.Application.Identity.Interfaces;

namespace OnlineCoffeeShop.Application.Identity.Commands.Create;
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
{
    private readonly IIdentityService identity;

    public CreateUserCommandHandler(IIdentityService identity)
    {
        this.identity = identity;
    }

    public async Task<Result> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var result = await this.identity.CreateUser(request);

        if (!result.Succeeded)
        {
            return result;
        }

        return result;
    }
}
