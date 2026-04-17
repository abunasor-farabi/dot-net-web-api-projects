using LibraryApi1.Data;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi1.Controllers
{
    // NO [ApiController] or [Route] attributes here!
    // This controller uses convention-based routing from Program.cs
    public class ReportsController : ControllerBase
    {
        // URL: GET /api/reports/sumary
        [HttpGet]
        public IActionResult Summary()
        {
            var totalBooks = BookData.Books.Count;
            var availableBooks = BookData.Books.Count(b => b.IsAvailable);

            return Ok(new
            {
                TotalBooks = totalBooks,
                AvailableBooks = availableBooks,
                AvailabilityRate = totalBooks > 0 ? 
                    (availableBooks * 100 / totalBooks) : 0
            });
        }

        // URL: GET /api/reports/byauthor/tolkien
        [HttpGet("byauthor/{author}")]
        public IActionResult GetBooksByAuthor(string author)
        {
            var books = BookData.Books.Where(b => b.Author.Contains(author, 
                StringComparison.OrdinalIgnoreCase)).ToList();

            return Ok(books);
        }
    }
}

