using LibraryApi3.Models;

namespace LibraryApi3.Data
{
    public static class BookData
    {
        public static List<Book> Books = new List<Book>
        {
            new Book { 
                Id = 1, 
                Title = "The Hobbit", 
                Author = "J.R.R. Tolkien", 
                Genre = "Fantasy", 
                PublishedYear = 1937, 
                IsAvailable = true, 
                Price = 15.99m 
            },
            new Book { 
                Id = 2, 
                Title = "1984", 
                Author = "George Orwell", 
                Genre = "Dystopian", 
                PublishedYear = 1949, 
                IsAvailable = false, 
                Price = 12.99m 
            },
            new Book { 
                Id = 3, 
                Title = "Pride and Prejudice", 
                Author = "Jane Austen", 
                Genre = "Romance", 
                PublishedYear = 1813, 
                IsAvailable = true, 
                Price = 9.99m 
            },
            new Book { 
                Id = 4, 
                Title = "The Great Gatsby", 
                Author = "F. Scott Fitzgerald", 
                Genre = "Classic", 
                PublishedYear = 1925, 
                IsAvailable = true, 
                Price = 11.99m 
            },
            new Book { 
                Id = 5, 
                Title = "Harry Potter", 
                Author = "J.K. Rowling", 
                Genre = "Fantasy", 
                PublishedYear = 1997, 
                IsAvailable = true, 
                Price = 19.99m 
            },
            new Book { 
                Id = 6, 
                Title = "Moby Dick", 
                Author = "Herman Melville", 
                Genre = "Adventure", 
                PublishedYear = 1851, 
                IsAvailable = false, 
                Price = 14.99m 
            },
            new Book { 
                Id = 7, 
                Title = "The Shining", 
                Author = "Stephen King", 
                Genre = "Horror", 
                PublishedYear = 1977, 
                IsAvailable = true, 
                Price = 13.99m 
            }
        };
    }
}

