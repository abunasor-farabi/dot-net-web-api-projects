var builder = WebApplication.CreateBuilder(args);

// Register MVC services (Controller Discovery, Model Binding, etc.)
// No routes created yet
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();      // Finalizes DI container

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Creates RouteEndpoint objects and 
// stores in CompositeEndpointDataSource
// NET 6+ automatically adds Routing Middleware + Endpoint Middleware
app.MapControllers();

app.Run();


