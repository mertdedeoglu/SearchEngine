using System.Net;
using System.Text.Json;

namespace SearchEngine.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

                if (context.Response.StatusCode == (int)HttpStatusCode.TooManyRequests)
                {
                    await HandleErrorAsync(context, "Rate limit exceeded. Please try again later.", "RateLimitExceeded", context.Response.StatusCode);
                    return;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");

                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                success = false,
                message = ex.Message,
                errorType = ex.GetType().Name,
                statusCode = context.Response.StatusCode
            };

            var json = JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(json);
        }
        private Task HandleErrorAsync(HttpContext context, string message, string errorType, int statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var response = new
            {
                success = false,
                message,
                errorType,
                statusCode
            };

            var json = JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(json);
        }
    }

}
