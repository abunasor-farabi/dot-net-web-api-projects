using LibraryApi1.Data;
using LibraryApi1.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        // SINGLE ROUTE PARAMETER - Get book by ID
        // URL: GET /api/books/3
        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(int id)
        {
            var book = BookData.Books.FirstOrDefault(b => b.Id == id);

            if(book == null)
            {
                return NotFound($"Book with ID {id} not found.");
            }
            return Ok(book);
        }

        // MULTIPLE ROUTE PARAMETERS - Get books by Genre and Author
        // URL: GET /api/books/genre/Fantasy/author/Tolkien
        [HttpGet("genre/{genre}/author/{author}")]
        public ActionResult<IEnumerable<Book>> GetBooksByGenreAndAuthor(string genre, string author)
        {
            var filteredBooks = BookData.Books.Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase) && b.Author.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!filteredBooks.Any())
            {
                return NotFound($"No books found with Genre '{genre}' and Author containing '{author}'.");
            }

            return Ok(filteredBooks);
        }

        // SINGLE QUERY STRING - Search by Genre
        // URL: GET /api/books/search?genre=Fantasy
        [HttpGet("search")]
        public ActionResult<IEnumerable<Book>> SearchByGenre([FromQuery] string genre)
        {
            var filteredBooks = BookData.Books.Where(b => b.Genre
                .Equals(genre, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!filteredBooks.Any())
            {
                return NotFound($"No books found in Genre '{genre}'.");
            }

            return Ok(filteredBooks);
        }

        // MULTIPLE QUERY STRINGS - Filter by Genre, Author and Availablity
        // URL: GET /api/books/filter?genre=Fantasy&author=Rowling&isAvailable=true
        [HttpGet("filter")]
        public ActionResult<IEnumerable<Book>> FilterBooks (
            [FromQuery] string? genre, 
            [FromQuery] string? author, 
            [FromQuery] bool? isAvailable
        )
        {
            var query = BookData.Books.AsQueryable();

            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(b => b.Genre.Equals(genre, 
                    StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(b => b.Author.Contains(author, 
                    StringComparison.OrdinalIgnoreCase));
            }

            if (isAvailable.HasValue)
            {
                query = query.Where(b => b.IsAvailable == isAvailable.Value);
            }

            var results = query.ToList();

            if (!results.Any())
            {
                return NotFound("No books match the provided search criteria.");
            }

            return Ok(results);
        }

        // COMPLEX TYPE BINDING - Using Model Class for many filters
        // URL: GET /api/books/advanced-search?genre=Fantasy&author=Rowling&minPrice=10&maxPrice=20
        [HttpGet("advanced-search")]
        public ActionResult<IEnumerable<Book>> AdvancedSearch([FromQuery] BookSearchFilter filter)
        {
            var query = BookData.Books.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Genre))
            {
                query = query.Where(b => b.Genre.Equals(filter.Genre, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(filter.Author))
            {
                query = query.Where(b => b.Author.Contains(filter.Author, StringComparison.OrdinalIgnoreCase));
            }

            if (filter.IsAvailable.HasValue)
            {
                query = query.Where(b => b.IsAvailable == filter.IsAvailable.Value);
            }

            if (filter.MinPrice.HasValue)
            {
                query = query.Where(b => b.Price >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(b => b.Price <= filter.MaxPrice.Value);
            }

            var results = query.ToList();

            if (!results.Any())
            {
                return NotFound("No books match the provided search criteria.");
            }

            return Ok(results);
        }

        // COMBINED: Route Parameter + Query Strings
        // URL: GET /api/books/genre/Fantasy?minPrice=10&maxPrice=20&isAvailable=true
        [HttpGet("genre/{genre}")]
        public ActionResult<IEnumerable<Book>> GetBooksByGenreWithFilters(
            [FromRoute] string genre,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] bool? isAvailable
        )
        {
            // First filter by genre (mandatory route parameters)
            var filteredBooks = BookData.Books.Where(b => b.Genre.Equals(genre, 
                StringComparison.OrdinalIgnoreCase)).AsQueryable();

            // Apply optioanl query string filters
            if (minPrice.HasValue)
            {
                filteredBooks = filteredBooks.Where(b => b.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                filteredBooks = filteredBooks.Where(b => b.Price <= maxPrice.Value);
            }

            if (isAvailable.HasValue)
            {
                filteredBooks = filteredBooks.Where(b => b.IsAvailable == isAvailable.Value);
            }

            var results = filteredBooks.ToList();

            if (!results.Any())
            {
                return NotFound($"No {genre} books match the provided criteria.");
            }

            return Ok(
                new
                {
                    Genre = genre,
                    Filters = new {minPrice, maxPrice, isAvailable},
                    Count = results.Count,
                    Books = results
                }
            );
        }
    }
}

