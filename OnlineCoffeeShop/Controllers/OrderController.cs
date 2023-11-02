
using Microsoft.AspNetCore.Mvc;
using OnlineCoffeeShop.Application.Order.Commands.Create;
using OnlineCoffeeShop.WebApi.Controllers.Base;

namespace OnlineCoffeeShop.WebApi.Controllers;

public class OrderController : ApiController
{
    [HttpPost()]
    public async Task<ActionResult<int>> Create(CreateOrderCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
}
