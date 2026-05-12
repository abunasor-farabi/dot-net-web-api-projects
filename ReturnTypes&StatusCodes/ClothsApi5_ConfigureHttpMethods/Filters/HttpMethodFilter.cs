using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ClothsApi5_ConfigureHttpMethods.Filters
{
    // ACTION FILTER APPROACH - Runs after routing, before action executes
    public class HttpMethodFilter : IActionFilter
    {
        private readonly string[] _allowedMethods;

        public HttpMethodFilter(string[] allowedMethods)
        {
            _allowedMethods = allowedMethods;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Check if current HTTP method is allowed
            if (!_allowedMethods.Contains(context.HttpContext.Request.Method))
            {
                var response = new
                {
                    Code = 405,
                    Message = $"HTTP Method '{context.HttpContext.Request.Method}' is"+
                $" not allowed. Allowed methods: {string.Join(", ", _allowedMethods)}",
                    Source = "HttpMethodFilter",
                    Controller = context.Controller?.GetType().Name,
                    Action = context.ActionDescriptor.DisplayName
                };

                context.Result = new ObjectResult(response)
                {
                    StatusCode = 405
                };
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Not used in this example
        }
    }
}

