using MediatR;
using OnlineCoffeeShop.Application.Common.Models;

namespace OnlineCoffeeShop.Application.Identity.Commands.Create;
public class CreateUserCommand : UserInputModel, IRequest<Result>
{
    public string Name { get; set; } = default!;

    public string PhoneNumber { get; set; } = default!;
}
