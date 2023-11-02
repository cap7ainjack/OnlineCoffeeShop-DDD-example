using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineCoffeeShop.Application.Common.Models;
using OnlineCoffeeShop.Application.Identity.Commands;
using OnlineCoffeeShop.Application.Identity.Commands.Login;
using OnlineCoffeeShop.Application.Identity.Interfaces;

namespace OnlineCoffeeShop.Infrastructure.Identity;
internal class IdentityService : IIdentityService
{
    private const string InvalidErrorMessage = "Invalid credentials.";

    private readonly UserManager<User> userManager;
    private readonly IJwtTokenGenerator jwtTokenGenerator;

    public IdentityService(UserManager<User> userManager, IJwtTokenGenerator jwtTokenGenerator)
    {
        this.userManager = userManager;
        this.jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<string> GetUserName(string userId)
        => await this.userManager
            .Users
            .Where(u => u.Id == userId)
            .Select(u => u.UserName)
            .FirstOrDefaultAsync();

    public async Task<Result> CreateUser(UserInputModel userInput)
    {
        var user = new User
        {
            UserName = userInput.Email,
            Email = userInput.Email,
        };

        var identityResult = await this.userManager.CreateAsync(user, userInput.Password);
        var errors = identityResult.Errors.Select(e => e.Description);

        return identityResult.Succeeded
            ? Result.Success()
            : Result.Failure(errors);
    }

    public async Task<Result<LoginSuccessModel>> Login(UserInputModel userInput)
    {
        var user = await this.userManager.FindByEmailAsync(userInput.Email);
        if (user == null)
        {
            return InvalidErrorMessage;
        }

        var passwordValid = await this.userManager.CheckPasswordAsync(user, userInput.Password);
        if (!passwordValid)
        {
            return InvalidErrorMessage;
        }

        var token = this.jwtTokenGenerator.GenerateToken(user);

        return new LoginSuccessModel(user.Id, token);
    }
}
