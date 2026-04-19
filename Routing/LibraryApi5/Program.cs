using LibraryApi5.Constraints;

var builder = WebApplication.CreateBuilder(args);

// Service Registration Section ---
// Add our custom route constraint here
builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap.Add("allowedGenres", 
        typeof(AllowedGenresConstraint));
});

builder.Services.AddControllers();
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
app.MapControllers();

app.Run();

