using LibraryApi4.Data;
using LibraryApi4.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi4.Controllers
{
    [ApiController]

    // Route prefix at controller level
    [Route("staff")]
    public class StaffController : ControllerBase
    {
        // CLEANER: Route inside the HTTP method attributes
        // URL: GET /staff/all
        [HttpGet("all")]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            return Ok(BookData.Books);
        }

        // URL: GET /staff/5
        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(int id)
        {
            var book = BookData.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        // URL: GET /staff/available
        [HttpGet("available")]
        public ActionResult<IEnumerable<Book>> GetAvailableBooks()
        {
            var books = BookData.Books.Where(b => b.IsAvailable).ToList();
            return Ok(books);
        }
        
        // URL: GET /staff/genre/fantasy
        [HttpGet("genre/{genre}")]
        public ActionResult<IEnumerable<Book>> GetBooksByGenre(string genre)
        {
            var books = BookData.Books
                .Where(b => b.Genre
                .Equals(genre, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return Ok(books);
        }
        
        // Override with tilde - URL: GET /info/version
        [HttpGet("~/info/version")]
        public IActionResult GetVersion()
        {
            return Ok(new { Version = "1.0.0", 
                Api = "Library Management" });
        }
    }
}

