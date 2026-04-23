using LibraryApi6.Data;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        // RouteEndpoint 1: GET /api/books
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            return Ok(BookData.Books);
        }

        // RouteEndpoint 2: GET /api/books/{id}
        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = BookData.Books
                .FirstOrDefault(b => b.Id == id);
            return 
                book is not null ? Ok(book) : NotFound();
        }

        // RouteEndpoint 3: GET /api/books/genre/{genre}
        [HttpGet("genre/{genre}")]
        public IActionResult GetByGenre(string genre)
        {
            var books = BookData.Books.Where(
                b => b.Genre.Equals(
                    genre, StringComparison.OrdinalIgnoreCase
                )
            ).ToList();
            return books.Any() ? Ok(books) : NotFound();
        }
    }
}


