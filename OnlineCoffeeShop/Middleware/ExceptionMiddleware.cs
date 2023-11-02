using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OnlineCoffeeShop.Application.Exceptions;
using OnlineCoffeeShop.Domain.Exceptions;
using System.Net;

namespace OnlineCoffeeShop.WebApi.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate next;

    public ExceptionMiddleware(RequestDelegate next)
    => this.next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await this.next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;

        var result = string.Empty;

        switch (exception)
        {
            case ModelValidationException modelValidationException:
                code = HttpStatusCode.BadRequest;
                result = SerializeObject(new
                {
                    ValidationDetails = true,
                    modelValidationException.Errors
                });
                break;
            case NullReferenceException _:
                code = HttpStatusCode.BadRequest;
                result = SerializeObject(new[] { "Invalid request." });
                break;
            case NotFoundException _:
                code = HttpStatusCode.NotFound;
                break;
            case BaseDomainException ex:
                code = HttpStatusCode.BadRequest;
                result = ex.Error;
                break;
            default:
                result = SerializeObject(new ErrorDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = exception.Message
                }.ToString());

                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (string.IsNullOrEmpty(result))
        {
            var error = exception.Message;

            if (exception is BaseDomainException baseDomainException)
            {
                error = baseDomainException.Error;
            }

            result = SerializeObject(new[] { error });
        }

        return context.Response.WriteAsync(result);
    }

    private static string SerializeObject(object obj)
    => JsonConvert.SerializeObject(obj, new JsonSerializerSettings
    {
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy(true, true)
        }
    });

}
