using Duende.IdentityServer.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineCoffeeShop.Application;
using OnlineCoffeeShop.Application.Common;
using OnlineCoffeeShop.Application.Order;
using OnlineCoffeeShop.Application.Product;
using OnlineCoffeeShop.Infrastructure.Identity.Database;
using OnlineCoffeeShop.Infrastructure.Repositories;

namespace OnlineCoffeeShop.Infrastructure;
public static class InfrastructureConfiguration
{
    public static IServiceCollection AddIdentityInfrastructure(
this IServiceCollection services,
IConfiguration configuration)
    {
        services.AddDbContext<IdentityApplicationDbContext>(options =>
        {
            options.UseSqlServer
                (configuration.GetConnectionString("Default"), sqlOptions => 
                    sqlOptions.MigrationsAssembly(typeof(IdentityApplicationDbContext).Assembly.FullName));
        });

       services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<IdentityApplicationDbContext>();

        services.AddIdentityServer(opt =>
        {
            opt.Events.RaiseErrorEvents = true;
            opt.Events.RaiseSuccessEvents = true;
            opt.Events.RaiseFailureEvents = true;
            opt.Events.RaiseInformationEvents = true;

            opt.EmitStaticAudienceClaim = true;
        })
        .AddConfigurationStore(options =>
        {
            options.ConfigureDbContext = b =>
                b.UseSqlServer(configuration.GetConnectionString("Default"),
                    sql => sql
                    .MigrationsAssembly(typeof(OnlineCoffeeShopContext).Assembly.FullName));
        })
        .AddOperationalStore(options =>
        {
            options.ConfigureDbContext = b =>
                b.UseSqlServer(configuration.GetConnectionString("Default"),
                    sql => sql
                    .MigrationsAssembly(typeof(OnlineCoffeeShopContext).Assembly.FullName));
        })
        .AddAspNetIdentity<IdentityUser>();

        return services;
    }


    public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration)
    => services
        .AddDatabase(configuration)
        .AddRepositories()
        .AddIdentity(configuration)
        .AddServices();

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

    private static IServiceCollection AddServices(
    this IServiceCollection services)
    {
        return services
            .AddTransient<IQueueService, QueueService.QueueService>();
    }

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

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = configuration
                    .GetSection(nameof(ApplicationSettings)).GetSection("Authority").Value;
                options.Audience = configuration
                    .GetSection(nameof(ApplicationSettings)).GetSection("Audience").Value;

                options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
            });

        return services;
    }
}
