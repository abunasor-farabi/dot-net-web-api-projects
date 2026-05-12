using ClothsApi5_ConfigureHttpMethods.Middleware;
using ClothsApi5_ConfigureHttpMethods.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    // ============================================================
    // APPROACH 2: Register ActionFilter Globally
    // This filter will run after routing, before every action
    // ============================================================
    options.Filters.Add(new HttpMethodFilter(new[] { "GET", "POST", "DELETE" }));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// ============================================================
// APPROACH 1: Register Middleware
// This runs BEFORE routing (earliest in pipeline)
// ============================================================
var allowedMethods = new[] { "GET", "POST", "DELETE" };
app.Use(async (context, next) =>
{
    var middleware = new HttpMethodMiddleware(next, allowedMethods);
    await middleware.InvokeAsync(context);
});

app.MapControllers();

app.Run();

