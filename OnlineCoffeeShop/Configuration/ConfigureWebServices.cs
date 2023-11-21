using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineCoffeeShop.Application.Common.Models;
using OnlineCoffeeShop.Application.Identity.Commands.Create;
using OnlineCoffeeShop.Application.Identity.Commands.Login;
using OnlineCoffeeShop.Application.Order.Commands.Create;
using OnlineCoffeeShop.Application.Product.Commands.Create;
using OnlineCoffeeShop.Application.Product.Queries.ById;
using OnlineCoffeeShop.Application.Product.Queries.Common;

namespace OnlineCoffeeShop.WebApi.Configuration;

public static class ConfigureWebServices
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        // services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });

        services.AddScoped<IRequestHandler<GetProductById, ProductOutputModel>, GetProductByIdHandler>();
        services.AddScoped<IRequestHandler<CreateProductCommand, int>, CreateProductHandler>();
        services.AddScoped<IRequestHandler<CreateOrderCommand, int>, CreateOrderHandler>();

    //    services.AddScoped<IRequestHandler<CreateUserCommand, Result>, CreateUserCommandHandler>();
     //   services.AddScoped<IRequestHandler<LoginUserCommand, Result<LoginOutputModel>>, LoginUserCommandHandler>();

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

      //  services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

        return services;
    }
}
