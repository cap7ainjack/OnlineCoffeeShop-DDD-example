using OnlineCoffeeShop.Application.Common.Models;
using OnlineCoffeeShop.Application.Identity.Commands;
using OnlineCoffeeShop.Application.Identity.Commands.Login;

namespace OnlineCoffeeShop.Application.Identity.Interfaces;
public interface IIdentityService
{
    Task<string?> GetUserName(string userId);
    Task<Result> CreateUser(UserInputModel userInput);

    Task<Result<LoginSuccessModel>> Login(UserInputModel userInput);
}
