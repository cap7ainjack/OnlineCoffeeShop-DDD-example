using Microsoft.AspNetCore.Mvc;
using OnlineCoffeeShop.Application.Identity.Commands.Create;
using OnlineCoffeeShop.Application.Identity.Commands.Login;
using OnlineCoffeeShop.WebApi.Controllers.Base;

namespace OnlineCoffeeShop.WebApi.Controllers;

public class IdentityController : ApiController
{
    [HttpPost]
    [Route(nameof(Register))]
    public async Task<ActionResult> Register(
        CreateUserCommand command)
        => await Send(command);

    [HttpPost]
    [Route(nameof(Login))]
    public async Task<ActionResult<LoginOutputModel>> Login(
        LoginUserCommand command)
        => await Send(command);
}
