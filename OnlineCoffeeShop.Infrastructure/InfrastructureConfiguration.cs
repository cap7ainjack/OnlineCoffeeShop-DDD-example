using Duende.IdentityServer.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineCoffeeShop.Application;
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
        //services.AddIdentity<IdentityUser, IdentityRole>()
        //    .AddEntityFrameworkStores<OnlineCoffeeShopContext>();

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

    //private static IServiceCollection AddIdentity(
    //this IServiceCollection services,
    //IConfiguration configuration)
    //{
    //    services
    //        .AddIdentityCore<User>(options =>
    //        {
    //            options.Password.RequiredLength = 6;
    //            options.Password.RequireDigit = false;
    //            options.Password.RequireLowercase = false;
    //            options.Password.RequireNonAlphanumeric = false;
    //            options.Password.RequireUppercase = false;
    //        })
    //        .AddEntityFrameworkStores<OnlineCoffeeShopContext>();

    //    var secret = configuration
    //        .GetSection(nameof(ApplicationSettings)).GetSection("Secret").Value;

    //    var key = Encoding.ASCII.GetBytes(secret);

    //    services
    //        .AddAuthentication(authentication =>
    //        {
    //            authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //            authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //        })
    //        .AddJwtBearer(bearer =>
    //        {
    //            bearer.RequireHttpsMetadata = false;
    //            bearer.SaveToken = true;
    //            bearer.TokenValidationParameters = new TokenValidationParameters
    //            {
    //                ValidateIssuerSigningKey = true,
    //                IssuerSigningKey = new SymmetricSecurityKey(key),
    //                ValidateIssuer = false,
    //                ValidateAudience = false
    //            };
    //        });

    //    services.AddTransient<IIdentityService, IdentityService>();
    //    services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();

    //    return services;
    //}
}
