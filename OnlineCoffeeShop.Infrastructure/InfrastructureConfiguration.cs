using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OnlineCoffeeShop.Application;
using OnlineCoffeeShop.Application.Identity.Interfaces;
using OnlineCoffeeShop.Application.Order;
using OnlineCoffeeShop.Application.Product;
using OnlineCoffeeShop.Infrastructure.Identity;
using OnlineCoffeeShop.Infrastructure.Repositories;
using System.Text;

namespace OnlineCoffeeShop.Infrastructure;
public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration)
    => services
        .AddDatabase(configuration)
        .AddRepositories()
        .AddIdentity(configuration);

    private static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddDbContext<OnlineCoffeeShopContext>(options => options
                .UseSqlServer(
                    configuration.GetConnectionString("Default"),
                    sqlServer => sqlServer
                        .MigrationsAssembly(typeof(OnlineCoffeeShopContext)
                            .Assembly.FullName)));

    internal static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }

    private static IServiceCollection AddIdentity(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        services
            .AddIdentityCore<User>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<OnlineCoffeeShopContext>();

        var secret = configuration
            .GetSection(nameof(ApplicationSettings)).GetSection("Secret").Value;

        var key = Encoding.ASCII.GetBytes(secret);

        services
            .AddAuthentication(authentication =>
            {
                authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearer =>
            {
                bearer.RequireHttpsMetadata = false;
                bearer.SaveToken = true;
                bearer.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}
