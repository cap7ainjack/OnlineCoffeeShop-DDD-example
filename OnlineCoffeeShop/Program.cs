using Microsoft.EntityFrameworkCore;
using OnlineCoffeeShop.Application;
using OnlineCoffeeShop.Application.Identity.Interfaces;
using OnlineCoffeeShop.Infrastructure;
using OnlineCoffeeShop.Infrastructure.Identity;
using OnlineCoffeeShop.WebApi.Configuration;
using OnlineCoffeeShop.WebApi.Middleware;
using OnlineCoffeeShop.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICurrentUser, CurrentUserService>();
builder.Services.AddWebServices();

builder.Services
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

