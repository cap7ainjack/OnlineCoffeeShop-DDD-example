using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineCoffeeShop.Application.Common.Models;
using OnlineCoffeeShop.WebApi.Common;

namespace OnlineCoffeeShop.WebApi.Controllers.Base;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase
{
    private IMediator? _mediator;

    protected IMediator Mediator
        => this._mediator ??= this.HttpContext
            .RequestServices
            .GetService<IMediator>();

    protected Task<ActionResult<TResult>> Send<TResult>(IRequest<TResult> request)
    => this.Mediator.Send(request).ToActionResult();

    protected Task<ActionResult> Send(IRequest<Result> request)
        => this.Mediator.Send(request).ToActionResult();

    protected Task<ActionResult<TResult>> Send<TResult>(IRequest<Result<TResult>> request)
        => this.Mediator.Send(request).ToActionResult();
}

