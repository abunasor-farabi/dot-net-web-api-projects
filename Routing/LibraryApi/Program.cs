using LibraryApi.Data;
using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add database context
builder.Services.AddDbContext<ApplicationDbContext>
(options => options.UseSqlServer
    (builder.Configuration.GetConnectionString
        ("DefaultConnection")));

var app = builder.Build();

// Configre the http request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();   // For atribute routing

// Aff this for convention-based routing
app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller}/[action]/{id?}"
);

// Seed some initial data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
    .GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();
    // Creates database if not exists

    // Add some data if none exists
    if (!dbContext.Books.Any())
    {
        dbContext.Books.AddRange(
            new Book { Title = "The Hobbit", 
                        Author = "J.R.R. Tolkien", 
                        Genre = "Fantasy", 
                        PublishedYear = 1937, 
                        Price = 15.99m, 
                        IsAvailable = true 
                    },
            new Book { Title = "1984", 
                        Author = "George Orwell", 
                        Genre = "Dystopian", 
                        PublishedYear = 1949, 
                        Price = 12.99m, 
                        IsAvailable = false 
                    },
            new Book { Title = "Pride and Prejudice", 
                        Author = "Jane Austen", 
                        Genre = "Romance", 
                        PublishedYear = 1813, 
                        Price = 9.99m, 
                        IsAvailable = true 
                    }
        );
        
        dbContext.SaveChanges();
    }
}

app.Run();

