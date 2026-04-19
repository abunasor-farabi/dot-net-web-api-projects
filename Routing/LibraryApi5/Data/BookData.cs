using LibraryApi5.Models;

namespace LibraryApi5.Data
{
    public static class BookData
    {
        public static List<Book> Books = new()
        {
            new Book { 
                Id = 1, 
                Title = "The Hobbit", 
                Author = "J.R.R. Tolkien", 
                Genre = "Fantasy", 
                Rating = 5, 
                ISBN = "978-0-547-92822-7", 
                PublishedYear = 1937, 
                IsAvailable = true, 
                Price = 15.99m 
            },
            new Book { 
                Id = 2, 
                Title = "1984", 
                Author = "George Orwell", 
                Genre = "Dystopian", 
                Rating = 4, 
                ISBN = "978-0-452-28423-4", 
                PublishedYear = 1949, 
                IsAvailable = false, 
                Price = 12.99m 
            },
            new Book { 
                Id = 3, 
                Title = "Pride and Prejudice", 
                Author = "Jane Austen", 
                Genre = "Romance", 
                Rating = 5, 
                ISBN = "978-0-14-143951-8", 
                PublishedYear = 1813, 
                IsAvailable = true, 
                Price = 9.99m 
            },
            new Book { 
                Id = 4, 
                Title = "The Great Gatsby", 
                Author = "F. Scott Fitzgerald", 
                Genre = "Classic", 
                Rating = 4, 
                ISBN = "978-0-7432-7356-5", 
                PublishedYear = 1925, 
                IsAvailable = true, 
                Price = 11.99m 
            },
            new Book { 
                Id = 5, 
                Title = "Harry Potter", 
                Author = "J.K. Rowling", 
                Genre = "Fantasy", 
                Rating = 5, 
                ISBN = "978-0-439-70818-2", 
                PublishedYear = 1997, 
                IsAvailable = true, 
                Price = 19.99m 
            },
            new Book { 
                Id = 6, 
                Title = "Moby Dick", 
                Author = "Herman Melville", 
                Genre = "Adventure", 
                Rating = 3, 
                ISBN = "978-0-14-243724-7", 
                PublishedYear = 1851, 
                IsAvailable = false, 
                Price = 14.99m 
            },
            new Book { 
                Id = 7, 
                Title = "The Shining", 
                Author = "Stephen King", 
                Genre = "Horror", 
                Rating = 4, 
                ISBN = "978-0-385-12167-5", 
                PublishedYear = 1977, 
                IsAvailable = true, 
                Price = 13.99m 
            },
            new Book { 
                Id = 8, 
                Title = "Dune", 
                Author = "Frank Herbert", 
                Genre = "Sci-Fi", 
                Rating = 5, 
                ISBN = "978-0-441-17271-9", 
                PublishedYear = 1965, 
                IsAvailable = true, 
                Price = 17.99m 
            },
            new Book { 
                Id = 9, 
                Title = "The Alchemist", 
                Author = "Paulo Coelho", 
                Genre = "Fiction", 
                Rating = 4, 
                ISBN = "978-0-06-250217-4", 
                PublishedYear = 1988, 
                IsAvailable = true, 
                Price = 10.99m 
            },
            new Book { 
                Id = 10, 
                Title = "It Ends With Us", 
                Author = "Colleen Hoover", 
                Genre = "Romance", 
                Rating = 4, 
                ISBN = "978-1-501-11336-5", 
                PublishedYear = 2016, 
                IsAvailable = true, 
                Price = 12.99m 
            }
        };
    }
}

