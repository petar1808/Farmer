using Application.Exceptions;
using Domain.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using Web.Exceptions;
using Web.Controllers;

namespace Web.Middleware
{
    public class ValidationExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationExceptionHandlerMiddleware(
            RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<ValidationExceptionHandlerMiddleware> logger)
        {
            try
            {
                await this._next(context);
                if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    context.Response.Redirect("/Error/NotFound");
                }
            }
            catch (Exception exception)
            {
                logger.LogError(default(EventId), exception, exception.Message);
                
                switch (exception)
                {
                    case DomainException:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.Redirect("/Error/ServerError");
                        await context.Response.WriteAsync(exception.Message);
                        break;
                    case BadRequestExeption:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        context.Response.Redirect("/Error/BadRequest");
                        await context.Response.WriteAsync(exception.Message);
                        break;
                    default:
                        context.Response.Redirect("/Error/ServerError");
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
            }
        }
    }

    public static class ValidationExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseValidationExceptionHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware<ValidationExceptionHandlerMiddleware>();
    }
}

