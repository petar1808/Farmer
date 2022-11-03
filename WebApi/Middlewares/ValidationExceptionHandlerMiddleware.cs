using Application.Models;
using System;
using System.Net;

namespace WebApi.Middlewares
{
    public class ValidationExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _currentEnvironment;

        public ValidationExceptionHandlerMiddleware(
            RequestDelegate next,
            IWebHostEnvironment currentEnvironment)
        {
            _next = next;
            _currentEnvironment = currentEnvironment;
        }

        public async Task Invoke(HttpContext context, ILogger<ValidationExceptionHandlerMiddleware> logger)
        {
            try
            {
                await this._next(context);
            }
            catch (Exception ex)
            {
                string message = ex.Message + ex.InnerException + ex.Source;
                if (_currentEnvironment.IsProduction())
                {
                    message = "Грешка в сървара";
                }

                logger.LogError(default(EventId), ex, ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync(message);
            }
        }
    }
}
