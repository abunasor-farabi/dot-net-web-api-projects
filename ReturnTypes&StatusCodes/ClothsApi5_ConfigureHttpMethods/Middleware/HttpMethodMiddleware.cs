using System.Text.Json;

namespace ClothsApi5_ConfigureHttpMethods.Middleware
{
    // MIDDLEWARE - Runs before routing (earliest in pipeline)
    public class HttpMethodMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string[] _allowedMethods;

        public HttpMethodMiddleware(RequestDelegate next, string[] allowedMethods)
        {
            _next = next;
            _allowedMethods = allowedMethods;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if current HTTP method is allowed
            if (!_allowedMethods.Contains(context.Request.Method))
            {
                // Return 405 immediately - request never reaches controller
                context.Response.StatusCode = StatusCodes.Status405MethodNotAllowed;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    Code = 405,
                    Message = $"Http Method '{context.Request.Method}' is not allowed." +
                    $" Allowed methods: {string.Join(", ", _allowedMethods)}",
                    Source = "HttpMethodMiddleware"
                };

                var responseJson = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(responseJson);
                return;
            }

            await _next(context);   // Continue to next middleware
        }
    }
}

