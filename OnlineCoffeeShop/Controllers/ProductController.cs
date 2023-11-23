using Microsoft.AspNetCore.Mvc;
using OnlineCoffeeShop.Application.Product.Commands.Create;
using OnlineCoffeeShop.Application.Product.Queries.Common;
using OnlineCoffeeShop.Application.Product.Queries.ById;
using OnlineCoffeeShop.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;

namespace OnlineCoffeeShop.WebApi.Controllers;

public class ProductController : ApiController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductOutputModel>> Get(int id)
    {
        var result = await Mediator.Send(new GetProductById(id));
        return Ok(result);
    }

    [HttpPost()]
    [Authorize]
    public async Task<ActionResult<int>> Create(CreateProductCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
}
