using LibraryApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers
{
    // NO [ApiController] or [Route] attributes here!
    // This controller will use convention-based routing from Program.cs
    public class ReportsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Will be accessible via: api/reports/summary
        [HttpGet]
        public async Task<IActionResult> Summary()
        {
            var totalBooks = await _context.Books.CountAsync();
            var availableBooks = await _context.Books.CountAsync(b => b.IsAvailable);

            return Ok(new
            {
                TotalBooks = totalBooks,
                AvailableBooks = availableBooks,
                AvailablityRate = totalBooks > 0 ? (availableBooks * 100 / totalBooks) : 0
            });
        }

        // Will be accessible via: api/reports/byauthor/jkrowling
        [HttpGet("byauthor/{author}")]
        public async Task<IActionResult> GetBooksByAuthor(string author)
        {
            var books = await _context.Books.Where(b => b.Author.Contains(author)).ToListAsync();

            return Ok(books);
        }
    }
}

