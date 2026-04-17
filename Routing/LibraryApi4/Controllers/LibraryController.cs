using LibraryApi4.Data;
using LibraryApi4.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi4.Controllers
{
    [ApiController]
    
    // Step 1: Define the prefix ONCE at controller level
    [Route("library")]
    public class LibraryController : ControllerBase
    {
        // Step 2: Each method only defines what comes AFTER the prefix
        // Final URL = "library" + "all" = /library/all
        [Route("all")]
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            return Ok(BookData.Books);
        }

        // Final URL = "library" + "{id}" = /library/5
        [Route("{id}")]
        [HttpGet]
        public ActionResult<Book> GetBookById(int id)
        {
            var book = BookData.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound($"Book with {id} not found");
            }
            return Ok(book);
        }

        // Final URL = "library" + "available" = /library/available
        [Route("available")]
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAvailableBooks()
        {
            var books = BookData.Books.Where(b => b.IsAvailable).ToList();
            return Ok(books);
        }

        // Final URL = "library" + "genre/fantasy" = /library/genre/fantasy
        [Route("genre/{genre}")]
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooksByGenre(string genre)
        {
            var books = BookData.Books.Where(b => b.Genre
                .Equals(genre, StringComparison.OrdinalIgnoreCase)).ToList();
            return Ok(books);
        }

        // TILDE (~) MEANS: Ignore the controller prefix
        // Without ~ : URL would be /library/health
        // With ~ : URL becomes /health
        [Route("~/health")]
        [HttpGet]
        public ActionResult HealthCheck()
        {
            return Ok(new
            {
                Status = "API is running",
                Timestamp = DateTime.UtcNow
            });
        }
    }
}

