using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineCoffeeShop.Application.Behaviours;
using OnlineCoffeeShop.Application.Order.Commands.Common;
using OnlineCoffeeShop.Application.Order.Commands.Create;
using OnlineCoffeeShop.Application.Product.Commands.Common;
using OnlineCoffeeShop.Application.Product.Commands.Create;
using OnlineCoffeeShop.Domain.Factories.Order;
using OnlineCoffeeShop.Domain.Factories.Product;

namespace OnlineCoffeeShop.Application;
public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplication(
    this IServiceCollection services, IConfiguration configuration)
    => services
        .Configure<ApplicationSettings>(
                    configuration.GetSection(nameof(ApplicationSettings)),
                    options => options.BindNonPublicProperties = true)
         .AddTransient<IProductFactory, ProductFactory>()
         .AddTransient<IOrderFactory, OrderFactory>()
         .AddScoped<IValidator<CreateProductCommand>, ProductCommandValidator>()
         .AddScoped<IValidator<CreateOrderCommand>, OrderCommandValidator>()
         .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
}
