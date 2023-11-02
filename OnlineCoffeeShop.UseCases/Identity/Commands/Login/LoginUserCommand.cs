using MediatR;
using OnlineCoffeeShop.Application.Common.Models;
namespace OnlineCoffeeShop.Application.Identity.Commands.Login;
public class LoginUserCommand : UserInputModel, IRequest<Result<LoginOutputModel>>
{
}
